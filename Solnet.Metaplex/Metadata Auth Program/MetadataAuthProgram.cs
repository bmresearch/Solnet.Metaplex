using Solnet.Programs;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System.Collections.Generic;

namespace Solnet.Metaplex.NFT.Library
{
    /// <summary>
    /// Implements the Metadata Auth program methods.
    /// <remarks>
    /// For more information see:
    /// https://github.com/metaplex-foundation/metaplex
    /// https://metaplex-foundation.github.io/metaplex-program-library/docs/token-metadata/index.html
    /// </remarks>
    /// </summary>
    public static class MetadataAuthProgram
    {
        /// <summary>
        /// The public key of the Metadata Program.
        /// </summary>
        public static readonly PublicKey ProgramIdKey = new("auth9SigNpDKz4sJJ1DfCTuZrZNSAgh9sFD3rboVmgg");

        /// <summary>
        /// The program's name.
        /// </summary>
        private const string ProgramName = "Metadata Ruleset Authorization Program";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="payerKey"></param>
        /// <param name="rulesetPDA"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TransactionInstruction CreateorUpdateRuleset(PublicKey payerKey, PublicKey rulesetPDA, byte[] data)
        {
            List<AccountMeta> _keys = new List<AccountMeta>();

            List<AccountMeta> OmniKeys = new()
            {

                AccountMeta.Writable(payerKey, true),
                AccountMeta.ReadOnly(rulesetPDA, false),
                AccountMeta.ReadOnly(SystemProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly(MetadataAuthProgram.ProgramIdKey, false)
            };

            _keys = OmniKeys;


            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = _keys,
                Data = MetadataAuthProgramData.EncodeCreateorUpdateData(data)
            };
        }

    }
}
