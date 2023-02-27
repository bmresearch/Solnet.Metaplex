using Solnet.Metaplex.Utilities;
using Solnet.Programs;
using Solnet.Programs.Utilities;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Collections.Generic;

namespace Solnet.Metaplex.NFT.Library
{
    /// <summary>
    /// Implements the Metadata program methods.
    /// <remarks>
    /// For more information see:
    /// https://github.com/metaplex-foundation/metaplex
    /// https://metaplex-foundation.github.io/metaplex-program-library/docs/token-metadata/index.html
    /// </remarks>
    /// </summary>
    public static class MetadataProgram
    {
        /// <summary>
        /// The public key of the Metadata Program.
        /// </summary>
        public static readonly PublicKey ProgramIdKey = new("metaqbxxUerdq28cj1RbAWkYQm3ybzjb6a8bt518x1s");

        /// <summary>
        /// The program's name.
        /// </summary>
        private const string ProgramName = "Metadata Program";

        /// <summary>
        /// Create Metadata for token mint instruction
        /// </summary>
        /// <param name="metadataKey"> Metadata key (pda of ['metadata', program id, mint id]) </param>
        /// <param name="masterEditionKey"> MasterEdition Address - optional</param>
        /// <param name="mintKey"> Mint of token asset </param>
        /// <param name="authorityKey"> Mint authority </param>
        /// <param name="payerKey"> Transaction payer </param>
        /// <param name="updateAuthority"> Metadata update authority </param>
        /// <param name="data"> Metadata struct with name,symbol,uri and optional list of creators </param>
        /// <param name="updateAuthorityIsSigner"> Is the update authority a signer </param>
        /// <param name="isMutable"> Will the account stay mutable.</param>
        /// <param name="collectionDetails"> Collection details - serial code</param>
        /// <param name="metadataVersion"> Metadata Version - Default is V4 but backport to V1 and V3 is available</param>
        /// <param name="tokenStandard">Token Standard</param>
        /// <param name="maxSupply"> Max supply of the Token</param>

