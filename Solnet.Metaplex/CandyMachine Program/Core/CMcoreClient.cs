using Solnet.Metaplex.Candymachine.Core.Accounts;
using Solnet.Metaplex.Candymachine.Core.Errors;
using Solnet.Metaplex.Candymachine.Core.Types;
using Solnet.Programs.Abstract;
using Solnet.Rpc;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Core.Sockets;
using Solnet.Rpc.Types;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine.Core
{
    /// <summary>
    /// Candy Machine Core Client - The core is different than the main candy machine program
    /// </summary>
    public partial class CandyMachineCoreClient : TransactionalBaseClient<CandyMachineCoreErrorKind>
    {
        public CandyMachineCoreClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<Solnet.Programs.Models.ProgramAccountsResultWrapper<List<CandyMachine>>> GetCandyMachinesAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<Solnet.Rpc.Models.MemCmp> { new Solnet.Rpc.Models.MemCmp { Bytes = CandyMachine.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<CandyMachine>>(res);
            List<CandyMachine> resultingAccounts = new List<CandyMachine>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => CandyMachine.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new Solnet.Programs.Models.ProgramAccountsResultWrapper<List<CandyMachine>>(res, resultingAccounts);
        }

        public async Task<Solnet.Programs.Models.AccountResultWrapper<CandyMachine>> GetCandyMachineAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new Solnet.Programs.Models.AccountResultWrapper<CandyMachine>(res);
            var resultingAccount = CandyMachine.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new Solnet.Programs.Models.AccountResultWrapper<CandyMachine>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeCandyMachineAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<Solnet.Rpc.Models.AccountInfo>, CandyMachine> callback, Commitment commitment = Commitment.Finalized)
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

        public async Task<RequestResult<string>> SendAddConfigLinesAsync(AddConfigLinesAccounts accounts, uint index, ConfigLine[] configLines, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.AddConfigLines(accounts, index, configLines, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendInitializeAsync(InitializeAccounts accounts, CandyMachineData data, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.Initialize(accounts, data, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMintAsync(MintAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.Mint(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetAuthorityAsync(SetAuthorityAccounts accounts, PublicKey newAuthority, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.SetAuthority(accounts, newAuthority, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetCollectionAsync(SetCoreCollectionAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.SetCollection(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetMintAuthorityAsync(SetMintAuthorityAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.SetMintAuthority(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUpdateAsync(UpdateAccounts accounts, CandyMachineData data, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.Update(accounts, data, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendWithdrawAsync(WithdrawAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            Solnet.Rpc.Models.TransactionInstruction instr = CMCoreProgram.Withdraw(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<CandyMachineCoreErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<CandyMachineCoreErrorKind>> { { 6000U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.IncorrectOwner, "Account does not have correct owner") }, { 6001U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.Uninitialized, "Account is not initialized") }, { 6002U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.MintMismatch, "Mint Mismatch") }, { 6003U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.IndexGreaterThanLength, "Index greater than length") }, { 6004U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.NumericalOverflowError, "Numerical overflow error") }, { 6005U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.TooManyCreators, "Can only provide up to 4 creators to candy machine (because candy machine is one)") }, { 6006U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CandyMachineEmpty, "Candy machine is empty") }, { 6007U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.HiddenSettingsDoNotHaveConfigLines, "Candy machines using hidden uris do not have config lines, they have a single hash representing hashed order") }, { 6008U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CannotChangeNumberOfLines, "Cannot change number of lines unless is a hidden config") }, { 6009U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CannotSwitchToHiddenSettings, "Cannot switch to hidden settings after items available is greater than 0") }, { 6010U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.IncorrectCollectionAuthority, "Incorrect collection NFT authority") }, { 6011U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.MetadataAccountMustBeEmpty, "The metadata account has data in it, and this must be empty to mint a new NFT") }, { 6012U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.NoChangingCollectionDuringMint, "Can't change collection settings after items have begun to be minted") }, { 6013U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.ExceededLengthError, "Value longer than expected maximum value") }, { 6014U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.MissingConfigLinesSettings, "Missing config lines settings") }, { 6015U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CannotIncreaseLength, "Cannot increase the length in config lines settings") }, { 6016U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CannotSwitchFromHiddenSettings, "Cannot switch from hidden settings") }, { 6017U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CannotChangeSequentialIndexGeneration, "Cannot change sequential index generation after items have begun to be minted") }, { 6018U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CollectionKeyMismatch, "Collection public key mismatch") }, { 6019U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.CouldNotRetrieveConfigLineData, "Could not retrive config line data") }, { 6020U, new ProgramError<CandyMachineCoreErrorKind>(CandyMachineCoreErrorKind.NotFullyLoaded, "Not all config lines were added to the candy machine") }, };
        }
    }
}
