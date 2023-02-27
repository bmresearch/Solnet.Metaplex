using Solnet.Metaplex.Bubblegum.Accounts;
using Solnet.Metaplex.Bubblegum.Errors;
using Solnet.Metaplex.Bubblegum.Types;
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
#pragma warning disable CS1591
namespace Solnet.Metaplex.Bubblegum
{
    /// <summary>
    /// NFT Compression with Metaplexs Bubblegum Client
    /// </summary>
    public partial class BubblegumClient : TransactionalBaseClient<BubblegumErrorKind>
    {
        public BubblegumClient(IRpcClient rpcClient, IStreamingRpcClient streamingRpcClient, PublicKey programId) : base(rpcClient, streamingRpcClient, programId)
        {
        }

        public async Task<ProgramAccountsResultWrapper<List<TreeConfig>>> GetTreeConfigsAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = TreeConfig.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<TreeConfig>>(res);
            List<TreeConfig> resultingAccounts = new List<TreeConfig>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => TreeConfig.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<TreeConfig>>(res, resultingAccounts);
        }

        public async Task<ProgramAccountsResultWrapper<List<Voucher>>> GetVouchersAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            var list = new List<MemCmp> { new MemCmp { Bytes = Voucher.ACCOUNT_DISCRIMINATOR_B58, Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<Voucher>>(res);
            List<Voucher> resultingAccounts = new List<Voucher>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => Voucher.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<Voucher>>(res, resultingAccounts);
        }

        public async Task<AccountResultWrapper<TreeConfig>> GetTreeConfigAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<TreeConfig>(res);
            var resultingAccount = TreeConfig.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<TreeConfig>(res, resultingAccount);
        }

        public async Task<AccountResultWrapper<Voucher>> GetVoucherAsync(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<Voucher>(res);
            var resultingAccount = Voucher.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<Voucher>(res, resultingAccount);
        }

        public async Task<SubscriptionState> SubscribeTreeConfigAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, TreeConfig> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                TreeConfig parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = TreeConfig.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<SubscriptionState> SubscribeVoucherAsync(string accountAddress, Action<SubscriptionState, Solnet.Rpc.Messages.ResponseValue<AccountInfo>, Voucher> callback, Commitment commitment = Commitment.Finalized)
        {
            SubscriptionState res = await StreamingRpcClient.SubscribeAccountInfoAsync(accountAddress, (s, e) =>
            {
                Voucher parsingResult = null;
                if (e.Value?.Data?.Count > 0)
                    parsingResult = Voucher.Deserialize(Convert.FromBase64String(e.Value.Data[0]));
                callback(s, e, parsingResult);
            }, commitment);
            return res;
        }

        public async Task<RequestResult<string>> SendCreateTreeAsync(CreateTreeAccounts accounts, uint maxDepth, uint maxBufferSize, bool? _public, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.CreateTree(accounts, maxDepth, maxBufferSize, _public, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetTreeDelegateAsync(SetTreeDelegateAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.SetTreeDelegate(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMintV1Async(MintV1Accounts accounts, MetadataArgs message, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.MintV1(accounts, message, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendMintToCollectionV1Async(MintToCollectionV1Accounts accounts, MetadataArgs metadataArgs, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.MintToCollectionV1(accounts, metadataArgs, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendVerifyCreatorAsync(VerifyCreatorAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.VerifyCreator(accounts, root, dataHash, creatorHash, nonce, index, message, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUnverifyCreatorAsync(UnverifyCreatorAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.UnverifyCreator(accounts, root, dataHash, creatorHash, nonce, index, message, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendVerifyCollectionAsync(VerifyCollectionAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.VerifyCollection(accounts, root, dataHash, creatorHash, nonce, index, message, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendUnverifyCollectionAsync(UnverifyCollectionAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.UnverifyCollection(accounts, root, dataHash, creatorHash, nonce, index, message, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendSetAndVerifyCollectionAsync(SetAndVerifyCollectionAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey collection, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.SetAndVerifyCollection(accounts, root, dataHash, creatorHash, nonce, index, message, collection, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendTransferAsync(TransferAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.Transfer(accounts, root, dataHash, creatorHash, nonce, index, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDelegateAsync(DelegateAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.Delegate(accounts, root, dataHash, creatorHash, nonce, index, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendBurnAsync(BurnAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.Burn(accounts, root, dataHash, creatorHash, nonce, index, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendRedeemAsync(RedeemAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.Redeem(accounts, root, dataHash, creatorHash, nonce, index, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCancelRedeemAsync(CancelRedeemAccounts accounts, byte[] root, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.CancelRedeem(accounts, root, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendDecompressV1Async(DecompressV1Accounts accounts, MetadataArgs metadata, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.DecompressV1(accounts, metadata, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        public async Task<RequestResult<string>> SendCompressAsync(CompressAccounts accounts, PublicKey feePayer, Func<byte[], PublicKey, byte[]> signingCallback, PublicKey programId)
        {
            TransactionInstruction instr = BubblegumProgram.Compress(accounts, programId);
            return await SignAndSendTransaction(instr, feePayer, signingCallback);
        }

        protected override Dictionary<uint, ProgramError<BubblegumErrorKind>> BuildErrorsDictionary()
        {
            return new Dictionary<uint, ProgramError<BubblegumErrorKind>> { { 6000U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.AssetOwnerMismatch, "Asset Owner Does not match") }, { 6001U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.PublicKeyMismatch, "PublicKeyMismatch") }, { 6002U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.HashingMismatch, "Hashing Mismatch Within Leaf Schema") }, { 6003U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.UnsupportedSchemaVersion, "Unsupported Schema Version") }, { 6004U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.CreatorShareTotalMustBe100, "Creator shares must sum to 100") }, { 6005U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.DuplicateCreatorAddress, "No duplicate creator addresses in metadata") }, { 6006U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.CreatorDidNotVerify, "Creator did not verify the metadata") }, { 6007U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.CreatorNotFound, "Creator not found in creator Vec") }, { 6008U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.NoCreatorsPresent, "No creators in creator Vec") }, { 6009U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.CreatorHashMismatch, "User-provided creator Vec must result in same user-provided creator hash") }, { 6010U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.DataHashMismatch, "User-provided metadata must result in same user-provided data hash") }, { 6011U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.CreatorsTooLong, "Creators list too long") }, { 6012U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.MetadataNameTooLong, "Name in metadata is too long") }, { 6013U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.MetadataSymbolTooLong, "Symbol in metadata is too long") }, { 6014U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.MetadataUriTooLong, "Uri in metadata is too long") }, { 6015U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.MetadataBasisPointsTooHigh, "Basis points in metadata cannot exceed 10000") }, { 6016U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.TreeAuthorityIncorrect, "Tree creator or tree delegate must sign.") }, { 6017U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.InsufficientMintCapacity, "Not enough unapproved mints left") }, { 6018U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.NumericalOverflowError, "NumericalOverflowError") }, { 6019U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.IncorrectOwner, "Incorrect account owner") }, { 6020U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.CollectionCannotBeVerifiedInThisInstruction, "Cannot Verify Collection in this Instruction") }, { 6021U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.CollectionNotFound, "Collection Not Found on Metadata") }, { 6022U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.AlreadyVerified, "Collection item is already verified.") }, { 6023U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.AlreadyUnverified, "Collection item is already unverified.") }, { 6024U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.UpdateAuthorityIncorrect, "Incorrect leaf metadata update authority.") }, { 6025U, new ProgramError<BubblegumErrorKind>(BubblegumErrorKind.LeafAuthorityMustSign, "This transaction must be signed by either the leaf owner or leaf delegate") }, };
        }
    }
}

