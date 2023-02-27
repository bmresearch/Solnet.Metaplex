#pragma warning disable CS1591
using Solnet.Metaplex.Auctioneer.Accounts;
using Solnet.Metaplex.Auctioneer.Errors;
using Solnet.Metaplex.Auctioneer.Types;
using Solnet.Programs.Abstract;
using Solnet.Programs.Models;
using Solnet.Rpc;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Core.Sockets;
using Solnet.Rpc.Models;
using Solnet.Rpc.Types;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solnet.Metaplex.Auctioneer
{

    /// <summary>
    /// Auctioneer Client for Metaplexs Auctioneer Program used with the Auction House Program
    /// </summary>
    public partial class AuctioneerClient : TransactionalBaseClient<AuctioneerErrorKind>
    {
        public AuctioneerClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<ProgramAccountsResultWrapper<List<AuctioneerAuthority>>> GetAuctioneerAuthoritysAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = AuctioneerAuthority.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<AuctioneerAuthority>>(res);
            List<AuctioneerAuthority> resultingAccounts = new List<AuctioneerAuthority>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => AuctioneerAuthority.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<AuctioneerAuthority>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<ListingConfig>>> GetListingConfigsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = ListingConfig.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<ListingConfig>>(res);
            List<ListingConfig> resultingAccounts = new List<ListingConfig>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => ListingConfig.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<ListingConfig>>(res, resultingAccounts);
        }

        public async Task<AccountResultWrapper<AuctioneerAuthority>> GetAuctioneerAuthorityAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<AuctioneerAuthority>(res);
            var resultingAccount = AuctioneerAuthority.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<AuctioneerAuthority>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<ListingConfig>> GetListingConfigAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<ListingConfig>(res);
            var resultingAccount = ListingConfig.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<ListingConfig>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeAuctioneerAuthorityAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, AuctioneerAuthority> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                AuctioneerAuthority parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = AuctioneerAuthority.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeListingConfigAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, ListingConfig> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                ListingConfig parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = ListingConfig.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendAuthorizeAsync(AuthorizeAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctioneerProgram.Authorize(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendWithdrawAsync(WithdrawAccounts accounts, byte escrowPaymentBump, byte auctioneerAuthorityBump, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctioneerProgram.Withdraw(accounts, escrowPaymentBump, auctioneerAuthorityBump, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDepositAsync(DepositAccounts accounts, byte escrowPaymentBump, byte auctioneerAuthorityBump, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctioneerProgram.Deposit(accounts, escrowPaymentBump, auctioneerAuthorityBump, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCancelAsync(CancelAccounts accounts, byte auctioneerAuthorityBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctioneerProgram.Cancel(accounts, auctioneerAuthorityBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendExecuteSaleAsync(ExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, byte auctioneerAuthorityBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctioneerProgram.ExecuteSale(accounts, escrowPaymentBump, freeTradeStateBump, programAsSignerBump, auctioneerAuthorityBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSellAsync(SellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, byte auctioneerAuthorityBump, ulong tokenSize, DateTime startTime, DateTime endTime, ulong? reservePrice, ulong? minBidIncrement, uint? timeExtPeriod, uint? timeExtDelta, bool? allowHighBidCancel, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctioneerProgram.Sell(accounts, tradeStateBump, freeTradeStateBump, programAsSignerBump, auctioneerAuthorityBump, tokenSize, startTime, endTime, reservePrice, minBidIncrement, timeExtPeriod, timeExtDelta, allowHighBidCancel, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendBuyAsync(BuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, byte auctioneerAuthorityBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctioneerProgram.Buy(accounts, tradeStateBump, escrowPaymentBump, auctioneerAuthorityBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<AuctioneerErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<AuctioneerErrorKind>> { { 6000U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.BumpSeedNotInHashMap, "Bump seed not in hash map") }, { 6001U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.AuctionNotStarted, "Auction has not started yet") }, { 6002U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.AuctionEnded, "Auction has ended") }, { 6003U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.AuctionActive, "Auction has not ended yet") }, { 6004U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.BidTooLow, "The bid was lower than the highest bid") }, { 6005U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.SignerNotAuth, "The signer must be the Auction House authority") }, { 6006U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.NotHighestBidder, "Execute Sale must be run on the highest bidder") }, { 6007U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.BelowReservePrice, "The bid price must be greater than the reserve price") }, { 6008U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.BelowBidIncrement, "The bid must match the highest bid plus the minimum bid increment") }, { 6009U, new ProgramError<AuctioneerErrorKind>(AuctioneerErrorKind.CannotCancelHighestBid, "The highest bidder is not allowed to cancel") }, };
        }
    }
}
