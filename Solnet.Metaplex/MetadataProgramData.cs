using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;

namespace Solnet.Metaplex
{

    public class Creator 
    {
        public PublicKey key;
        public bool verified;
        public uint share;

        public byte[] Encode() 
        {
            byte[] encodedBuffer = new byte[34];

            encodedBuffer.WritePubKey( key , 0);
            encodedBuffer.WriteU8( Convert.ToByte(verified) , 32 );
            encodedBuffer.WriteU8( (byte)share , 33 );

            return encodedBuffer;
        }
    }

    public struct MetadataParameters 
    {
        public string name;
        public string symbol;
        public string uri;
        public int sellerFeeBasisPoints;
        public List<Creator> creators;
    }


    /// <summary>
    /// Implements the metadata program data encodings.
    /// </summary>
    internal static class MetadataProgramData
    {
        
        internal const int MethodOffset = 0;

        internal static byte[] EncodeCreateMetadataAccountData (
            MetadataParameters parameters, bool isMutable
        )
        {
            byte[] methodBuffer = new byte[679];

            byte[] encodedName = Serialization.EncodeRustString(parameters.name);
            byte[] encodedSymbol = Serialization.EncodeRustString(parameters.symbol);
            byte[] encodedUri = Serialization.EncodeRustString(parameters.uri);

            methodBuffer.WriteU8((byte)MetadataProgramInstructions.Values.CreateMetadataAccount, MethodOffset); // 1
            methodBuffer.WriteSpan(encodedName, MetadataProgramLayout.nameOffset); // + 32
            methodBuffer.WriteSpan(encodedSymbol, MetadataProgramLayout.symbolOffset); // + 10
            methodBuffer.WriteSpan(encodedUri, MetadataProgramLayout.uriOffset); // + 200??
            methodBuffer.WriteU8((byte)parameters.sellerFeeBasisPoints, MetadataProgramLayout.feeBasisOffset );
            int count=1;
            foreach ( Creator c in parameters.creators)
            {
                byte[] encodedCreator = c.Encode();
                methodBuffer.WriteSpan( encodedCreator , MetadataProgramLayout.creatorsOffset + count * encodedCreator.Length );
                count++;
            }

            return methodBuffer;
        }
    }


}