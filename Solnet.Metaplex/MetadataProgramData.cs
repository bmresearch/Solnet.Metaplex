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
        public byte share;

        public static int length = 34;

        public Creator( PublicKey key,  byte share , bool verified = false )
        {
            this.key = key;
            this.verified = verified;
            this.share = share;
        }

        public Creator( ReadOnlySpan<byte> encoded )
        {
            this.key = encoded.GetPubKey(0);
            bool verified = Convert.ToBoolean( encoded.GetU8(32) );
            uint share = encoded.GetU8(33);
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
        public uint sellerFeeBasisPoints;
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

            if ( parameters.creators == null || parameters.creators?.Count < 1 )
            {
                writer.Write(new byte[] { 0 }); //Option()
            } else 
            {
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
            int offset = 0;

            decodedInstruction.Values.Add("metadataKey", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("mintKey", keys[keyIndices[1]]);
            decodedInstruction.Values.Add("authorityKey", keys[keyIndices[2]]);
            decodedInstruction.Values.Add("payerKey", keys[keyIndices[3]]);
            decodedInstruction.Values.Add("updateAuthorityKey", keys[keyIndices[4]]);
            decodedInstruction.Values.Add("SysProgramId", keys[keyIndices[5]]);
            decodedInstruction.Values.Add("SysVarRentKey", keys[keyIndices[6]]);
            
            (string name , int nameLength)  = data.DecodeRustString(1);
            (string symbol, int symbolLength) = data.DecodeRustString(1 + nameLength);
            (string uri, int uriLength) = data.DecodeRustString(1 + nameLength+symbolLength);
            int sellerFeeBasisPoints = data.GetU16(1 + nameLength + symbolLength + uriLength);
            
            decodedInstruction.Values.Add("name", name );
            decodedInstruction.Values.Add("symbol", symbol );
            decodedInstruction.Values.Add("uri", uri );
            decodedInstruction.Values.Add("sellerFeeBasisPoints", sellerFeeBasisPoints );

            offset = 1 + nameLength + symbolLength + uriLength + 2;

            if ( data.GetS8( offset)  == 0)
            {
                offset++;
            } else
            {
                offset++;
                int numOfCreators = data.GetS32(offset);
                offset = offset + 4;
                var creators = DecodeCreators(data.GetSpan( offset , numOfCreators * Creator.length));
                decodedInstruction.Values.Add("creators", creators);
                offset = offset + numOfCreators * Creator.length;
            }

            decodedInstruction.Values.Add("isMutable", data.GetU8( data.Length-1 ));
            
        }

        internal static IList<Creator> DecodeCreators ( ReadOnlySpan<byte> creatorsVector )
        {
            var creators = new List<Creator>();

            //int lenCreatorVector = BinaryPrimitives.ReadUInt32LittleEndian(creatorsVector.Slice(0, sizeof(uint)));
            int lenCreatorVector = creatorsVector.Length / Creator.length;

            int offset = 0;
            for (int i = 0; i < lenCreatorVector; i++)
            {             
                var c = new Creator(creatorsVector.GetSpan(offset, Creator.length));
                offset = offset + Creator.length;
                creators.Add(c);
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
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("update authority key", keys[keyIndices[1]]);

            int offset = 1;

            // Option <data>
            if ( data.GetU8(offset) == 1 )
            {
                offset++;
                (string name , int nameLength)  = data.DecodeRustString(offset);
                (string symbol, int symbolLength) = data.DecodeRustString(offset+nameLength);
                (string uri, int uriLength) = data.DecodeRustString(offset+nameLength+symbolLength);
                int sellerFeeBasisPoints = data.GetU16(offset + nameLength + symbolLength + uriLength);

                decodedInstruction.Values.Add("name", name );
                decodedInstruction.Values.Add("symbol", symbol );
                decodedInstruction.Values.Add("uri", uri );
                decodedInstruction.Values.Add("selletFeeBasisPoints", sellerFeeBasisPoints );

                offset = offset + nameLength + symbolLength + uriLength + 2;

                //Option<Creators>
                if ( data.GetS8( offset)  == 0)
                {
                    offset++;
                } else
                {
                    offset++;
                    int numOfCreators = data.GetS32(offset);
                    offset = offset + 4;
                    var creators = DecodeCreators(data.GetSpan( offset , numOfCreators * Creator.length));
                    decodedInstruction.Values.Add("creators", creators);
                    offset = offset + numOfCreators * Creator.length;
                }
            } else {
                offset++;
            }
            // Option<PubKey>
            if (data.GetU8(offset) == 1)
            {
                offset++;
                decodedInstruction.Values.Add("newUpdateAuthority", data.GetPubKey(offset));
            } else 
            {
                offset++;
            }
            // Option<bool>
            if (data.GetU8(offset) == 1)
            {
                offset++;
                decodedInstruction.Values.Add("primarySaleHappend", data.GetU8(offset));
            } 
        }

        internal static void DecodeCreateMasterEdition(
            DecodedInstruction decodedInstruction,
            ReadOnlySpan<byte> data,
            IList<PublicKey> keys,
            byte[] keyIndices
            )
        {
            decodedInstruction.Values.Add("master edition key", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("mint key", keys[keyIndices[1]]);
            decodedInstruction.Values.Add("update authority key", keys[keyIndices[2]]);
            decodedInstruction.Values.Add("mint authority key", keys[keyIndices[3]]);
            decodedInstruction.Values.Add("payer", keys[keyIndices[4]]);
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[5]]);
            decodedInstruction.Values.Add("token program key", keys[keyIndices[6]]);
            decodedInstruction.Values.Add("system program key", keys[keyIndices[7]]);
            decodedInstruction.Values.Add("system program rent key", keys[keyIndices[8]]);

            if ( data.GetU8(0) == 0 )
            {
                return;
            } else 
            {
                decodedInstruction.Values.Add("max supply", data.GetU64(1));
            }
        }

        internal static void DecodeSignMetada(
            DecodedInstruction decodedInstruction,
            ReadOnlySpan<byte> data,
            IList<PublicKey> keys,
            byte[] keyIndices
            )
        {
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("creator key", keys[keyIndices[1]]);
        }

        internal static void DecodePuffMetada(
            DecodedInstruction decodedInstruction,
            ReadOnlySpan<byte> data,
            IList<PublicKey> keys,
            byte[] keyIndices
            )
        {
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[0]]);
        }
        
    }
}