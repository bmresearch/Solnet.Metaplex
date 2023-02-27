using Solnet.Metaplex.Auctionhouse.Accounts;
using Solnet.Metaplex.Auctionhouse.Errors;
using Solnet.Metaplex.Auctionhouse.Types;
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

namespace Solnet.Metaplex.Auctionhouse
{
    /// <summary>
    /// Auction House Client
    /// </summary>
    public partial class AuctionHouseClient : TransactionalBaseClient<AuctionHouseErrorKind>
    {
        /// <summary>
        /// AH client Constructor
        /// </summary>
        /// <param name="rpcClient"></param>
        /// <param name="streamingRpcClient"></param>
        /// <param name="programId"></param>
        public AuctionHouseClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId) { }

        /// <summary>
        /// Retrieve Bids Receipts
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<BidReceipt>>> GetBidReceiptsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = BidReceipt.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<BidReceipt>>(res);
            List<BidReceipt> resultingAccounts = new List<BidReceipt>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => BidReceipt.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<BidReceipt>>(res, resultingAccounts);
        }
        /// <summary>
        /// Retrieve Listing Receipts
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<ListingReceipt>>> GetListingReceiptsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = ListingReceipt.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<ListingReceipt>>(res);
            List<ListingReceipt> resultingAccounts = new List<ListingReceipt>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => ListingReceipt.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<ListingReceipt>>(res, resultingAccounts);
        }
        /// <summary>
        /// Retrieve purchase receipts
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<PurchaseReceipt>>> GetPurchaseReceiptsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = PurchaseReceipt.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<PurchaseReceipt>>(res);
            List<PurchaseReceipt> resultingAccounts = new List<PurchaseReceipt>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PurchaseReceipt.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<PurchaseReceipt>>(res, resultingAccounts);
        }
        /// <summary>
        /// Retrieve Auction Houses
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<AuctionHouse>>> GetAuctionHousesAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = AuctionHouse.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<AuctionHouse>>(res);
            List<AuctionHouse> resultingAccounts = new List<AuctionHouse>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => AuctionHouse.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<AuctionHouse>>(res, resultingAccounts);
        }
        /// <summary>
        /// Retrieve Auctioneers
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<AuctioneerUser>>> GetAuctioneersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = AuctioneerUser.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<AuctioneerUser>>(res);
            List<AuctioneerUser> resultingAccounts = new List<AuctioneerUser>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => AuctioneerUser.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<AuctioneerUser>>(res, resultingAccounts);
        }
        /// <summary>
        /// Retrieve Bid Receipt
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<BidReceipt>> GetBidReceiptAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<BidReceipt>(res);
            var resultingAccount = BidReceipt.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<BidReceipt>(res, resultingAccount);
        }
        /// <summary>
        /// Retrieve Listing Receipt
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<ListingReceipt>> GetListingReceiptAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<ListingReceipt>(res);
            var resultingAccount = ListingReceipt.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<ListingReceipt>(res, resultingAccount);
        }
        /// <summary>
        /// Retrieve Purchase Receipt
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<PurchaseReceipt>> GetPurchaseReceiptAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<PurchaseReceipt>(res);
            var resultingAccount = PurchaseReceipt.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<PurchaseReceipt>(res, resultingAccount);
        }
        /// <summary>
        /// Retrieve Auction House
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<AuctionHouse>> GetAuctionHouseAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<AuctionHouse>(res);
            var resultingAccount = AuctionHouse.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<AuctionHouse>(res, resultingAccount);
        }
        /// <summary>
        /// Retrieve Auctioneer
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<AuctioneerUser>> GetAuctioneerAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<AuctioneerUser>(res);
            var resultingAccount = AuctioneerUser.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<AuctioneerUser>(res, resultingAccount);
        }
        /// <summary>
        /// Subscribe and receive Bid receipt 
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeBidReceiptAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, BidReceipt> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                BidReceipt parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = BidReceipt.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Subscribe and receive Listing receipt
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeListingReceiptAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, ListingReceipt> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                ListingReceipt parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = ListingReceipt.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Subscribe purchase receipt
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribePurchaseReceiptAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, PurchaseReceipt> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                PurchaseReceipt parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = PurchaseReceipt.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Subscribe and receive auction house
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeAuctionHouseAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, AuctionHouse> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                AuctionHouse parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = AuctionHouse.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Subscribe and receive Auctioneer
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeAuctioneerAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, AuctioneerUser> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                AuctioneerUser parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = AuctioneerUser.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Sign and send Withdraw From Fee instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="amount"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendWithdrawFromFeeAsync(WithdrawFromFeeAccounts accounts, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.WithdrawFromFee(accounts, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Withdraw from Treasury instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="amount"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendWithdrawFromTreasuryAsync(WithdrawFromTreasuryAccounts accounts, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.WithdrawFromTreasury(accounts, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send UpdateAuctionHouse instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="sellerFeeBasisPoints"></param>
        /// <param name="requiresSignOff"></param>
        /// <param name="canChangeSalePrice"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendUpdateAuctionHouseAsync(UpdateAuctionHouseAccounts accounts, ushort? sellerFeeBasisPoints, bool? requiresSignOff, bool? canChangeSalePrice, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.UpdateAuctionHouse(accounts, sellerFeeBasisPoints, requiresSignOff, canChangeSalePrice, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Create Auction house instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="bump"></param>
        /// <param name="feePayerBump"></param>
        /// <param name="treasuryBump"></param>
        /// <param name="sellerFeeBasisPoints"></param>
        /// <param name="requiresSignOff"></param>
        /// <param name="canChangeSalePrice"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendCreateAuctionHouseAsync(CreateAuctionHouseAccounts accounts, byte bump, byte feePayerBump, byte treasuryBump, ushort sellerFeeBasisPoints, bool requiresSignOff, bool canChangeSalePrice, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.CreateAuctionHouse(accounts, bump, feePayerBump, treasuryBump, sellerFeeBasisPoints, requiresSignOff, canChangeSalePrice, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Buy instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="tradeStateBump"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendBuyAsync(BuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.Buy(accounts, tradeStateBump, escrowPaymentBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Auctioneer Buy instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="tradeStateBump"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerBuyAsync(AuctioneerBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerBuy(accounts, tradeStateBump, escrowPaymentBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send public buy instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="tradeStateBump"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendPublicBuyAsync(PublicBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.PublicBuy(accounts, tradeStateBump, escrowPaymentBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send auctioneer public buy instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="tradeStateBump"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerPublicBuyAsync(AuctioneerPublicBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerPublicBuy(accounts, tradeStateBump, escrowPaymentBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send cancel instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendCancelAsync(CancelAccounts accounts, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.Cancel(accounts, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send auctioneer cancel instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerCancelAsync(AuctioneerCancelAccounts accounts, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerCancel(accounts, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Deposit instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="amount"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendDepositAsync(DepositAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.Deposit(accounts, escrowPaymentBump, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Auctioneer Deposit instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="amount"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerDepositAsync(AuctioneerDepositAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerDeposit(accounts, escrowPaymentBump, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send ExecuteSale instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="freeTradeStateBump"></param>
        /// <param name="programAsSignerBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendExecuteSaleAsync(ExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.ExecuteSale(accounts, escrowPaymentBump, freeTradeStateBump, programAsSignerBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Execute Parital Sale instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="freeTradeStateBump"></param>
        /// <param name="programAsSignerBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="partialOrderSize"></param>
        /// <param name="partialOrderPrice"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendExecutePartialSaleAsync(ExecutePartialSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, ulong? partialOrderSize, ulong? partialOrderPrice, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.ExecutePartialSale(accounts, escrowPaymentBump, freeTradeStateBump, programAsSignerBump, buyerPrice, tokenSize, partialOrderSize, partialOrderPrice, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Auctioneer Execute Sale instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="freeTradeStateBump"></param>
        /// <param name="programAsSignerBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerExecuteSaleAsync(AuctioneerExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerExecuteSale(accounts, escrowPaymentBump, freeTradeStateBump, programAsSignerBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Auctioneer Execute Parital Sale instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="freeTradeStateBump"></param>
        /// <param name="programAsSignerBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="partialOrderSize"></param>
        /// <param name="partialOrderPrice"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerExecutePartialSaleAsync(AuctioneerExecutePartialSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, ulong? partialOrderSize, ulong? partialOrderPrice, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerExecutePartialSale(accounts, escrowPaymentBump, freeTradeStateBump, programAsSignerBump, buyerPrice, tokenSize, partialOrderSize, partialOrderPrice, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Sell instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="tradeStateBump"></param>
        /// <param name="freeTradeStateBump"></param>
        /// <param name="programAsSignerBump"></param>
        /// <param name="buyerPrice"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendSellAsync(SellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.Sell(accounts, tradeStateBump, freeTradeStateBump, programAsSignerBump, buyerPrice, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send AuctioneerSell instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="tradeStateBump"></param>
        /// <param name="freeTradeStateBump"></param>
        /// <param name="programAsSignerBump"></param>
        /// <param name="tokenSize"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerSellAsync(AuctioneerSellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, ulong tokenSize, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerSell(accounts, tradeStateBump, freeTradeStateBump, programAsSignerBump, tokenSize, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Withdraw instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="amount"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendWithdrawAsync(WithdrawAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.Withdraw(accounts, escrowPaymentBump, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Auctioneer Withdraw instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="amount"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendAuctioneerWithdrawAsync(AuctioneerWithdrawAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.AuctioneerWithdraw(accounts, escrowPaymentBump, amount, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Close Escrow Account instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="escrowPaymentBump"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendCloseEscrowAccountAsync(CloseEscrowAccountAccounts accounts, byte escrowPaymentBump, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.CloseEscrowAccount(accounts, escrowPaymentBump, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Delegate Auctioneer instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="scopes"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendDelegateAuctioneerAsync(DelegateAuctioneerAccounts accounts, AuthorityScope[] scopes, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.DelegateAuctioneer(accounts, scopes, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Update Auctioneer instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="scopes"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendUpdateAuctioneerAsync(UpdateAuctioneerAccounts accounts, AuthorityScope[] scopes, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.UpdateAuctioneer(accounts, scopes, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send print listing receipt instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="receiptBump"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendPrintListingReceiptAsync(PrintListingReceiptAccounts accounts, byte receiptBump, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.PrintListingReceipt(accounts, receiptBump, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send cancel listing receipt instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendCancelListingReceiptAsync(CancelListingReceiptAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.CancelListingReceipt(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send print bid receipt instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="receiptBump"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendPrintBidReceiptAsync(PrintBidReceiptAccounts accounts, byte receiptBump, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.PrintBidReceipt(accounts, receiptBump, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send cancel bid receipt instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendCancelBidReceiptAsync(CancelBidReceiptAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.CancelBidReceipt(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Sign and send Print Purchase Receipt instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="purchaseReceiptBump"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendPrintPurchaseReceiptAsync(PrintPurchaseReceiptAccounts accounts, byte purchaseReceiptBump, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = AuctionHouseProgram.PrintPurchaseReceipt(accounts, purchaseReceiptBump, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        /// <summary>
        /// Build Error Book
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<uint, ProgramError<AuctionHouseErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<AuctionHouseErrorKind>> { { 6000U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.PublicKeyMismatch, "PublicKeyMismatch") }, { 6001U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.InvalidMintAuthority, "InvalidMintAuthority") }, { 6002U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.UninitializedAccount, "UninitializedAccount") }, { 6003U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.IncorrectOwner, "IncorrectOwner") }, { 6004U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.PublicKeysShouldBeUnique, "PublicKeysShouldBeUnique") }, { 6005U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.StatementFalse, "StatementFalse") }, { 6006U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.NotRentExempt, "NotRentExempt") }, { 6007U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.NumericalOverflow, "NumericalOverflow") }, { 6008U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.ExpectedSolAccount, "Expected a sol account but got an spl token account instead") }, { 6009U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.CannotExchangeSOLForSol, "Cannot exchange sol for sol") }, { 6010U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.SOLWalletMustSign, "If paying with sol, sol wallet must be signer") }, { 6011U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.CannotTakeThisActionWithoutAuctionHouseSignOff, "Cannot take this action without auction house signing too") }, { 6012U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.NoPayerPresent, "No payer present on this txn") }, { 6013U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.DerivedKeyInvalid, "Derived key invalid") }, { 6014U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.MetadataDoesntExist, "Metadata doesn't exist") }, { 6015U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.InvalidTokenAmount, "Invalid token amount") }, { 6016U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.BothPartiesNeedToAgreeToSale, "Both parties need to agree to this sale") }, { 6017U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.CannotMatchFreeSalesWithoutAuctionHouseOrSellerSignoff, "Cannot match free sales unless the auction house or seller signs off") }, { 6018U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.SaleRequiresSigner, "This sale requires a signer") }, { 6019U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.OldSellerNotInitialized, "Old seller not initialized") }, { 6020U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.SellerATACannotHaveDelegate, "Seller ata cannot have a delegate set") }, { 6021U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.BuyerATACannotHaveDelegate, "Buyer ata cannot have a delegate set") }, { 6022U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.NoValidSignerPresent, "No valid signer present") }, { 6023U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.InvalidBasisPoints, "BP must be less than or equal to 10000") }, { 6024U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.TradeStateDoesntExist, "The trade state account does not exist") }, { 6025U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.TradeStateIsNotEmpty, "The trade state is not empty") }, { 6026U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.ReceiptIsEmpty, "The receipt is empty") }, { 6027U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.InstructionMismatch, "The instruction does not match") }, { 6028U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.InvalidAuctioneer, "Invalid Auctioneer for this Auction House instance.") }, { 6029U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.MissingAuctioneerScope, "The Auctioneer does not have the correct scope for this action.") }, { 6030U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.MustUseAuctioneerHandler, "Must use auctioneer handler.") }, { 6031U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.NoAuctioneerProgramSet, "No Auctioneer program set.") }, { 6032U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.TooManyScopes, "Too many scopes.") }, { 6033U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.AuctionHouseNotDelegated, "Auction House not delegated.") }, { 6034U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.BumpSeedNotInHashMap, "Bump seed not in hash map.") }, { 6035U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.EscrowUnderRentExemption, "The instruction would drain the escrow below rent exemption threshold") }, { 6036U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.InvalidSeedsOrAuctionHouseNotDelegated, "Invalid seeds or Auction House not delegated") }, { 6037U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.BuyerTradeStateNotValid, "The buyer trade state was unable to be initialized.") }, { 6038U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.MissingElementForPartialOrder, "Partial order size and price must both be provided in a partial buy.") }, { 6039U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.NotEnoughTokensAvailableForPurchase, "Amount of tokens available for purchase is less than the partial order amount.") }, { 6040U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.PartialPriceMismatch, "Calculated partial price does not not partial price that was provided.") }, { 6041U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.AuctionHouseAlreadyDelegated, "Auction House already delegated.") }, { 6042U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.AuctioneerAuthorityMismatch, "Auctioneer Authority Mismatch") }, { 6043U, new ProgramError<AuctionHouseErrorKind>(AuctionHouseErrorKind.InsufficientFunds, "Insufficient funds in escrow account to purchase.") }, };
        }
    }


}