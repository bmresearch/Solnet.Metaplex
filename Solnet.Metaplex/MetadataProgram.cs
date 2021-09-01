using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using Solnet.Wallet.Utilities;
using Solnet.Programs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solnet.Metaplex
{


    /// <summary>
    /// Implements the Metadata program methods.
    /// <remarks>
    /// For more information see:
    /// https://github.com/metaplex-foundation/metaplex
    /// https://www.notion.so/Metaplex-Developer-Guide-afefbc19841744c28587ab948a08cfac
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
        /// Create Metadata object.
        /// </summary>
        /// <param name="metadataKey"> Metadata key (pda of ['metadata', program id, mint id]) </param>
        /// <param name="mintKey"> Mint of token asset </param>
        /// <param name="authorityKey"> Mint authority </param>
        /// <param name="payerKey"> Transaction payer </param>
        /// <param name="updateAuthority"> Metadata update authority </param>
        /// <param name="data"> Metadata struct with name,symbol,uri and optional list of creators </param>
        /// <param name="updateAuthorityIsSigner"> Is the update authority a signer </param>
        /// <param name="isMutable"> Will the account stay mutable.</param>
        /// <returns>The transaction instruction.</returns> 
        public static TransactionInstruction CreateMetadataAccount (
            PublicKey metadataKey, 
            PublicKey mintKey, 
            PublicKey authorityKey, 
            PublicKey payerKey,
            PublicKey updateAuthority, 
            MetadataParameters data,
            bool updateAuthorityIsSigner, 
            bool isMutable
        )
        {

            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(metadataKey, false),
                AccountMeta.ReadOnly(mintKey, false),
                AccountMeta.ReadOnly(authorityKey, true),
                AccountMeta.ReadOnly(payerKey, true),
                AccountMeta.ReadOnly(updateAuthority, updateAuthorityIsSigner ),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(SystemProgram.SysVarRentKey, false)
            };


            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = MetadataProgramData.EncodeCreateMetadataAccountData( data , isMutable )
            };
        }

        ///<summary>
        /// Update metadata account.
        ///</summary>
        public static TransactionInstruction UpdateMetadataAccount (
            PublicKey metadataKey,
            PublicKey updateAuthority,
            PublicKey newUpdateAuthority,
            MetadataParameters data,
            bool primarySaleHappend
        )
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
                Data = MetadataProgramData.EncodeUpdateMetadataData( data, newUpdateAuthority, primarySaleHappend )
            };
        }

        /// <summary>
        /// Sign a piece of metadata that has you as an unverified creator so that it is now verified.
        /// </summary>
        /// <param name="metadataKey"> PDA of ('metadata', program id, mint id) </param>
        /// <param name="creatorKey"> Creator key </param>
        /// <returns></returns>
        public static TransactionInstruction SignMetada (
            PublicKey metadataKey,
            PublicKey creatorKey
        )
        {
            byte[] data = new byte[1];
            data.WriteU8((byte)MetadataProgramInstructions.Values.SignMetadata, 0);

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
        public static TransactionInstruction PuffMetada(
            PublicKey metadataKey
        )
        {
            return new TransactionInstruction()
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = new List<AccountMeta>()
                {
                    AccountMeta.Writable( metadataKey , false )
                },
                Data = new byte[] { (byte) MetadataProgramInstructions.Values.PuffMetadata } 
        };
        }

        public static TransactionInstruction CreateMasterEdition(
            ulong? maxSupply,
            PublicKey masterEditionKey,
            PublicKey mintKey,
            PublicKey updateAuthorityKey,
            PublicKey mintAuthority,
            PublicKey payer,
            PublicKey metadataKey
        )
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
                AccountMeta.ReadOnly(SystemProgram.SysVarRentKey, false)
            };

            return new TransactionInstruction 
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = MetadataProgramData.EncodeCreateMasterEdition( maxSupply )
            };
        }

    }
}
