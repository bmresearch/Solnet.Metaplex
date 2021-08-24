using Solnet.Programs.Utilities;
using Solnet.Programs;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Buffers.Binary;


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
    /// <summary>
    /// Metadata parameters for instructions
    /// </summary>
    public struct MetadataParameters 
    {
        /// <summary>  Name or discription. Max 32 bytes. </summary>
        public string name;
        /// <summary>  Symbol. Max 10 bytes. </summary>
        public string symbol;
        /// <summary>  Uri. Max 100 bytes. </summary>
        public string uri;
        /// <summary>  Seller fee basis points for secondary sales. </summary>
        public int sellerFeeBasisPoints;
        /// <summary>  List of creators. </summary>
        public List<Creator> creators;
    }


    /// <summary>
    /// Implements the metadata program data encodings.
    /// </summary>
    internal static class MetadataProgramData
    {
        internal const int MethodOffset = 0;

        /// <summary>
        /// Make encodings for CreateMetadataAccount instruction
        /// </summary>
        internal static byte[] EncodeCreateMetadataAccountData (
            MetadataParameters parameters, 
            bool isMutable=true
        )
        {
            byte[] methodBuffer = new byte[679+1];

            byte[] encodedName = Serialization.EncodeRustString(parameters.name);
            byte[] encodedSymbol = Serialization.EncodeRustString(parameters.symbol);
            byte[] encodedUri = Serialization.EncodeRustString(parameters.uri);

            methodBuffer.WriteU8((byte)MetadataProgramInstructions.Values.CreateMetadataAccount, MethodOffset); // 1
            methodBuffer.WriteSpan(encodedName, MetadataProgramLayout.nameOffset); // + 32
            methodBuffer.WriteSpan(encodedSymbol, MetadataProgramLayout.symbolOffset); // + 10
            methodBuffer.WriteSpan(encodedUri, MetadataProgramLayout.uriOffset); // + 200??
            methodBuffer.WriteU8((byte)parameters.sellerFeeBasisPoints, MetadataProgramLayout.feeBasisOffset );
            int counter = 1;
            foreach ( Creator c in parameters.creators)
            {
                byte[] encodedCreator = c.Encode();
                methodBuffer.WriteSpan( encodedCreator , MetadataProgramLayout.creatorsOffset + counter * encodedCreator.Length );
                counter++;
            }

            methodBuffer.WriteU8(Convert.ToByte(isMutable),679);

            return methodBuffer;
        }
        
        /// <summary>
        /// Make encodings for UpdateMetadata instruction
        /// </summary>        
        internal static byte[] EncodeUpdateMetadataData (
            MetadataParameters data, 
            PublicKey newUpdateAuthority, 
            bool primarySaleHappend
        )
        {
            byte[] methodBuffer = new byte[679+32+1];

            byte[] encodedName = Serialization.EncodeRustString(data.name);
            byte[] encodedSymbol = Serialization.EncodeRustString(data.symbol);
            byte[] encodedUri = Serialization.EncodeRustString(data.uri);

            methodBuffer.WriteU8((byte)MetadataProgramInstructions.Values.UpdateMetadataAccount, MethodOffset); // 0
            methodBuffer.WriteSpan(encodedName, MetadataProgramLayout.nameOffset); // + 32
            methodBuffer.WriteSpan(encodedSymbol, MetadataProgramLayout.symbolOffset); // + 10
            methodBuffer.WriteSpan(encodedUri, MetadataProgramLayout.uriOffset); // + 200??
            methodBuffer.WriteU8((byte)data.sellerFeeBasisPoints, MetadataProgramLayout.feeBasisOffset );
            int counter = 1;
            foreach ( Creator c in data.creators)
            {
                byte[] encodedCreator = c.Encode();
                methodBuffer.WriteSpan( encodedCreator , MetadataProgramLayout.creatorsOffset + counter * encodedCreator.Length );
                counter++;
            }

            methodBuffer.WritePubKey(newUpdateAuthority,679);
            methodBuffer.WriteU8(Convert.ToByte(primarySaleHappend),679+32);

            return methodBuffer;
        }


        internal static void DecodeCreateMetadataAccountData( 
            DecodedInstruction decodedInstruction, 
            ReadOnlySpan<byte> data,
            IList<PublicKey> keys, 
            byte[] keyIndices
            )
        {
            decodedInstruction.Values.Add("metadataKey", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("mintKey", keys[keyIndices[1]]);
            decodedInstruction.Values.Add("authorityKey", keys[keyIndices[2]]);
            decodedInstruction.Values.Add("payerKey", keys[keyIndices[3]]);
            decodedInstruction.Values.Add("ProgramIdKey", keys[keyIndices[4]]);
            decodedInstruction.Values.Add("SysVarRentKey", keys[keyIndices[5]]);

            var name = data.DecodeRustString(MetadataProgramLayout.nameOffset);
            var symbol = data.DecodeRustString(MetadataProgramLayout.symbolOffset);
            var uri = data.DecodeRustString(MetadataProgramLayout.uriOffset);

            decodedInstruction.Values.Add("name", name.EncodedString );
            decodedInstruction.Values.Add("symbol", symbol.EncodedString );
            decodedInstruction.Values.Add("uri", uri.EncodedString );
            decodedInstruction.Values.Add("selletFeeBasisPoints", MetadataProgramLayout.feeBasisOffset);

            var creators = DecodeCreators(data.GetSpan(MetadataProgramLayout.creatorsOffset, 4 + 5 * 34));
            decodedInstruction.Values.Add("creators", creators);

            for (int i = 0; i < creators.Count; i++ ){
                decodedInstruction.Values.Add("creator {i} key", creators[i].key.ToString());
                decodedInstruction.Values.Add("creator {i} verified", creators[i].verified.ToString());
                decodedInstruction.Values.Add("creator {i} share", creators[i].share.ToString());
            }

            decodedInstruction.Values.Add("isMutable", data.GetU8(679));

        }

        internal static IList<Creator> DecodeCreators ( ReadOnlySpan<byte> creatorsVector )
        {
            var creators = new List<Creator>();

            //int lenCreatorVector = BinaryPrimitives.ReadUInt32LittleEndian(creatorsVector.Slice(0, sizeof(uint)));
            uint lenCreatorVector = creatorsVector.GetU32(0);

            int offset = 4;
            for (int i = 0; i < lenCreatorVector; i++)
            {
                var creator = new Creator();
                creator.key = creatorsVector.GetPubKey(offset);
                creator.verified = Convert.ToBoolean( creatorsVector.GetU8(offset + 32) );
                creator.share = creatorsVector.GetU32(offset + 32 + 1);
                offset = offset + 34;
                creators.Add(creator);
            }
                
            return creators;
        }

        internal static void DecodeUpdateMetadataAccountData(
            DecodedInstruction decodedInstruction,
            ReadOnlySpan<byte> data,
            IList<PublicKey> keys,
            byte[] keyIndices
            )
        {
            decodedInstruction.Values.Add("metadataKey", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("updateAuthority", keys[keyIndices[1]]);
            decodedInstruction.Values.Add("newUpdateAuthority", keys[keyIndices[2]]);

            var name = data.DecodeRustString(MetadataProgramLayout.nameOffset);
            var symbol = data.DecodeRustString(MetadataProgramLayout.symbolOffset);
            var uri = data.DecodeRustString(MetadataProgramLayout.uriOffset);

            decodedInstruction.Values.Add("name", name.EncodedString );
            decodedInstruction.Values.Add("symbol", symbol.EncodedString );
            decodedInstruction.Values.Add("uri", uri.EncodedString );
            decodedInstruction.Values.Add("selletFeeBasisPoints", MetadataProgramLayout.feeBasisOffset);

            var creators = DecodeCreators(data.GetSpan(MetadataProgramLayout.creatorsOffset, 4 + 5 * 34));
            decodedInstruction.Values.Add("creators", creators);

            for (int i = 0; i < creators.Count; i++ ){
                decodedInstruction.Values.Add("creator {i} key", creators[i].key.ToString());
                decodedInstruction.Values.Add("creator {i} verified", creators[i].verified.ToString());
                decodedInstruction.Values.Add("creator {i} share", creators[i].share.ToString());
            }

            decodedInstruction.Values.Add("newUpdateAuthority", data.GetPubKey(679));
            decodedInstruction.Values.Add("primarySaleHappend", data.GetU8(679 + 32));

        }

    }
}