using Solnet.Metaplex.Bubblegum.Types;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Bubblegum.Accounts
{
    public partial class TreeConfig
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 14915960087858115962UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 122, 245, 175, 248, 171, 34, 0, 207 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "MZs54B3fwcz";
            public PublicKey TreeCreator { get; set; }

            public PublicKey TreeDelegate { get; set; }

            public ulong TotalMintCapacity { get; set; }

            public ulong NumMinted { get; set; }

            public bool IsPublic { get; set; }

            public static TreeConfig Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                TreeConfig result = new TreeConfig();
                result.TreeCreator = _data.GetPubKey(offset);
                offset += 32;
                result.TreeDelegate = _data.GetPubKey(offset);
                offset += 32;
                result.TotalMintCapacity = _data.GetU64(offset);
                offset += 8;
                result.NumMinted = _data.GetU64(offset);
                offset += 8;
                result.IsPublic = _data.GetBool(offset);
                offset += 1;
                return result;
            }
        }

        public partial class Voucher
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 4687585125344857279UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 191, 204, 149, 234, 213, 165, 13, 65 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "Z5h9LgqQQwJ";
            public LeafSchema LeafSchema { get; set; }

            public uint Index { get; set; }

            public PublicKey MerkleTree { get; set; }

            public static Voucher Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                Voucher result = new Voucher();
                offset += LeafSchema.Deserialize(_data, offset, out var resultLeafSchema);
                result.LeafSchema = resultLeafSchema;
                result.Index = _data.GetU32(offset);
                offset += 4;
                result.MerkleTree = _data.GetPubKey(offset);
                offset += 32;
                return result;
            }
        }
    }

