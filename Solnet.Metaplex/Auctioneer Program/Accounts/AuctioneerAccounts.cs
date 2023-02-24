#pragma warning disable CS1591
using Solnet.Metaplex.Auctioneer.Types;
using Solnet.Programs.Utilities;
using System;

namespace Solnet.Metaplex.Auctioneer.Accounts
{
    public partial class AuctioneerAuthority
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 920233374776249060UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 228, 74, 255, 245, 96, 83, 197, 12 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "fBjDC8APBxF";
            public byte Bump { get; set; }

            public static AuctioneerAuthority Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                AuctioneerAuthority result = new AuctioneerAuthority();
                result.Bump = _data.GetU8(offset);
                offset += 1;
                return result;
            }
        }

        public partial class ListingConfig
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 8338465850941686967UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 183, 196, 26, 41, 131, 46, 184, 115 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "Xjm7kw3Unhx";
            public ListingConfigVersion Version { get; set; }

            public long StartTime { get; set; }

            public long EndTime { get; set; }

            public Bid HighestBid { get; set; }

            public byte Bump { get; set; }

            public ulong ReservePrice { get; set; }

            public ulong MinBidIncrement { get; set; }

            public uint TimeExtPeriod { get; set; }

            public uint TimeExtDelta { get; set; }

            public bool AllowHighBidCancel { get; set; }

            public static ListingConfig Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                ListingConfig result = new ListingConfig();
                result.Version = (ListingConfigVersion)_data.GetU8(offset);
                offset += 1;
                result.StartTime = _data.GetS64(offset);
                offset += 8;
                result.EndTime = _data.GetS64(offset);
                offset += 8;
                offset += Bid.Deserialize(_data, offset, out var resultHighestBid);
                result.HighestBid = resultHighestBid;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.ReservePrice = _data.GetU64(offset);
                offset += 8;
                result.MinBidIncrement = _data.GetU64(offset);
                offset += 8;
                result.TimeExtPeriod = _data.GetU32(offset);
                offset += 4;
                result.TimeExtDelta = _data.GetU32(offset);
                offset += 4;
                result.AllowHighBidCancel = _data.GetBool(offset);
                offset += 1;
                return result;
            }
        }
    }