        /// <returns>The transaction instruction.</returns> 
        public static TransactionInstruction CreateMetadataAccount(PublicKey metadataKey, PublicKey mintKey, PublicKey authorityKey, PublicKey payerKey, PublicKey updateAuthority, Metadata data, TokenStandard tokenStandard, bool isMutable, bool updateAuthorityIsSigner, PublicKey masterEditionKey = null, int maxSupply = 0, ulong collectionDetails = 0, MetadataVersion metadataVersion = MetadataVersion.V4)
        {
            List<AccountMeta> _keys = new();

            AccountMeta masterEditionMeta = AccountMeta.ReadOnly(MetadataProgram.ProgramIdKey, false);
            if (masterEditionKey != null)
                masterEditionMeta = AccountMeta.ReadOnly(masterEditionKey, false);

            List<AccountMeta> OmniKeys = new()
            {
                AccountMeta.Writable(metadataKey, false),
                masterEditionMeta,
                AccountMeta.Writable(mintKey, false),
                AccountMeta.ReadOnly(authorityKey, true),
                AccountMeta.ReadOnly(payerKey, true),
                AccountMeta.ReadOnly(updateAuthority, updateAuthorityIsSigner),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(new PublicKey("Sysvar1nstructions1111111111111111111111111"), false),
                AccountMeta.ReadOnly(TokenProgram.ProgramIdKey, false)

            };

            List<AccountMeta> LegacyKeys = new()
            {
                AccountMeta.Writable(metadataKey, false),
                AccountMeta.ReadOnly(mintKey, false),
                AccountMeta.ReadOnly(authorityKey, true),
                AccountMeta.Writable(payerKey, true),
                AccountMeta.ReadOnly(updateAuthority, updateAuthorityIsSigner),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(SysVars.RentKey, false)
            };

            if (metadataVersion == MetadataVersion.V1 || metadataVersion == MetadataVersion.V3)
                _keys = LegacyKeys;

            if (metadataVersion == MetadataVersion.V4)
                _keys = OmniKeys;


            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = _keys,
                Data = MetadataProgramData.EncodeCreateMetadataAccountData(data, tokenStandard, isMutable, collectionDetails, maxSupply, metadataVersion)
            };
        }
        /// <summary>
        /// Omni-Mint Metaplex NFT Instruction - Supports programmable NFTs
        /// </summary>
        /// <param name="metadataKey"> Metadata key (pda of ['metadata', program id, mint id]) </param>
        /// <param name="mintKey"> Mint of token asset </param>
        /// <param name="authorityKey"> Mint authority </param>
        /// <param name="payerKey"> Transaction payer </param>
        /// <param name="associatedTokenAccount"> Metadata struct with name,symbol,uri and optional list of creators </param>
        /// <param name="masterEditionKey"> MasterEdition Address - optional </param>
        /// <param name="tokenOwner"> Token Owner Address - optional - preferred </param>
        /// <param name="delegateRecord"> Delegate Record Account Address  - optional</param>
        /// <param name="tokenRecord"> Token Record Account Address  - optional</param>
        /// <param name="tokenRuleset"> Token Ruleset Account Address - optional</param>
        /// <param name="amount"> Amount of tokens to mint</param>
        /// <returns>The transaction instruction.</returns> 
        public static TransactionInstruction Mint(PublicKey associatedTokenAccount, PublicKey metadataKey, PublicKey masterEditionKey, PublicKey mintKey, PublicKey authorityKey, PublicKey payerKey, PublicKey delegateRecord = null, PublicKey tokenRecord = null, PublicKey tokenRuleset = null, PublicKey tokenOwner = null, int amount = 1)
        {

            AccountMeta tokenOwnerMeta = AccountMeta.ReadOnly(MetadataProgram.ProgramIdKey, false);
            AccountMeta tokenRecordMeta = AccountMeta.ReadOnly(MetadataProgram.ProgramIdKey, false);
            AccountMeta delegateRecordMeta = AccountMeta.ReadOnly(MetadataProgram.ProgramIdKey, false);
            AccountMeta masterEditionMeta = AccountMeta.ReadOnly(MetadataProgram.ProgramIdKey, false);
            AccountMeta tokenRulesetMeta = AccountMeta.ReadOnly(MetadataProgram.ProgramIdKey, false);
            AccountMeta rulesetProgramMeta = AccountMeta.ReadOnly(MetadataProgram.ProgramIdKey, false);

            if (tokenOwner != null)
                tokenOwnerMeta = AccountMeta.ReadOnly(tokenOwner, false);

            if (masterEditionKey != null)
                masterEditionMeta = AccountMeta.ReadOnly(masterEditionKey, false);

            if (tokenRecord != null)
                tokenRecordMeta = AccountMeta.Writable(tokenRecord, false);

            if (delegateRecord != null)
                delegateRecordMeta = AccountMeta.ReadOnly(delegateRecord, false);

            if (tokenRuleset != null)
            {
                tokenRulesetMeta = AccountMeta.ReadOnly(tokenRuleset, false);
                rulesetProgramMeta = AccountMeta.ReadOnly(MetadataAuthProgram.ProgramIdKey, false);
            }
         

            List<AccountMeta> OmniKeys = new()
            {
                AccountMeta.Writable(associatedTokenAccount, false),
                tokenOwnerMeta,
                AccountMeta.ReadOnly(metadataKey, false),
                masterEditionMeta,
                tokenRecordMeta,
                AccountMeta.Writable(mintKey, false),
                AccountMeta.ReadOnly(authorityKey, true),
                delegateRecordMeta,
                AccountMeta.ReadOnly(payerKey, false),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(new PublicKey("Sysvar1nstructions1111111111111111111111111"), false),
                AccountMeta.ReadOnly(TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(AssociatedTokenAccountProgram.ProgramIdKey, false),
                tokenRulesetMeta,
                rulesetProgramMeta,
            };


            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = OmniKeys,
                Data = MetadataProgramData.EncodeOmniMint(amount)
            };
        }
        ///<summary>
        /// Update metadata account.
        ///</summary>
        public static TransactionInstruction UpdateMetadataAccount(PublicKey metadataKey, PublicKey updateAuthority, PublicKey newUpdateAuthority, Metadata data, bool? primarySaleHappend)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(metadataKey, false),
                AccountMeta.ReadOnly(updateAuthority, true)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = MetadataProgramData.EncodeUpdateMetadataData(data, newUpdateAuthority, primarySaleHappend)
            };
        }

        /// <summary>
        /// Sign a piece of metadata that has you as an unverified creator so that it is now verified.
        /// </summary>
        /// <param name="metadataKey"> PDA of ('metadata', program id, mint id) </param>
        /// <param name="creatorKey"> Creator key </param>
        /// <returns></returns>
        public static TransactionInstruction SignMetadata(PublicKey metadataKey, PublicKey creatorKey)
        {
            byte[] data = new byte[1];
            data.WriteU8((byte)InstructionID.SignMetadata, 0);

            return new TransactionInstruction()
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = new List<AccountMeta>()
                {
                    AccountMeta.Writable( metadataKey , false),
                    AccountMeta.ReadOnly( creatorKey, true)
                },
                Data = data
            };
        }

        /// <summary>
        /// Make all of metadata variable length fields (name/uri/symbol) a fixed length
        /// </summary>
        /// <param name="metadataKey"> PDA of ('metadata', program id, mint id) </param>
        /// <returns></returns>
        public static TransactionInstruction PuffMetada(PublicKey metadataKey)
        {
            return new TransactionInstruction()
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = new List<AccountMeta>()
                {
                    AccountMeta.Writable( metadataKey , false )
                },
                Data = new byte[] { (byte)InstructionID.PuffMetadata }
            };
        }

        /// <summary>
        ///  Allows updating the primary sale boolean on Metadata solely through owning an account
        /// containing a token from the metadata's mint and being a signer on this transaction.
        /// A sort of limited authority for limited update capability that is required for things like
        /// Metaplex to work without needing full authority passing.
        /// </summary>
        /// <param name="metadataKey"> Metadata key (pda of ['metadata', program id, mint id]) </param>
        /// <param name="owner"> Owner on the token account </param>
        /// <param name="tokenAccount">  Account containing tokens from the metadata's mint </param>
        /// <returns></returns>
        public static TransactionInstruction UpdatePrimarySaleHappendViaToken(PublicKey metadataKey, PublicKey owner, PublicKey tokenAccount)
        {
            return new TransactionInstruction()
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = new List<AccountMeta>()
                {
                    AccountMeta.Writable(metadataKey, false),
                    AccountMeta.ReadOnly(owner, true),
                    AccountMeta.ReadOnly(tokenAccount, false)
                },
                Data = new byte[] { (byte)InstructionID.UpdatePrimarySaleHappenedViaToken }
            };
        }


        /// <summary>
        ///  Create MasterEdition PDA.
        /// </summary>
        /// <param name="maxSupply"></param>
        /// <param name="masterEditionKey"> PDA of [ 'metadata', program id, mint, 'edition' ]</param>
        /// <param name="mintKey"></param>
        /// <param name="updateAuthorityKey"> </param>
        /// <param name="mintAuthority"> Mint authority on the metadata's mint - THIS WILL TRANSFER AUTHORITY AWAY FROM THIS KEY </param>
        /// <param name="payer"></param>
        /// <param name="metadataKey"></param>
        /// <returns> Transaction instruction. </returns>/
        public static TransactionInstruction CreateMasterEdition(ulong? maxSupply, PublicKey masterEditionKey, PublicKey mintKey, PublicKey updateAuthorityKey, PublicKey mintAuthority, PublicKey payer, PublicKey metadataKey)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(masterEditionKey, false),
                AccountMeta.Writable(mintKey, false),
                AccountMeta.ReadOnly(updateAuthorityKey, true),
                AccountMeta.ReadOnly(mintAuthority, true),
                AccountMeta.ReadOnly(payer, true),
                AccountMeta.ReadOnly(metadataKey, false),
                AccountMeta.ReadOnly(TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = MetadataProgramData.EncodeCreateMasterEdition(maxSupply)
            };
        }

        /// <summary>
        ///  Mint a new edition via token
        /// </summary>
        /// <param name="edition"></param>
        /// <param name="newMetadataKey"></param>
        /// <param name="newEdition"></param>
        /// <param name="masterEdition"></param>
        /// <param name="newMint"></param>
        /// <param name="newMintAuthority"></param>
        /// <param name="payer"></param>
        /// <param name="tokenAccountOwner"></param>
        /// <param name="tokenAccount"></param>
        /// <param name="updateAuthority"></param>
        /// <param name="metadataKey"></param>
        /// <param name="metadataMint"></param>
        /// <returns></returns>
        public static TransactionInstruction MintNewEditionFromMasterEditionViaToken(
            uint edition,
            PublicKey newMetadataKey,
            PublicKey newEdition,
            PublicKey masterEdition,
            PublicKey newMint,
            PublicKey newMintAuthority,
            PublicKey payer,
            PublicKey tokenAccountOwner,
            PublicKey tokenAccount,
            PublicKey updateAuthority,
            PublicKey metadataKey,
            PublicKey metadataMint
        )
        {
            PublicKey editionPda = PDALookup.FindEditionMarkerPDA(metadataMint);


            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(newMetadataKey, false),
                AccountMeta.Writable(newEdition, false),
                AccountMeta.Writable(masterEdition, false),
                AccountMeta.Writable(newMint, false),
                AccountMeta.Writable(editionPda, false),

                AccountMeta.ReadOnly(newMintAuthority, true),
                AccountMeta.ReadOnly(payer, true),
                AccountMeta.ReadOnly(tokenAccountOwner, true),

                AccountMeta.ReadOnly(tokenAccount, false),
                AccountMeta.ReadOnly(updateAuthority, false),
                AccountMeta.ReadOnly(metadataKey, false),

                AccountMeta.ReadOnly(TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = MetadataProgramData.EncodeMintNewEditionFromMasterEditionViaToken(edition)
            };
        }

        /// <summary>
        /// Decodes an instruction created by the Metadata Program.
        /// </summary>
        /// <param name="data">The instruction data to decode.</param>
        /// <param name="keys">The account keys present in the transaction.</param>
        /// <param name="keyIndices">The indices of the account keys for the instruction as they appear in the transaction.</param>
        /// <returns>A decoded instruction.</returns>
        public static DecodedInstruction Decode(ReadOnlySpan<byte> data, IList<PublicKey> keys, byte[] keyIndices)
        {
            uint instruction = data.GetU8(MetadataProgramData.MethodOffset);
            InstructionID instructionValue =
                (InstructionID)Enum.Parse(typeof(InstructionID), instruction.ToString());

            DecodedInstruction decodedInstruction = new()
            {
                PublicKey = ProgramIdKey,
                InstructionName = MetadataInstructionBook.Names[instructionValue],
                ProgramName = ProgramName,
                Values = new Dictionary<string, object>(),
                InnerInstructions = new List<DecodedInstruction>()
            };

            switch (instructionValue)
            {
                case InstructionID.CreateMetadataAccount:
                    MetadataProgramData.DecodeCreateMetadataAccountData(decodedInstruction, data, keys, keyIndices);
                    break;
                case InstructionID.CreateMetadataAccountV3:
                    MetadataProgramData.DecodeCreateMetadataAccountData(decodedInstruction, data, keys, keyIndices);
                    break;
                case InstructionID.UpdateMetadataAccount:
                    MetadataProgramData.DecodeUpdateMetadataAccountData(decodedInstruction, data, keys, keyIndices);
                    break;
                case InstructionID.CreateMasterEdition:
                    MetadataProgramData.DecodeCreateMasterEdition(decodedInstruction, data, keys, keyIndices);
                    break;
                case InstructionID.PuffMetadata:
                    MetadataProgramData.DecodePuffMetada(decodedInstruction, keys, keyIndices);
                    break;
                case InstructionID.SignMetadata:
                    MetadataProgramData.DecodeSignMetada(decodedInstruction, keys, keyIndices);
                    break;
                case InstructionID.UpdatePrimarySaleHappenedViaToken:
                    MetadataProgramData.DecodeUpdatePrimarySaleHappendViaToken(decodedInstruction, keys, keyIndices);
                    break;
                case InstructionID.MintNewEditionFromMasterEditionViaToken:
                    MetadataProgramData.DecodeMintNewEditionFromMasterEditionViaToken(decodedInstruction, data, keys, keyIndices);
                    break;
            }

            return decodedInstruction;
        }

    } //class
} //namespace
