using Solnet.Metaplex.NFT.Library;
using Solnet.Programs;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solnet.Metaplex.Utilities
{
    /// <summary>
    /// PDA Lookup Class to make finding PDAs simple
    /// </summary>
    public static class PDALookup
    {
        /// <summary>
        /// Find the Metadata PDA from the mint address
        /// </summary>
        /// <param name="mintAddress"></param>
        /// <returns></returns>
        public static PublicKey FindMetadataPDA(PublicKey mintAddress)
        {
            PublicKey.TryFindProgramAddress(new List<byte[]>()
            {
                Encoding.UTF8.GetBytes("metadata"),
                MetadataProgram.ProgramIdKey,
                mintAddress
            },
            MetadataProgram.ProgramIdKey,
            out PublicKey metadataAddress,
            out _);

            return metadataAddress;
        }
        /// <summary>
        /// Find the master edition PDA from a mint address
        /// </summary>
        /// <param name="mintAddress"></param>
        /// <returns></returns>
        public static PublicKey FindMasterEditionPDA(PublicKey mintAddress)
        {
            PublicKey.TryFindProgramAddress(
            new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mintAddress,
                    Encoding.UTF8.GetBytes("edition")
            },
            MetadataProgram.ProgramIdKey,
            out PublicKey masterEditionAddress,
            out _);
            return masterEditionAddress;
        }
        /// <summary>
        /// Find the Edition Marker PDA to keep track of which editions were minted
        /// </summary>
        /// <param name="_EditionAddress"></param>
        /// <returns></returns>
        public static PublicKey FindEditionMarkerPDA(PublicKey _EditionAddress)
        {
            int editionNumber = (int)Math.Floor((double)1 / 248);

            PublicKey.TryFindProgramAddress(
            new List<byte[]>()
            {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    _EditionAddress,
                    Encoding.UTF8.GetBytes("edition"),
                    Encoding.UTF8.GetBytes(editionNumber.ToString())

            },
            MetadataProgram.ProgramIdKey,
            out PublicKey editionPda,
            out _);
            return editionPda;
        }
        /// <summary>
        /// Find the Token Record PDA if nothing is found the program address is returned
        /// </summary>
        /// <param name="ownerAccount"></param>
        /// <param name="mintAddress"></param>
        /// <returns></returns>
        public static PublicKey FindTokenRecordPDA(PublicKey ownerAccount, PublicKey mintAddress)
        {
            bool foundRecord = PublicKey.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mintAddress,
                    Encoding.UTF8.GetBytes("token_record"),
                    AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(ownerAccount, mintAddress)
                },
                MetadataProgram.ProgramIdKey,
                out PublicKey tokenRecord,
               out _);
            if (foundRecord == false)
            {
                tokenRecord = MetadataProgram.ProgramIdKey;
            }
            return tokenRecord;
        }
        /// <summary>
        /// Find the Delegate Record PDA if nothing is found the program address is returned
        /// </summary>
        /// <param name="updateAuthority"></param>
        /// <param name="mintAddress"></param>
        /// <param name="delegateAddress"></param>
        /// <param name="delegateRole"></param>
        /// <returns></returns>
        public static PublicKey FindDelegateRecordPDA(PublicKey updateAuthority, PublicKey mintAddress, PublicKey delegateAddress, MetadataDelegateRole delegateRole)
        {
            bool foundRecord = PublicKey.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mintAddress,
                    new byte[1]{ (byte)delegateRole },
                    updateAuthority,
                    delegateAddress
                },
                MetadataProgram.ProgramIdKey,
                out PublicKey delegateRecord,
                out _);

            if (foundRecord == false)
            {
                delegateRecord = MetadataProgram.ProgramIdKey;
            }
            return delegateRecord;
        }
        /// <summary>
        /// Find the collection authority PDA using the mint address and collection authority address
        /// </summary>
        /// <param name="mintAddress"></param>
        /// <param name="collectionAuthority"></param>
        /// <returns></returns>
        public static PublicKey FindCollectionAuthRecordPDA(PublicKey mintAddress, PublicKey collectionAuthority)
        {
            PublicKey.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mintAddress,
                    Encoding.UTF8.GetBytes("collection_authority"),
                    collectionAuthority
                },
                MetadataProgram.ProgramIdKey,
                out PublicKey collectionAuthRecord,
                out _
            );

            return collectionAuthRecord;
        }
        /// <summary>
        /// Find Use Authority Record PDA
        /// </summary>
        /// <param name="mintAddress"></param>
        /// <param name="useAuthority"></param>
        /// <returns></returns>
        public static PublicKey FindUseAuthorityRecordPDA(PublicKey mintAddress, PublicKey useAuthority)
        {
            PublicKey.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mintAddress,
                    Encoding.UTF8.GetBytes("user"),
                    useAuthority
                },
                MetadataProgram.ProgramIdKey,
                out PublicKey useAuthRecord,
                out _
            );

            return useAuthRecord;
        }
        /// <summary>
        /// Find RuleSet PDA using Auth Program and ruleset name
        /// </summary>
        /// <param name="payer"></param>
        /// <param name="rulesetName"></param>
        /// <returns></returns>
        public static PublicKey FindRulesetPDA(PublicKey payer, string rulesetName)
        {
            PublicKey rulesetAccount;
            byte nonce;

            PublicKey.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("rule_set"),
                    payer,
                    Encoding.UTF8.GetBytes(rulesetName)
                },
                MetadataAuthProgram.ProgramIdKey,
                out rulesetAccount,
                out nonce
            );

            return rulesetAccount;
        }
        /// <summary>
        /// Find Vault PDA using vault address and mint address
        /// </summary>
        /// <param name="VaultAddress"></param>
        /// <param name="MintAddress"></param>
        /// <returns></returns>
        public static PublicKey FindVaultPDA(PublicKey VaultAddress, PublicKey MintAddress)
        {
            PublicKey.TryFindProgramAddress(
                   new List<byte[]>() {
                        Encoding.UTF8.GetBytes("vault"),
                        VaultAddress,
                        MintAddress
                   },
                   VaultProgram.ProgramIdKey,
                   out PublicKey VaultPDA,
                   out _
               );

            return VaultPDA;
        }
    }
}
