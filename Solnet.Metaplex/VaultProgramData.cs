using Solnet.Programs.Utilities;
using Solnet.Programs;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Buffers.Binary;
using System.IO;
using System.Text;


namespace Solnet.Metaplex
{
    public class VaultProgramData
    {
        internal static byte[] EncodeAddTokenToInactiveVault( UInt64 amount )
        {
            var buffer = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(buffer);

            writer.Write( (byte) VaultProgramInstructions.Values.AddTokenToInactiveVault );
            writer.Write( amount );

            return buffer.ToArray();
        }
    }
}