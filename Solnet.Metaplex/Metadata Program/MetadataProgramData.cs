using Solnet.Programs;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Solnet.Metaplex.NFT.Library
{
    /// <summary>
    /// Implements the metadata program data encodings.
    /// </summary>
    internal static class MetadataProgramData
    {
        internal const int MethodOffset = 0;


        /// <summary>
        /// Make encodings for CreateMetadataAccount instruction
        /// </summary>
        internal static byte[] EncodeCreateMetadataAccountData(Metadata parameters, TokenStandard tokenStandard, bool isMutable = true, ulong collectionDetails = 0, int maxSupply = 0, MetadataVersion metadataVersion = MetadataVersion.V4)
        {

            byte[] encodedName = Encoding.UTF8.GetBytes(parameters.name);
            byte[] encodedSymbol = Encoding.UTF8.GetBytes(parameters.symbol);
            byte[] encodedUri = Encoding.UTF8.GetBytes(parameters.uri);

            var buffer = new MemoryStream();
            BinaryWriter writer = new(buffer);
            //Omni Create Instruction ID
            byte decriminator = (byte)InstructionID.OmniCreate;

            //Legacy Create Instruction ID
            if (metadataVersion == MetadataVersion.V1)
                decriminator = (byte)InstructionID.CreateMetadataAccount;

            //V3 Create Instruction ID
            if (metadataVersion == MetadataVersion.V3)
                decriminator = (byte)InstructionID.CreateMetadataAccountV3;

            writer.Write((byte)decriminator);

            if (metadataVersion == MetadataVersion.V4)
            {
                if (maxSupply == 0)
                    writer.Write((byte)0);
                else
                {
                    writer.Write((byte)1);
                    writer.Write((byte)maxSupply);
                }
            }

            writer.Write(encodedName.Length);
            writer.Write(encodedName);
            writer.Write(encodedSymbol.Length);
            writer.Write(encodedSymbol);
            writer.Write(encodedUri.Length);
            writer.Write(encodedUri);
            writer.Write((ushort)parameters.sellerFeeBasisPoints);

            if (parameters.creators == null || parameters.creators?.Count < 1)
            {
                writer.Write((byte)0); // 0 = null | No Creators
            }
            else
            {
                writer.Write((byte)1);
                writer.Write(parameters.creators.Count);
                foreach (Creator c in parameters.creators)
                {
                    byte[] encodedCreator = c.Encode();
                    writer.Write(encodedCreator);
                }
            }
            if (metadataVersion == MetadataVersion.V1)
                writer.Write(isMutable);


            if (metadataVersion == MetadataVersion.V4)
            {
                writer.Write((byte)0);
                writer.Write(isMutable);
                writer.Write((byte)tokenStandard);
            }
            if (metadataVersion == MetadataVersion.V4 || metadataVersion == MetadataVersion.V3)
            {
                if (parameters.collection == null)
                {
                    writer.Write((byte)0); // 0 = null | No Collection link
                }
                else
                {
                    writer.Write((byte)1);
                    writer.Write(parameters.collection.Encode());
                }
                if (parameters.uses == null)
                {
                    writer.Write((byte)0); // 0 = null | Not consumable
                }
                else
                {
                    writer.Write((byte)1);
                    writer.Write((byte)parameters.uses.useMethod);
                    writer.Write((ulong)Convert.ToUInt64(parameters.uses.remaining));
                    writer.Write((ulong)Convert.ToUInt64(parameters.uses.total));

                }
            }
            if (metadataVersion == MetadataVersion.V3)
            {
                writer.Write(isMutable);
            }

            if (collectionDetails == 0)
            {
                writer.Write((byte)0); // 0 = null | No collection details
            }
            else
            {
                writer.Write((byte)1);
                writer.Write((byte)0);// collection detail size sits in an array - this byte defines its position | should always be 0
                writer.Write((ulong)collectionDetails); //collection details size of V1 

            }
            if (metadataVersion == MetadataVersion.V4)
            {
                if (parameters.programmableConfig == null)
                {
                    writer.Write((byte)0);

                }
                else
                {
                    writer.Write((byte)1);
                    writer.Write(parameters.programmableConfig.Encode());
                }

                writer.Write((byte)1);
                writer.Write((byte)0);
                writer.Write((byte)1);
                writer.Write((byte)0);
            }

            return buffer.ToArray();
        }
        internal static byte[] EncodeOmniMint(int amount)
        {
            var buffer = new MemoryStream();
            BinaryWriter writer = new(buffer);
            writer.Write((byte)InstructionID.OmniMint);
            writer.Write((byte)0);
            writer.Write((ulong)Convert.ToUInt64(amount));
            writer.Write((byte)0);

            return buffer.ToArray();
        }
        /// <summary>
        /// Make encodings for UpdateMetadata instruction
        /// </summary>        
        internal static byte[] EncodeUpdateMetadataData(Metadata parameters = null, PublicKey newUpdateAuthority = null, bool? primarySaleHappend = null, MetadataVersion metadataVersion = MetadataVersion.V4)
        {
            var buffer = new MemoryStream();
            BinaryWriter writer = new(buffer);

            writer.Write((byte)InstructionID.UpdateMetadataAccount);

            if (parameters is not null)
            {
                writer.Write((byte)1);

                byte[] encodedName = Encoding.UTF8.GetBytes(parameters.name);
                byte[] encodedSymbol = Encoding.UTF8.GetBytes(parameters.symbol);
                byte[] encodedUri = Encoding.UTF8.GetBytes(parameters.uri);
                byte decriminator;
                if (metadataVersion == MetadataVersion.V3 || metadataVersion == MetadataVersion.V4)
                {
                    decriminator = 15;
                }
                else
                {
                    decriminator = 1;
                }

                writer.Write(decriminator);
                writer.Write(encodedName.Length);
                writer.Write(encodedName);
                writer.Write(encodedSymbol.Length);
                writer.Write(encodedSymbol);
                writer.Write(encodedUri.Length);
                writer.Write(encodedUri);

                writer.Write((ushort)parameters.sellerFeeBasisPoints);

                if (parameters.creators == null || parameters.creators?.Count < 1)
                {
                    writer.Write((byte)0); //Option()
                }
                else
                {
                    writer.Write((byte)1);
                    writer.Write(parameters.creators.Count);
                    foreach (Creator c in parameters.creators)
                    {
                        byte[] encodedCreator = c.Encode();
                        writer.Write(encodedCreator);
                    }
                }
                if (metadataVersion == MetadataVersion.V3 || metadataVersion == MetadataVersion.V4)
                {
                    if (parameters.collection == null)
                    {
                        writer.Write((byte)0); // 0 = null | No Collection link
                    }
                    else
                    {
                        writer.Write((byte)1);
                        writer.Write(parameters.collection.Encode());
                    }
                    if (parameters.uses == null)
                    {
                        writer.Write((byte)0); // 0 = null | Not consumable
                    }
                    else
                    {
                        writer.Write((byte)1);
                        writer.Write((byte)parameters.uses.useMethod);
                        writer.Write((ulong)Convert.ToUInt64(parameters.uses.remaining));
                        writer.Write((ulong)Convert.ToUInt64(parameters.uses.total));

                    }

                    if (metadataVersion == MetadataVersion.V4)
                    {
                        if (parameters.programmableConfig == null)
                        {
                            writer.Write((byte)0);
                        }
                        else
                        {
                            writer.Write((byte)1);
                            writer.Write(parameters.programmableConfig.Encode());
                        }
                    }
                }

            }
            else
            {
                writer.Write((byte)0);
            }

            if (newUpdateAuthority is not null)
            {
                writer.Write((byte)1);
                writer.Write(newUpdateAuthority.KeyBytes.AsSpan());
            }
            else
            {
                writer.Write((byte)0);
            }

            if (primarySaleHappend is not null)
            {
                writer.Write((byte)1);
                writer.Write(primarySaleHappend.Value);
            }
            else
            {
                writer.Write((byte)0);
            }

            return buffer.ToArray();
        }

        /// <summary>
        /// Make encodings for CreateMasterEdition instruction
        /// </summary> 
        public static byte[] EncodeCreateMasterEdition(ulong? maxSupply)
        {
            var buffer = new MemoryStream();
            BinaryWriter writer = new(buffer);

            writer.Write((byte)InstructionID.CreateMasterEdition);

            if (maxSupply == null)
            {
                writer.Write(new byte[] { 0 }); //Option<>
            }
            else
            {
                writer.Write((byte)1); //Option<u64>
                writer.Write((ulong)maxSupply);
            }

            return buffer.ToArray();
        }

        public static byte[] EncodeMintNewEditionFromMasterEditionViaToken(ulong edition) //u64
        {
            var buffer = new MemoryStream();
            BinaryWriter writer = new(buffer);

            writer.Write((byte)InstructionID.MintNewEditionFromMasterEditionViaToken);
            writer.Write(edition);

            return buffer.ToArray();
        }


        internal static void DecodeCreateMetadataAccountData(DecodedInstruction decodedInstruction, ReadOnlySpan<byte> data, IList<PublicKey> keys, byte[] keyIndices)
        {


            decodedInstruction.Values.Add("metadataKey", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("mintKey", keys[keyIndices[1]]);
            decodedInstruction.Values.Add("authorityKey", keys[keyIndices[2]]);
            decodedInstruction.Values.Add("payerKey", keys[keyIndices[3]]);
            decodedInstruction.Values.Add("updateAuthorityKey", keys[keyIndices[4]]);
            decodedInstruction.Values.Add("SysProgramId", keys[keyIndices[5]]);
            decodedInstruction.Values.Add("SysVarRentKey", keys[keyIndices[6]]);


            int nameLength = data.GetBorshString(1, out string name);
            int symbolLength = data.GetBorshString(1 + nameLength, out string symbol);
            int uriLength = data.GetBorshString(1 + nameLength + symbolLength, out string uri);

            int sellerFeeBasisPoints = data.GetU16(1 + nameLength + symbolLength + uriLength);

            decodedInstruction.Values.Add("name", name);
            decodedInstruction.Values.Add("symbol", symbol);
            decodedInstruction.Values.Add("uri", uri);
            decodedInstruction.Values.Add("sellerFeeBasisPoints", sellerFeeBasisPoints);

            int offset = 1 + nameLength + symbolLength + uriLength + 2;

            if (data.GetS8(offset) == 0)
            {
                decodedInstruction.Values.Add("creators", null);
            }
            else
            {
                offset++;
                int numOfCreators = data.GetS32(offset);
                offset += 4;
                var creators = DecodeCreators(data.GetSpan(offset, numOfCreators * Creator.length));
                decodedInstruction.Values.Add("creators", creators);

            }

            decodedInstruction.Values.Add("isMutable", data.GetU8(data.Length - 1));

        }

        internal static IList<Creator> DecodeCreators(ReadOnlySpan<byte> creatorsVector)
        {
            var creators = new List<Creator>();

            //int lenCreatorVector = BinaryPrimitives.ReadUInt32LittleEndian(creatorsVector.Slice(0, sizeof(uint)));
            int lenCreatorVector = creatorsVector.Length / Creator.length;

            int offset = 0;
            for (int i = 0; i < lenCreatorVector; i++)
            {
                var c = new Creator(creatorsVector.GetSpan(offset, Creator.length));
                offset += Creator.length;
                creators.Add(c);
            }

            return creators;
        }

        internal static void DecodeUpdateMetadataAccountData(DecodedInstruction decodedInstruction, ReadOnlySpan<byte> data, IList<PublicKey> keys, byte[] keyIndices)
        {
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("update authority key", keys[keyIndices[1]]);

            int offset = 1;

            // Option <data>
            if (data.GetU8(offset) == 1)
            {
                offset++;


                int nameLength = data.GetBorshString(offset, out string name);
                int symbolLength = data.GetBorshString(offset + nameLength, out string symbol);
                int uriLength = data.GetBorshString(offset + nameLength + symbolLength, out string uri);

                int sellerFeeBasisPoints = data.GetU16(offset + nameLength + symbolLength + uriLength);

                decodedInstruction.Values.Add("name", name);
                decodedInstruction.Values.Add("symbol", symbol);
                decodedInstruction.Values.Add("uri", uri);
                decodedInstruction.Values.Add("selletFeeBasisPoints", sellerFeeBasisPoints);

                offset = offset + nameLength + symbolLength + uriLength + 2;

                //Option<Creators>
                if (data.GetS8(offset) == 0)
                {
                    offset++;
                }
                else
                {
                    offset++;
                    int numOfCreators = data.GetS32(offset);
                    offset += 4;
                    var creators = DecodeCreators(data.GetSpan(offset, numOfCreators * Creator.length));
                    decodedInstruction.Values.Add("creators", creators);
                    offset += numOfCreators * Creator.length;
                }
            }
            else
            {
                offset++;
            }
            // Option<PubKey>
            if (data.GetU8(offset) == 1)
            {
                offset++;
                decodedInstruction.Values.Add("newUpdateAuthority", data.GetPubKey(offset));
            }
            else
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

        internal static void DecodeCreateMasterEdition(DecodedInstruction decodedInstruction, ReadOnlySpan<byte> data, IList<PublicKey> keys, byte[] keyIndices)
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

            if (data.GetU8(0) == 0)
            {
                return;
            }
            else
            {
                decodedInstruction.Values.Add("max supply", data.GetU64(1));
            }
        }

        internal static void DecodeSignMetada(DecodedInstruction decodedInstruction, IList<PublicKey> keys, byte[] keyIndices)
        {
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("creator key", keys[keyIndices[1]]);
        }

        internal static void DecodePuffMetada(DecodedInstruction decodedInstruction, IList<PublicKey> keys, byte[] keyIndices)
        {
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[0]]);
        }

        internal static void DecodeUpdatePrimarySaleHappendViaToken(DecodedInstruction decodedInstruction, IList<PublicKey> keys, byte[] keyIndices)
        {
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("owner key", keys[keyIndices[1]]);
            decodedInstruction.Values.Add("token account key", keys[keyIndices[2]]);
        }

        internal static void DecodeMintNewEditionFromMasterEditionViaToken(DecodedInstruction decodedInstruction, ReadOnlySpan<byte> data, IList<PublicKey> keys, byte[] keyIndices)
        {
            decodedInstruction.Values.Add("new metadata key", keys[keyIndices[0]]);
            decodedInstruction.Values.Add("new edition", keys[keyIndices[1]]);
            decodedInstruction.Values.Add("master edition", keys[keyIndices[2]]);
            decodedInstruction.Values.Add("new mint", keys[keyIndices[3]]);
            decodedInstruction.Values.Add("edition PDA", keys[keyIndices[4]]);
            decodedInstruction.Values.Add("new mint authority", keys[keyIndices[5]]);
            decodedInstruction.Values.Add("payer", keys[keyIndices[6]]);
            decodedInstruction.Values.Add("token account owner", keys[keyIndices[7]]);
            decodedInstruction.Values.Add("token account", keys[keyIndices[8]]);
            decodedInstruction.Values.Add("update authority", keys[keyIndices[8]]);
            decodedInstruction.Values.Add("master edition", keys[keyIndices[9]]);
            decodedInstruction.Values.Add("metadata key", keys[keyIndices[10]]);
            decodedInstruction.Values.Add("token program id", keys[keyIndices[11]]);
            decodedInstruction.Values.Add("system program id", keys[keyIndices[12]]);
            decodedInstruction.Values.Add("system program rent", keys[keyIndices[13]]);

            decodedInstruction.Values.Add("edition number", data.GetU64(1));
        }

    }
}