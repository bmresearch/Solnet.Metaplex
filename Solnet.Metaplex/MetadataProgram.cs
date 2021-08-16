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
    public static class TokenProgram
        {
            /// <summary>
            /// The public key of the Metadata Program.
            /// </summary>
            public static readonly PublicKey ProgramIdKey = new("metaqbxxUerdq28cj1RbAWkYQm3ybzjb6a8bt518x1s");

            /// <summary>
            /// The program's name.
            /// </summary>
            private const string ProgramName = "Metadata Program";

}