using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using Solnet.Wallet.Utilities;
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
        /// <param name="authority"> Mint authority </param>
        /// <param name="payer"> Transaction payer </param>
        /// <param name="dataParameters"> Metadata struct with name,symbol,uri and optional list of creators </param>
        /// <param name="isMutable"> Will the account stay mutable.</param>
        /// <returns>The transaction instruction.</returns> 
        public static TransactionInstruction CreateMetadataAccount (
            PublicKey metadataKey, PublicKey mintKey, PublicKey authority, PublicKey payer, 
            MetadataParameters dataParameters, bool isMutable
        )
        {

            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(metadataKey, false),
                AccountMeta.ReadOnly(mintKey, false),
                AccountMeta.ReadOnly(authority, true),
                AccountMeta.ReadOnly(payer, true),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(SystemProgram.SysVarRentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = MetadaProgramData.EncodeCreateMetadataAccountData( dataParameters , isMutable )
            };
        }
    }
}
