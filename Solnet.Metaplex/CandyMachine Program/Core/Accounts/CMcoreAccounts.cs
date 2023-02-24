using Solnet.Metaplex.Candymachine.Types;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine.Core.Accounts
{
    public partial class CandyMachine
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 13649831137213787443UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 51, 173, 177, 113, 25, 241, 109, 189 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "9eM5CfcKCCt";
            public ulong Features { get; set; }

            public PublicKey Authority { get; set; }

            public PublicKey MintAuthority { get; set; }

            public PublicKey CollectionMint { get; set; }

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
                result.Features = _data.GetU64(offset);
                offset += 8;
                result.Authority = _data.GetPubKey(offset);
                offset += 32;
                result.MintAuthority = _data.GetPubKey(offset);
                offset += 32;
                result.CollectionMint = _data.GetPubKey(offset);
                offset += 32;
                result.ItemsRedeemed = _data.GetU64(offset);
                offset += 8;
                offset += CandyMachineData.Deserialize(_data, offset, out var resultData);
                result.Data = resultData;
                return result;
            }
        }
    }
