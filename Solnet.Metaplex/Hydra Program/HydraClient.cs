using Solnet.Metaplex.Hydra.Accounts;
using Solnet.Metaplex.Hydra.Types;
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

namespace Solnet.Metaplex.Hydra
{
    /// <summary>
    /// Hydra Client for Metaplex Protocol
    /// </summary>
    public partial class HydraClient : TransactionalBaseClient<HydraErrorKind>
    {
        /// <summary>
        /// Hydra Client Constructor
        /// </summary>
        /// <param name="rpcClient"></param>
        /// <param name="streamingRpcClient"></param>
        /// <param name="programId"></param>
        public HydraClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }
        /// <summary>
        /// Get Fanouts
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<Fanout>>> GetFanoutsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Fanout.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Fanout>>(res);
            List<Fanout> resultingAccounts = new List<Fanout>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Fanout.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Fanout>>(res, resultingAccounts);
        }
        /// <summary>
        /// Get Fanout Mints
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<FanoutMint>>> GetFanoutMintsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = FanoutMint.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<FanoutMint>>(res);
            List<FanoutMint> resultingAccounts = new List<FanoutMint>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => FanoutMint.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<FanoutMint>>(res, resultingAccounts);
        }
        /// <summary>
        /// Get Fanout Membership Vouchers
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<FanoutMembershipVoucher>>> GetFanoutMembershipVouchersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = FanoutMembershipVoucher.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<FanoutMembershipVoucher>>(res);
            List<FanoutMembershipVoucher> resultingAccounts = new List<FanoutMembershipVoucher>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => FanoutMembershipVoucher.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<FanoutMembershipVoucher>>(res, resultingAccounts);
        }
        /// <summary>
        /// Get Fanout Membership Mint Vouchers
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<FanoutMembershipMintVoucher>>> GetFanoutMembershipMintVouchersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = FanoutMembershipMintVoucher.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<FanoutMembershipMintVoucher>>(res);
            List<FanoutMembershipMintVoucher> resultingAccounts = new List<FanoutMembershipMintVoucher>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => FanoutMembershipMintVoucher.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<FanoutMembershipMintVoucher>>(res, resultingAccounts);
        }
        /// <summary>
        /// Get Fanout
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<Fanout>> GetFanoutAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Fanout>(res);
            var resultingAccount = Fanout.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Fanout>(res, resultingAccount);
        }
        /// <summary>
        /// Get Fanout Mint
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<FanoutMint>> GetFanoutMintAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<FanoutMint>(res);
            var resultingAccount = FanoutMint.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<FanoutMint>(res, resultingAccount);
        }
        /// <summary>
        /// Get Fanout Membership Voucher
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<FanoutMembershipVoucher>> GetFanoutMembershipVoucherAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<FanoutMembershipVoucher>(res);
            var resultingAccount = FanoutMembershipVoucher.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<FanoutMembershipVoucher>(res, resultingAccount);
        }
        /// <summary>
        /// Get Fanout Membership Mint Voucher
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<FanoutMembershipMintVoucher>> GetFanoutMembershipMintVoucherAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<FanoutMembershipMintVoucher>(res);
            var resultingAccount = FanoutMembershipMintVoucher.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<FanoutMembershipMintVoucher>(res, resultingAccount);
        }
        /// <summary>
        /// Subscribe Fanout
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeFanoutAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, Fanout> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Fanout parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Fanout.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Subscribe Fanout Mint
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeFanoutMintAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, FanoutMint> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                FanoutMint parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = FanoutMint.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Subscribe Fanout Membership Voucher
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeFanoutMembershipVoucherAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, FanoutMembershipVoucher> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                FanoutMembershipVoucher parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = FanoutMembershipVoucher.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// Subscribe Fanout Membership Mint Voucher
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="callback"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<SubscriptionState> SubscribeFanoutMembershipMintVoucherAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, FanoutMembershipMintVoucher> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                FanoutMembershipMintVoucher parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = FanoutMembershipMintVoucher.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }
        /// <summary>
        /// ProcessInit
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="args"></param>
        /// <param name="model"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessInitAsync(ProcessInitAccounts accounts, InitializeFanoutArgs args, MembershipModel model, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessInit(accounts, args, model, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Initialize for Mint Instruction
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="bumpSeed"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessInitForMintAsync(ProcessInitForMintAccounts accounts, byte bumpSeed, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessInitForMint(accounts, bumpSeed, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Add Member Wallet
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="args"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessAddMemberWalletAsync(ProcessAddMemberWalletAccounts accounts, AddMemberArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessAddMemberWallet(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Add Member NFT
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="args"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessAddMemberNftAsync(ProcessAddMemberNftAccounts accounts, AddMemberArgs args, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessAddMemberNft(accounts, args, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Set Token Member Stake
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="shares"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessSetTokenMemberStakeAsync(ProcessSetTokenMemberStakeAccounts accounts, ulong shares, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessSetTokenMemberStake(accounts, shares, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Set For Token Member Stake 
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="shares"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessSetForTokenMemberStakeAsync(ProcessSetForTokenMemberStakeAccounts accounts, ulong shares, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessSetForTokenMemberStake(accounts, shares, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        ///  Send Process Distribute NFT
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="distributeForMint"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessDistributeNftAsync(ProcessDistributeNftAccounts accounts, bool distributeForMint, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessDistributeNft(accounts, distributeForMint, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Distribute Wallet
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="distributeForMint"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessDistributeWalletAsync(ProcessDistributeWalletAccounts accounts, bool distributeForMint, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessDistributeWallet(accounts, distributeForMint, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Distribute Token
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="distributeForMint"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessDistributeTokenAsync(ProcessDistributeTokenAccounts accounts, bool distributeForMint, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessDistributeToken(accounts, distributeForMint, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Send Process Sign Metadata
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessSignMetadataAsync(ProcessSignMetadataAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessSignMetadata(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Process Transfer Shares
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="shares"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessTransferSharesAsync(ProcessTransferSharesAccounts accounts, ulong shares, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessTransferShares(accounts, shares, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// Process Unstake
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="feePayer"></param>
        /// <param name="signingCallback"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendProcessUnstakeAsync(ProcessUnstakeAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = HydraProgram.ProcessUnstake(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<uint, ProgramError<HydraErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<HydraErrorKind>> { };
        }
    }
}
