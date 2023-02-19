using Solnet.Metaplex.NFT.Library;
using Solnet.Metaplex.Utilities;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solnet.Metaplex.NFT
{
    /// <summary>
    ///  A simple Metaplex Client to make create and minting metadata tokens extremely easy
    /// </summary>
    public class MetaplexClient
    {
        /// <summary>
        /// RPC client used to send transactions to the blockchain
        /// </summary>
        public IRpcClient RpcClient { get; set; }

        /// <summary>
        /// Initialize metaplex client
        /// </summary>
        /// <param name="_RPCclient"></param>
        public MetaplexClient(IRpcClient _RPCclient)
        {
            RpcClient = _RPCclient;
        }

        /// <summary>
        /// Omni-Create Instruction to create a metadata token of any type
        /// </summary>
        /// <param name="ownerAccount"> Owner Account - Controller Wallet/Account </param>
        /// <param name="mintAccount"> Mint Account - The token account that will become the mint account once the mint is initialized </param>
        /// <param name="tokenStandard"> Token Standard - Required to create a specific type of token. Semi-fungible, fungible, non-fungible or programmable.</param>
        /// <param name="metaData"> Metadata Objec that contains all the on-chain data including the off-chain data uri</param>
        /// <param name="isMasterEdition"> Is Masteredition - true or false </param>
        /// <param name="isMutable"> Is updatable - true or false</param>
        /// <param name="metadataVersion"> Metadataversion used to access different versions of the metadata instructions</param>
        /// <param name="_Authority"> Authority Address - Optional - ownerAccount by default</param>
        /// <param name="_UpdateAuthority"> Update Authority - Optional - owneraccount by default</param>
        /// <param name="_payerAddress">Fee Payer Address - Optional - ownerAccount by default</param>
        /// <param name="delegateAddress">Delegate address - Optional</param>
        /// <param name="delegateRole">Delegate role - optional - required if delegate address is used</param>
        /// <param name="UpdateAuthorityIsSigner">Is the update authority a signer? True by default as the owneraccount</param>
        /// <param name="collectionDetails">Collection details data</param>
        /// <param name="maxSupply">Max supply of a master edition token</param>
        /// <returns></returns>
        public async Task<RequestResult<string>> CreateNFT(Account ownerAccount, Account mintAccount, TokenStandard tokenStandard, Metadata metaData, bool isMasterEdition, bool isMutable, MetadataVersion metadataVersion = MetadataVersion.V4, PublicKey _Authority = null, Account _UpdateAuthority = null, PublicKey _payerAddress = null, PublicKey delegateAddress = null, MetadataDelegateRole delegateRole = MetadataDelegateRole.Update, bool UpdateAuthorityIsSigner = true, ulong collectionDetails = 0, int maxSupply = 0)
        {
            PublicKey Authority = ownerAccount.PublicKey;
            Account UpdateAuthority = ownerAccount;
            PublicKey PayerAddress = ownerAccount.PublicKey;
            PublicKey metadataAddress = PDALookup.FindMetadataPDA(mintAccount);
            PublicKey tokenRecord = PDALookup.FindTokenRecordPDA(ownerAccount, mintAccount);
            PublicKey masterEditionAddress = null;
            PublicKey delegateRecord = null;
            PublicKey rulesetAddress = null;
            List<Account> Signers = new() { ownerAccount, mintAccount };

            //Override default values if specific parameters are supplied
            if (delegateAddress != null)
            {
                delegateRecord = PDALookup.FindDelegateRecordPDA(UpdateAuthority, mintAccount.PublicKey, delegateAddress, delegateRole);
            }
            if (isMasterEdition == true)
            {
                masterEditionAddress = PDALookup.FindMasterEditionPDA(mintAccount.PublicKey);
            }
            if (Authority != _Authority && _Authority != null)
            {
                Authority = _Authority;
            }
            if (UpdateAuthority != _UpdateAuthority && _UpdateAuthority != null)
            {
                UpdateAuthority = _UpdateAuthority;

                Signers.Add(UpdateAuthority);
            }
            if (PayerAddress != _payerAddress && _payerAddress != null)
            {
                PayerAddress = _payerAddress;
            }
            if (metaData.programmableConfig != null)
            {
                rulesetAddress = metaData.programmableConfig.key;
            }


            ulong minBalanceForExemptionMint = (await RpcClient.GetMinimumBalanceForRentExemptionAsync(TokenProgram.MintAccountDataSize)).Result;
            TransactionInstruction NFTmetadata_instruction = MetadataProgram.CreateMetadataAccount(metadataAddress, mintAccount.PublicKey, Authority, PayerAddress, UpdateAuthority, metaData, tokenStandard, isMutable, UpdateAuthorityIsSigner, masterEditionAddress, maxSupply, collectionDetails);
            TransactionInstruction mintPNFT = MetadataProgram.Mint(AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(ownerAccount, mintAccount), metadataAddress, masterEditionAddress, mintAccount, Authority, PayerAddress, delegateRecord, tokenRecord, ownerAccount, rulesetAddress, 1);
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = await RpcClient.GetLatestBlockHashAsync();
            if (metadataVersion == MetadataVersion.V4)
            {
                byte[] OmniCreateMintInstruction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(ownerAccount).
                AddInstruction(SystemProgram.CreateAccount(ownerAccount, mintAccount, minBalanceForExemptionMint, TokenProgram.MintAccountDataSize, TokenProgram.ProgramIdKey)).
                AddInstruction(TokenProgram.InitializeMint(mintAccount.PublicKey, 0, ownerAccount.PublicKey, ownerAccount.PublicKey)).
                AddInstruction(AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(ownerAccount.PublicKey, ownerAccount.PublicKey, mintAccount.PublicKey)).
                AddInstruction(NFTmetadata_instruction).
                AddInstruction(mintPNFT).
                Build(Signers);

                var tx = await RpcClient.SendTransactionAsync(OmniCreateMintInstruction);
                return tx;
            }
            else
            {
                byte[] LegacyCreateMintInstruction = new TransactionBuilder().
                    SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                    SetFeePayer(ownerAccount).
                    AddInstruction(SystemProgram.CreateAccount(ownerAccount, mintAccount, minBalanceForExemptionMint, TokenProgram.MintAccountDataSize, TokenProgram.ProgramIdKey)).
                    AddInstruction(TokenProgram.InitializeMint(mintAccount.PublicKey, 0, ownerAccount.PublicKey, ownerAccount.PublicKey)).
                    AddInstruction(AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(ownerAccount.PublicKey, ownerAccount.PublicKey, mintAccount.PublicKey)).
                    AddInstruction(TokenProgram.MintTo(mintAccount.PublicKey, ownerAccount.PublicKey, 1, ownerAccount.PublicKey)).
                    AddInstruction(NFTmetadata_instruction).
                    AddInstruction(MetadataProgram.SignMetadata(new PublicKey((string)metadataAddress), ownerAccount.PublicKey)).
                    Build(Signers);

                var tx = await RpcClient.SendTransactionAsync(LegacyCreateMintInstruction);
                return tx;
            }

        }
        /// <summary>
        /// Omni-Mint Instruction to mint Metaplex metadata tokens of any type
        /// </summary>
        /// <param name="ownerAccount">Owner Account</param>
        /// <param name="mintAccount">Mint Account</param>
        /// <param name="isMasterEdition"> Is master edition?</param>
        /// <param name="mintAmount">Amount that will be minted</param>
        /// <param name="_Authority"> Authority Address - Optional </param>
        /// <param name="_UpdateAuthority">Update Authority Address - Optional</param>
        /// <param name="_payerAddress">Fee payer address - Optional</param>
        /// <param name="delegateAddress">Delegate address - Optional</param>
        /// <param name="delegateRole">Delegate role - Optional</param>
        /// <returns></returns>
        public async Task<RequestResult<string>> Mint(Account ownerAccount, Account mintAccount, bool isMasterEdition, int mintAmount = 1, PublicKey _Authority = null, Account _UpdateAuthority = null, PublicKey _payerAddress = null, PublicKey delegateAddress = null, MetadataDelegateRole delegateRole = MetadataDelegateRole.Update)
        {
            PublicKey Authority = ownerAccount.PublicKey;
            Account UpdateAuthority = ownerAccount;
            PublicKey PayerAddress = ownerAccount.PublicKey;
            PublicKey metadataAddress = PDALookup.FindMetadataPDA(mintAccount);
            PublicKey tokenRecord = PDALookup.FindTokenRecordPDA(ownerAccount, mintAccount);
            PublicKey masterEditionAddress = null;
            PublicKey delegateRecord = null;
            PublicKey rulesetAddress = null;
            List<Account> Signers = new() { ownerAccount, mintAccount };

            //Override default values if specific parameters are supplied
            if (delegateAddress != null)
            {
                delegateRecord = PDALookup.FindDelegateRecordPDA(UpdateAuthority, mintAccount.PublicKey, delegateAddress, delegateRole);
            }
            if (isMasterEdition == true)
            {
                masterEditionAddress = PDALookup.FindMasterEditionPDA(mintAccount.PublicKey);
            }
            if (UpdateAuthority != _UpdateAuthority && _UpdateAuthority != null)
            {
                UpdateAuthority = _UpdateAuthority;

                Signers.Add(UpdateAuthority);
            }
            if (PayerAddress != _payerAddress && _payerAddress != null)
            {
                PayerAddress = _payerAddress;
            }


            TransactionInstruction mintPNFT = MetadataProgram.Mint(AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(ownerAccount, mintAccount), metadataAddress, masterEditionAddress, mintAccount, Authority, PayerAddress, delegateRecord, tokenRecord, ownerAccount, rulesetAddress, mintAmount);
            RequestResult<ResponseValue<LatestBlockHash>> blockHash = await RpcClient.GetLatestBlockHashAsync();

            byte[] OmniMintInstruction = new TransactionBuilder().
            SetRecentBlockHash(blockHash.Result.Value.Blockhash).
            SetFeePayer(ownerAccount).
            AddInstruction(mintPNFT).
            Build(Signers);

            var tx = await RpcClient.SendTransactionAsync(OmniMintInstruction);
            return tx;

        }
    }
}

