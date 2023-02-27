using System.IO;

namespace Solnet.Metaplex.NFT.Library
{
    /// <summary>
    /// Implements the metadata program data encodings.
    /// </summary>
    internal class MetadataAuthProgramData
    {
        internal static byte[] EncodeCreateorUpdateData(byte[] rulesetData)
        {
            var buffer = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(buffer);
            writer.Write(rulesetData);
            return buffer.ToArray();
        }


    }
}
