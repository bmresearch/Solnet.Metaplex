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


    public class Creator 
    {
        public PublicKey key;
        public bool verified;
        public uint share;

        public Creator( PublicKey key,  uint share , bool verified = false )
        {
            this.key = key;
            this.verified = verified;
            this.share = share;
        }
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

        public static void PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("\nnew byte[] { ");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}\n");
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Make encodings for CreateMetadataAccount instruction
        /// </summary>
        internal static byte[] EncodeCreateMetadataAccountData (
            MetadataParameters parameters, 
            bool isMutable=true
        )
        {

            byte[] encodedName = Serialization.EncodeRustString(parameters.name);
            byte[] encodedSymbol = Serialization.EncodeRustString(parameters.symbol);
            byte[] encodedUri = Serialization.EncodeRustString(parameters.uri);

            var buffer = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(buffer);

            writer.Write( (byte) MetadataProgramInstructions.Values.CreateMetadataAccount );
            writer.Write( encodedName) ;
            writer.Write( encodedSymbol );
            writer.Write( encodedUri );
            writer.Write( (ushort) parameters.sellerFeeBasisPoints);

            if ( parameters.creators == null || parameters.creators?.Count < 1 ){
                writer.Write(new byte[] { 0 }); //Option()
            } else {
                writer.Write((byte)1);
                writer.Write( parameters.creators.Count );
                foreach ( Creator c in parameters.creators )
                {
                    byte[] encodedCreator = c.Encode();
                    writer.Write( encodedCreator );
                }
            }            

            writer.Write(isMutable);

            return buffer.ToArray();
        }
        
        /// <summary>
        /// Make encodings for UpdateMetadata instruction
        /// </summary>        
        internal static byte[] EncodeUpdateMetadataData (
            MetadataParameters parameters, 
            PublicKey newUpdateAuthority, 
            bool primarySaleHappend
        )
        {
            byte[] encodedName = Serialization.EncodeRustString(parameters.name);
            byte[] encodedSymbol = Serialization.EncodeRustString(parameters.symbol);
            byte[] encodedUri = Serialization.EncodeRustString(parameters.uri);

            var buffer = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(buffer);

            writer.Write( (byte) MetadataProgramInstructions.Values.CreateMetadataAccount );
            writer.Write( encodedName) ;
            writer.Write( encodedSymbol );
            writer.Write( encodedUri );
            writer.Write( (ushort) parameters.sellerFeeBasisPoints);

            if ( parameters.creators == null || parameters.creators?.Count < 1 ){
                writer.Write(new byte[] { 0 }); //Option()
            } else {
                writer.Write((byte)1);
                writer.Write( parameters.creators.Count );
                foreach ( Creator c in parameters.creators )
                {
                    byte[] encodedCreator = c.Encode();
                    writer.Write( encodedCreator );
                }
            }

            writer.Write(newUpdateAuthority.KeyBytes);
            writer.Write(primarySaleHappend);

            return buffer.ToArray();
        }

        /// <summary>
        /// Make encodings for CreateMasterEdition instruction
        /// </summary> 
        public static byte[] EncodeCreateMasterEdition( ulong? maxSupply )
        {
            var buffer = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(buffer);

            writer.Write((byte)MetadataProgramInstructions.Values.CreateMasterEdition);

            if ( maxSupply == null ){
                writer.Write(new byte[] { 0 }); //Option<>
            } else {
                writer.Write((byte)1); //Option<u64>
                writer.Write((ulong) maxSupply);
            }

            PrintByteArray(buffer.ToArray());

            return buffer.ToArray();
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
                
                PublicKey key = creatorsVector.GetPubKey(offset);
                bool verified = Convert.ToBoolean( creatorsVector.GetU8(offset + 32) );
                uint share = creatorsVector.GetU32(offset + 32 + 1);
                offset = offset + 34;
                creators.Add( new Creator( key, share , verified ));
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