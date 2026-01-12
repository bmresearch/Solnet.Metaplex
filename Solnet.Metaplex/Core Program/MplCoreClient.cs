
using Solnet.Metaplex.Core.Accounts;
using Solnet.Programs.Abstract;
using Solnet.Rpc.Models;
using Solnet.Rpc;
using Solnet.Rpc.Core.Http;
using Solnet.Wallet;
using Solnet.Programs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solnet.Rpc.Types;
using Solnet.Metaplex.Core.Models;
using Solnet.Metaplex.Core.Types;
using Solnet.Programs;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Messages;
using Solnet.Wallet.Utilities;
#pragma warning disable CS1591 

namespace Solnet.Metaplex.Core
{
    public partial class MplCoreClient
    {
        public IRpcClient RpcClient { get; set; }
        public MplCoreClient(IRpcClient rpcClient)
        {
            RpcClient = rpcClient;
        }

        /// <summary>
        /// Builds the metaplex transaction, signs it then sends it to the RPC
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="feePayer"></param>
        /// <param name="transactionInstruction"></param>
        /// <param name="computebudget"></param>
        /// <param name="computeprice"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> SendMetaplexTransaction(Account[] signers, PublicKey feePayer, TransactionInstruction transactionInstruction, ulong computebudget = 100000, ulong computeprice = 10000)
        {
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = await RpcClient.GetLatestBlockHashAsync();
            byte[] Transaction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(feePayer).
                AddInstruction(ComputeBudgetProgram.SetComputeUnitLimit((uint)computebudget)).
                AddInstruction(ComputeBudgetProgram.SetComputeUnitPrice(computeprice)).
                AddInstruction(transactionInstruction).
                Build(signers);

           return await RpcClient.SendTransactionAsync(Transaction);
        }

