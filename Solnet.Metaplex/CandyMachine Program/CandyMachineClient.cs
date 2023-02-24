using Solnet.Metaplex.Candymachine.Accounts;
using Solnet.Metaplex.Candymachine.Errors;
using Solnet.Metaplex.Candymachine.Types;
using Solnet.Programs.Abstract;
using Solnet.Rpc;
using Solnet.Rpc.Models;
using Solnet.Programs.Models;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Core.Sockets;
using Solnet.Rpc.Types;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine
{
    /// <summary>
    /// CandyMachine client for Metaplex protocol
    /// </summary>
    public partial class CandyMachineClient : TransactionalBaseClient<CandyMachineErrorKind>
    {
        public CandyMachineClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<ProgramAccountsResultWrapper<List<CandyMachine>>> GetCandyMachinesAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = CandyMachine.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<CandyMachine>>(res);
            List<CandyMachine> resultingAccounts = new List<CandyMachine>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => CandyMachine.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<CandyMachine>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<CollectionPDA>>> GetCollectionPDAsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = CollectionPDA.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<CollectionPDA>>(res);
            List<CollectionPDA> resultingAccounts = new List<CollectionPDA>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => CollectionPDA.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<CollectionPDA>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<FreezePDA>>> GetFreezePDAsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = FreezePDA.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<FreezePDA>>(res);
            List<FreezePDA> resultingAccounts = new List<FreezePDA>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => FreezePDA.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<FreezePDA>>(res, resultingAccounts);
        }

        public async Task<AccountResultWrapper<CandyMachine>> GetCandyMachineAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<CandyMachine>(res);
            var resultingAccount = CandyMachine.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<CandyMachine>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<CollectionPDA>> GetCollectionPDAAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<CollectionPDA>(res);
            var resultingAccount = CollectionPDA.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<CollectionPDA>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<FreezePDA>> GetFreezePDAAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<FreezePDA>(res);
            var resultingAccount = FreezePDA.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<FreezePDA>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeCandyMachineAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, CandyMachine> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                CandyMachine parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = CandyMachine.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeCollectionPDAAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, CollectionPDA> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                CollectionPDA parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = CollectionPDA.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeFreezePDAAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, FreezePDA> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                FreezePDA parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = FreezePDA.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendInitializeCandyMachineAsync(InitializeCandyMachineAccounts accounts, CandyMachineData data, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.InitializeCandyMachine(accounts, data, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateCandyMachineAsync(UpdateCandyMachineAccounts accounts, CandyMachineData data, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.UpdateCandyMachine(accounts, data, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateAuthorityAsync(UpdateAuthorityAccounts accounts, PublicKey newAuthority, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.UpdateAuthority(accounts, newAuthority, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendAddConfigLinesAsync(AddConfigLinesAccounts accounts, uint index, ConfigLine[] configLines, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.AddConfigLines(accounts, index, configLines, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetCollectionAsync(SetCollectionAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.SetCollection(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendRemoveCollectionAsync(RemoveCollectionAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.RemoveCollection(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMintNftAsync(MintNftAccounts accounts, byte creatorBump, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.MintNft(accounts, creatorBump, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetCollectionDuringMintAsync(SetCollectionDuringMintAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.SetCollectionDuringMint(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendWithdrawFundsAsync(WithdrawFundsAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.WithdrawFunds(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetFreezeAsync(SetFreezeAccounts accounts, long freezeTime, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.SetFreeze(accounts, freezeTime, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendRemoveFreezeAsync(RemoveFreezeAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.RemoveFreeze(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendThawNftAsync(ThawNftAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.ThawNft(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUnlockFundsAsync(UnlockFundsAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = CandyMachineProgram.UnlockFunds(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<CandyMachineErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<CandyMachineErrorKind>> { { 6000U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.IncorrectOwner, "") }, { 6001U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.Uninitialized, "") }, { 6002U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.MintMismatch, "") }, { 6003U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.IndexGreaterThanLength, "") }, { 6004U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NumericalOverflowError, "") }, { 6005U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.TooManyCreators, "") }, { 6006U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.UuidMustBeExactly6Length, "") }, { 6007U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NotEnoughTokens, "") }, { 6008U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NotEnoughSOL, "") }, { 6009U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.TokenTransferFailed, "") }, { 6010U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.CandyMachineEmpty, "") }, { 6011U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.CandyMachineNotLive, "") }, { 6012U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.HiddenSettingsConfigsDoNotHaveConfigLines, "") }, { 6013U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.CannotChangeNumberOfLines, "") }, { 6014U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.DerivedKeyInvalid, "") }, { 6015U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.PublicKeyMismatch, "") }, { 6016U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoWhitelistToken, "") }, { 6017U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.TokenBurnFailed, "") }, { 6018U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.GatewayAppMissing, "") }, { 6019U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.GatewayTokenMissing, "") }, { 6020U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.GatewayTokenExpireTimeInvalid, "") }, { 6021U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NetworkExpireFeatureMissing, "") }, { 6022U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.CannotFindUsableConfigLine, "") }, { 6023U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.InvalidString, "") }, { 6024U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.SuspiciousTransaction, "") }, { 6025U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.CannotSwitchToHiddenSettings, "") }, { 6026U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.IncorrectSlotHashesPubkey, "") }, { 6027U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.IncorrectCollectionAuthority, "") }, { 6028U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.MismatchedCollectionPDA, "") }, { 6029U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.MismatchedCollectionMint, "") }, { 6030U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.SlotHashesEmpty, "") }, { 6031U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.MetadataAccountMustBeEmpty, "") }, { 6032U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.MissingSetCollectionDuringMint, "") }, { 6033U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoChangingCollectionDuringMint, "") }, { 6034U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.CandyCollectionRequiresRetainAuthority, "") }, { 6035U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.GatewayProgramError, "") }, { 6036U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoChangingFreezeDuringMint, "") }, { 6037U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoChangingAuthorityWithCollection, "") }, { 6038U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoChangingTokenWithFreeze, "") }, { 6039U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.InvalidThawNft, "") }, { 6040U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.IncorrectRemainingAccountsLen, "") }, { 6041U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.MissingFreezeAta, "") }, { 6042U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.IncorrectFreezeAta, "") }, { 6043U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.FreezePDAMismatch, "") }, { 6044U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.EnteredFreezeIsMoreThanMaxFreeze, "") }, { 6045U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoWithdrawWithFreeze, "") }, { 6046U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoWithdrawWithFrozenFunds, "") }, { 6047U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.MissingRemoveFreezeTokenAccounts, "") }, { 6048U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.InvalidFreezeWithdrawTokenAddress, "") }, { 6049U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.NoUnlockWithNFTsStillFrozen, "") }, { 6050U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.SizedCollectionMetadataMustBeMutable, "") }, { 6051U, new ProgramError<CandyMachineErrorKind>(CandyMachineErrorKind.CannotSwitchFromHiddenSettings, "") }, };
        }
    }
}
