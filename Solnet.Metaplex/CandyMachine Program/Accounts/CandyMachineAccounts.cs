using Solnet.Metaplex.Candymachine.Types;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine.Accounts
{
    public partial class CandyMachine
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 13649831137213787443UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 51, 173, 177, 113, 25, 241, 109, 189 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "9eM5CfcKCCt";
            public PublicKey Authority { get; set; }

            public PublicKey Wallet { get; set; }

            public PublicKey TokenMint { get; set; }

            public ulong ItemsRedeemed { get; set; }

            public CandyMachineData Data { get; set; }

            public static CandyMachine Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                CandyMachine result = new CandyMachine();
                result.Authority = _data.GetPubKey(offset);
                offset += 32;
                result.Wallet = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.TokenMint = _data.GetPubKey(offset);
                    offset += 32;
                }

                result.ItemsRedeemed = _data.GetU64(offset);
                offset += 8;
                offset += CandyMachineData.Deserialize(_data, offset, out var resultData);
                result.Data = resultData;
                return result;
            }
        }

        public partial class CollectionPDA
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 3845182396760569650UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 50, 183, 127, 103, 4, 213, 92, 53 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "9V1x5Jvbgur";
            public PublicKey Mint { get; set; }

            public PublicKey CandyMachine { get; set; }

            public static CollectionPDA Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                CollectionPDA result = new CollectionPDA();
                result.Mint = _data.GetPubKey(offset);
                offset += 32;
                result.CandyMachine = _data.GetPubKey(offset);
                offset += 32;
                return result;
            }
        }

        public partial class FreezePDA
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 17181566288669752050UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 242, 186, 252, 248, 129, 47, 113, 238 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "hbnmicJtkMs";
            public PublicKey CandyMachine { get; set; }

            public bool AllowThaw { get; set; }

            public ulong FrozenCount { get; set; }

            public long? MintStart { get; set; }

            public long FreezeTime { get; set; }

            public ulong FreezeFee { get; set; }

            public static FreezePDA Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                FreezePDA result = new FreezePDA();
                result.CandyMachine = _data.GetPubKey(offset);
                offset += 32;
                result.AllowThaw = _data.GetBool(offset);
                offset += 1;
                result.FrozenCount = _data.GetU64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.MintStart = _data.GetS64(offset);
                    offset += 8;
                }

                result.FreezeTime = _data.GetS64(offset);
                offset += 8;
                result.FreezeFee = _data.GetU64(offset);
                offset += 8;
                return result;
            }
        }
    }