        /// <summary>
        /// Derive all plugin header accounts owned by the Metaplex Core program
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<PluginHeaderV1>>> GetPluginHeaderV1sAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            byte[] bytes = { (byte)3 };
            var b58 = new Base58Encoder();
            var list = new List<MemCmp> { new MemCmp { Bytes = b58.EncodeData(bytes), Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<PluginHeaderV1>>(res);
            List<PluginHeaderV1> resultingAccounts = new List<PluginHeaderV1>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PluginHeaderV1.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<PluginHeaderV1>>(res, resultingAccounts);
        }
        /// <summary>
        /// Derive all plugin registry accounts owned by the Metaplex Core program
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<PluginRegistryV1>>> GetPluginRegistryV1sAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            byte[] bytes = { (byte)4 };
            var b58 = new Base58Encoder();
            var list = new List<MemCmp> { new MemCmp { Bytes = b58.EncodeData(bytes), Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<PluginRegistryV1>>(res);
            List<PluginRegistryV1> resultingAccounts = new List<PluginRegistryV1>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => PluginRegistryV1.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<PluginRegistryV1>>(res, resultingAccounts);
        }
        /// <summary>
        /// Derive all metadata accounts owned by the Metaplex Core program
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<AssetV1>>> GetAssetV1sAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            byte[] bytes = { (byte)1 };
            var b58 = new Base58Encoder();
            var list = new List<MemCmp> { new MemCmp { Bytes = b58.EncodeData(bytes) , Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<AssetV1>>(res);
            List<AssetV1> resultingAccounts = new List<AssetV1>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => AssetV1.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<AssetV1>>(res, resultingAccounts);
        }
        /// <summary>
        /// Derive all collection accounts owned by the Metaplex Core program
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<CollectionV1>>> GetCollectionV1sAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            byte[] bytes = { (byte)5 };
            var b58 = new Base58Encoder();
            var list = new List<MemCmp> { new MemCmp { Bytes = b58.EncodeData(bytes), Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<CollectionV1>>(res);
            List<CollectionV1> resultingAccounts = new List<CollectionV1>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => CollectionV1.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<CollectionV1>>(res, resultingAccounts);
        }
        /// <summary>
        /// Derive all hashed assets owned by the Metaplex Core program
        /// </summary>
        /// <param name="programAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<ProgramAccountsResultWrapper<List<HashedAssetV1>>> GetHashedAssetV1sAsync(string programAddress, Commitment commitment = Commitment.Finalized)
        {
            byte[] bytes = { (byte)2 };
            var b58 = new Base58Encoder();
            var list = new List<MemCmp> { new MemCmp { Bytes = b58.EncodeData(bytes), Offset = 0 } };
            var res = await RpcClient.GetProgramAccountsAsync(programAddress, commitment, memCmpList: list);
            Console.WriteLine(res.RawRpcResponse);
            if (!res.WasSuccessful || !(res.Result?.Count > 0))
                return new ProgramAccountsResultWrapper<List<HashedAssetV1>>(res);
            List<HashedAssetV1> resultingAccounts = new List<HashedAssetV1>(res.Result.Count);
            resultingAccounts.AddRange(res.Result.Select(result => HashedAssetV1.Deserialize(Convert.FromBase64String(result.Account.Data[0]))));
            return new ProgramAccountsResultWrapper<List<HashedAssetV1>>(res, resultingAccounts);
        }
        /// <summary>
        /// Derive all plugin header accounts owned by the Metaplex Core program
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<PluginHeaderV1>> GetPluginHeaderV1Async(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<PluginHeaderV1>(res);
            var resultingAccount = PluginHeaderV1.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<PluginHeaderV1>(res, resultingAccount);
        }
        /// <summary>
        /// Derive all plugin registry accounts owned by the Metaplex Core program
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<PluginRegistryV1>> GetPluginRegistryV1Async(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<PluginRegistryV1>(res);
            var resultingAccount = PluginRegistryV1.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<PluginRegistryV1>(res, resultingAccount);
        }
        /// <summary>
        /// Retrieve metadata from Metaplex Core asset
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<AssetV1>> GetAssetV1Async(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            Console.WriteLine(res.RawRpcResponse);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<AssetV1>(res);
            var resultingAccount = AssetV1.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<AssetV1>(res, resultingAccount);
        }
        /// <summary>
        /// Retrieve collection info from collection account
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<CollectionV1> GetCollectionV1Async(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new CollectionV1();
            var resultingAccount = CollectionV1.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return resultingAccount;
        }
        /// <summary>
        /// Retrieve metadata from hashed asset account
        /// </summary>
        /// <param name="accountAddress"></param>
        /// <param name="commitment"></param>
        /// <returns></returns>
        public async Task<AccountResultWrapper<HashedAssetV1>> GetHashedAssetV1Async(string accountAddress, Commitment commitment = Commitment.Finalized)
        {
            var res = await RpcClient.GetAccountInfoAsync(accountAddress, commitment);
            if (!res.WasSuccessful)
                return new AccountResultWrapper<HashedAssetV1>(res);
            var resultingAccount = HashedAssetV1.Deserialize(Convert.FromBase64String(res.Result.Value.Data[0]));
            return new AccountResultWrapper<HashedAssetV1>(res, resultingAccount);
        }

        /// <summary>
        /// Create Metaplex Core NFTs
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="createV1Args"></param>
        /// <param name="computebudget"></param>
        /// <param name="computeprice"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> CreateNFTAsync(Account[] signers, CreateV1Accounts accounts, CreateV1Args createV1Args, ulong computebudget = 200000, ulong computeprice = 10000)
        {
            TransactionInstruction metaplex_create_instruction = MplCoreProgram.CreateV1(accounts, createV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_create_instruction, computebudget, computeprice);
        }

        /// <summary>
        /// Create Metaplex Core NFTs
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="asset"></param>
        /// <param name="authority"></param>
        /// <param name="owner"></param>
        /// <param name="feepayer"></param>
        /// <param name="createV1Args"></param>
        /// <param name="computebudget"></param>
        /// <param name="computeprice"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> CreateNFTAsync(Account[] signers, PublicKey asset, PublicKey authority, PublicKey owner, PublicKey feepayer,  CreateV1Args createV1Args, ulong computebudget = 100000, ulong computeprice = 10000)
        {
            CreateV1Accounts accounts = new CreateV1Accounts
            {
                Asset = asset,
                Authority = authority,
                Owner = owner,
                Payer = feepayer,
            };
            TransactionInstruction metaplex_create_instruction = MplCoreProgram.CreateV1(accounts, createV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_create_instruction, computebudget, computeprice);
        }

        /// <summary>
        /// Create Metaplex Core Collections
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="createCollectionV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> CreateCollectionV1Async(Account[] signers, CreateCollectionV1Accounts accounts, CreateCollectionV1Args createCollectionV1Args)
       {
            TransactionInstruction metaplex_instruction = MplCoreProgram.CreateCollectionV1(accounts, createCollectionV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Add Plugin to Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="addPluginV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> AddPluginV1Async(Account[] signers, AddPluginV1Accounts accounts, AddPluginV1Args addPluginV1Args)
       {
            TransactionInstruction metaplex_instruction = MplCoreProgram.AddPluginV1(accounts, addPluginV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Add Plugin to Metaplex Core collection account
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="addCollectionPluginV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> AddCollectionPluginV1Async(Account[] signers, AddCollectionPluginV1Accounts accounts, AddCollectionPluginV1Args addCollectionPluginV1Args)
       {
            TransactionInstruction metaplex_instruction = MplCoreProgram.AddCollectionPluginV1(accounts, addCollectionPluginV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Remove plugin from Metaplex Core assets
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="removePluginV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> RemovePluginV1Async(Account[] signers, RemovePluginV1Accounts accounts, RemovePluginV1Args removePluginV1Args)
       {
            TransactionInstruction metaplex_instruction =  MplCoreProgram.RemovePluginV1(accounts, removePluginV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Remove plugin from Metaplex Core collections
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="removeCollectionPluginV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> RemoveCollectionPluginV1Async(Account[] signers, RemoveCollectionPluginV1Accounts accounts, RemoveCollectionPluginV1Args removeCollectionPluginV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.RemoveCollectionPluginV1(accounts, removeCollectionPluginV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Update plugin associated with a Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="updatePluginV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> UpdatePluginV1Async(Account[] signers, UpdatePluginV1Accounts accounts, UpdatePluginV1Args updatePluginV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.UpdatePluginV1(accounts, updatePluginV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Update plugin associated with a Metaplex Core Collection
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="updateCollectionPluginV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> UpdateCollectionPluginV1Async(Account[] signers, UpdateCollectionPluginV1Accounts accounts, UpdateCollectionPluginV1Args updateCollectionPluginV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.UpdateCollectionPluginV1(accounts, updateCollectionPluginV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Approve plugin authority on a Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="approvePluginAuthorityV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> ApprovePluginAuthorityV1Async(Account[] signers, ApprovePluginAuthorityV1Accounts accounts, ApprovePluginAuthorityV1Args approvePluginAuthorityV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.ApprovePluginAuthorityV1(accounts, approvePluginAuthorityV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Approve plugin authority on a Metaplex Core Collection
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="approveCollectionPluginAuthorityV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> ApproveCollectionPluginAuthorityV1Async(Account[] signers, ApproveCollectionPluginAuthorityV1Accounts accounts, ApproveCollectionPluginAuthorityV1Args approveCollectionPluginAuthorityV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.ApproveCollectionPluginAuthorityV1(accounts, approveCollectionPluginAuthorityV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Revoke plugin authority on a Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="revokePluginAuthorityV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> RevokePluginAuthorityV1Async(Account[] signers, RevokePluginAuthorityV1Accounts accounts, RevokePluginAuthorityV1Args revokePluginAuthorityV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.RevokePluginAuthorityV1(accounts, revokePluginAuthorityV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Revoke plugin authority on a Metaplex Core Collection
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="revokeCollectionPluginAuthorityV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> RevokeCollectionPluginAuthorityV1Async(Account[] signers, RevokeCollectionPluginAuthorityV1Accounts accounts, RevokeCollectionPluginAuthorityV1Args revokeCollectionPluginAuthorityV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.RevokeCollectionPluginAuthorityV1(accounts, revokeCollectionPluginAuthorityV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Burn Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="burnV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> BurnV1Async(Account[] signers, BurnV1Accounts accounts, BurnV1Args burnV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.BurnV1(accounts, burnV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Burn Metaplex Core Collection
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="burnCollectionV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> BurnCollectionV1Async(Account[] signers, BurnCollectionV1Accounts accounts, BurnCollectionV1Args burnCollectionV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.BurnCollectionV1(accounts, burnCollectionV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Transfer Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="transferV1Args"></param>

        /// <returns></returns>
        public async Task<RequestResult<string>> TransferV1Async(Account[] signers, TransferV1Accounts accounts, TransferV1Args transferV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.TransferV1(accounts, transferV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Update Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="updateV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> UpdateNFTAsync(Account[] signers, UpdateV1Accounts accounts, UpdateV1Args updateV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.UpdateV1(accounts, updateV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Update Metaplex Core Collection
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="updateCollectionV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> UpdateCollectionV1Async(Account[] signers, UpdateCollectionV1Accounts accounts, UpdateCollectionV1Args updateCollectionV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.UpdateCollectionV1(accounts, updateCollectionV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Compress Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="compressV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> CompressNFTAsync(Account[] signers, CompressV1Accounts accounts, CompressV1Args compressV1Args, ulong computebudget = 200000, ulong computeprice = 10000)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.CompressV1(accounts, compressV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction, computebudget, computeprice);
        }
        /// <summary>
        /// Decompress Metaplex Core Asset
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <param name="decompressV1Args"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> DecompressV1Async(Account[] signers, DecompressV1Accounts accounts, DecompressV1Args decompressV1Args)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.DecompressV1(accounts, decompressV1Args);
            return await SendMetaplexTransaction(signers, accounts.Payer, metaplex_instruction);
        }
        /// <summary>
        /// Collect --- Not totally sure what this does
        /// </summary>
        /// <param name="signers"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public async Task<RequestResult<string>> CollectAsync(Account[] signers, CollectAccounts accounts)
       {
           TransactionInstruction metaplex_instruction = MplCoreProgram.Collect(accounts);
            return await SendMetaplexTransaction(signers, signers[0], metaplex_instruction);
        }
     

    }
}
