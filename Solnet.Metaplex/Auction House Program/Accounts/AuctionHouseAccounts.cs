using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Auctionhouse.Accounts
{

    public partial class BidReceipt
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 7144813729942443706UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 186, 150, 141, 135, 59, 122, 39, 99 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "YD929MgXGrE";
            public PublicKey TradeState { get; set; }

            public PublicKey Bookkeeper { get; set; }

            public PublicKey AuctionHouse { get; set; }

            public PublicKey Buyer { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey TokenAccount { get; set; }

            public PublicKey PurchaseReceipt { get; set; }

            public ulong Price { get; set; }

            public ulong TokenSize { get; set; }

            public byte Bump { get; set; }

            public byte TradeStateBump { get; set; }

            public long CreatedAt { get; set; }

            public long? CanceledAt { get; set; }

            public static BidReceipt Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                BidReceipt result = new BidReceipt();
                result.TradeState = _data.GetPubKey(offset);
                offset += 32;
                result.Bookkeeper = _data.GetPubKey(offset);
                offset += 32;
                result.AuctionHouse = _data.GetPubKey(offset);
                offset += 32;
                result.Buyer = _data.GetPubKey(offset);
                offset += 32;
                result.Metadata = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.TokenAccount = _data.GetPubKey(offset);
                    offset += 32;
                }

                if (_data.GetBool(offset++))
                {
                    result.PurchaseReceipt = _data.GetPubKey(offset);
                    offset += 32;
                }

                result.Price = _data.GetU64(offset);
                offset += 8;
                result.TokenSize = _data.GetU64(offset);
                offset += 8;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.TradeStateBump = _data.GetU8(offset);
                offset += 1;
                result.CreatedAt = _data.GetS64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.CanceledAt = _data.GetS64(offset);
                    offset += 8;
                }

                return result;
            }
        }

        public partial class ListingReceipt
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 16669031444762413040UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 240, 71, 225, 94, 200, 75, 84, 231 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "hC2RgjuNXEv";
            public PublicKey TradeState { get; set; }

            public PublicKey Bookkeeper { get; set; }

            public PublicKey AuctionHouse { get; set; }

            public PublicKey Seller { get; set; }

            public PublicKey Metadata { get; set; }

            public PublicKey PurchaseReceipt { get; set; }

            public ulong Price { get; set; }

            public ulong TokenSize { get; set; }

            public byte Bump { get; set; }

            public byte TradeStateBump { get; set; }

            public long CreatedAt { get; set; }

            public long? CanceledAt { get; set; }

            public static ListingReceipt Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                ListingReceipt result = new ListingReceipt();
                result.TradeState = _data.GetPubKey(offset);
                offset += 32;
                result.Bookkeeper = _data.GetPubKey(offset);
                offset += 32;
                result.AuctionHouse = _data.GetPubKey(offset);
                offset += 32;
                result.Seller = _data.GetPubKey(offset);
                offset += 32;
                result.Metadata = _data.GetPubKey(offset);
                offset += 32;
                if (_data.GetBool(offset++))
                {
                    result.PurchaseReceipt = _data.GetPubKey(offset);
                    offset += 32;
                }

                result.Price = _data.GetU64(offset);
                offset += 8;
                result.TokenSize = _data.GetU64(offset);
                offset += 8;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.TradeStateBump = _data.GetU8(offset);
                offset += 1;
                result.CreatedAt = _data.GetS64(offset);
                offset += 8;
                if (_data.GetBool(offset++))
                {
                    result.CanceledAt = _data.GetS64(offset);
                    offset += 8;
                }

                return result;
            }
        }

        public partial class PurchaseReceipt
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 9698083547350204239UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 79, 127, 222, 137, 154, 131, 150, 134 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "EJFBwhcydJm";
            public PublicKey Bookkeeper { get; set; }

            public PublicKey Buyer { get; set; }

            public PublicKey Seller { get; set; }

            public PublicKey AuctionHouse { get; set; }

            public PublicKey Metadata { get; set; }

            public ulong TokenSize { get; set; }

            public ulong Price { get; set; }

            public byte Bump { get; set; }

            public long CreatedAt { get; set; }

            public static PurchaseReceipt Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                PurchaseReceipt result = new PurchaseReceipt();
                result.Bookkeeper = _data.GetPubKey(offset);
                offset += 32;
                result.Buyer = _data.GetPubKey(offset);
                offset += 32;
                result.Seller = _data.GetPubKey(offset);
                offset += 32;
                result.AuctionHouse = _data.GetPubKey(offset);
                offset += 32;
                result.Metadata = _data.GetPubKey(offset);
                offset += 32;
                result.TokenSize = _data.GetU64(offset);
                offset += 8;
                result.Price = _data.GetU64(offset);
                offset += 8;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.CreatedAt = _data.GetS64(offset);
                offset += 8;
                return result;
            }
        }

        public partial class AuctionHouse
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 3527820258240326696UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 40, 108, 215, 107, 213, 85, 245, 48 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "7mB8j9Ym6fH";
            public PublicKey AuctionHouseFeeAccount { get; set; }

            public PublicKey AuctionHouseTreasury { get; set; }

            public PublicKey TreasuryWithdrawalDestination { get; set; }

            public PublicKey FeeWithdrawalDestination { get; set; }

            public PublicKey TreasuryMint { get; set; }

            public PublicKey Authority { get; set; }

            public PublicKey Creator { get; set; }

            public byte Bump { get; set; }

            public byte TreasuryBump { get; set; }

            public byte FeePayerBump { get; set; }

            public ushort SellerFeeBasisPoints { get; set; }

            public bool RequiresSignOff { get; set; }

            public bool CanChangeSalePrice { get; set; }

            public byte EscrowPaymentBump { get; set; }

            public bool HasAuctioneer { get; set; }

            public PublicKey AuctioneerAddress { get; set; }

            public bool[] Scopes { get; set; }

            public static AuctionHouse Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                AuctionHouse result = new AuctionHouse();
                result.AuctionHouseFeeAccount = _data.GetPubKey(offset);
                offset += 32;
                result.AuctionHouseTreasury = _data.GetPubKey(offset);
                offset += 32;
                result.TreasuryWithdrawalDestination = _data.GetPubKey(offset);
                offset += 32;
                result.FeeWithdrawalDestination = _data.GetPubKey(offset);
                offset += 32;
                result.TreasuryMint = _data.GetPubKey(offset);
                offset += 32;
                result.Authority = _data.GetPubKey(offset);
                offset += 32;
                result.Creator = _data.GetPubKey(offset);
                offset += 32;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                result.TreasuryBump = _data.GetU8(offset);
                offset += 1;
                result.FeePayerBump = _data.GetU8(offset);
                offset += 1;
                result.SellerFeeBasisPoints = _data.GetU16(offset);
                offset += 2;
                result.RequiresSignOff = _data.GetBool(offset);
                offset += 1;
                result.CanChangeSalePrice = _data.GetBool(offset);
                offset += 1;
                result.EscrowPaymentBump = _data.GetU8(offset);
                offset += 1;
                result.HasAuctioneer = _data.GetBool(offset);
                offset += 1;
                result.AuctioneerAddress = _data.GetPubKey(offset);
                offset += 32;
                result.Scopes = new bool[7];
                for (uint resultScopesIdx = 0; resultScopesIdx < 7; resultScopesIdx++)
                {
                    result.Scopes[resultScopesIdx] = _data.GetBool(offset);
                    offset += 1;
                }

                return result;
            }
        }

        public partial class AuctioneerUser
        {
            public static ulong ACCOUNT_DISCRIMINATOR => 8715906234422420782UL;
            public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 46, 101, 92, 150, 138, 30, 245, 120 };
            public static string ACCOUNT_DISCRIMINATOR_B58 => "8m6jHCBkyno";
            public PublicKey AuctioneerAuthority { get; set; }

            public PublicKey AuctionHouse { get; set; }

            public byte Bump { get; set; }

            public static AuctioneerUser Deserialize(ReadOnlySpan<byte> _data)
            {
                int offset = 0;
                ulong accountHashValue = _data.GetU64(offset);
                offset += 8;
                if (accountHashValue != ACCOUNT_DISCRIMINATOR)
                {
                    return null;
                }

                AuctioneerUser result = new AuctioneerUser();
                result.AuctioneerAuthority = _data.GetPubKey(offset);
                offset += 32;
                result.AuctionHouse = _data.GetPubKey(offset);
                offset += 32;
                result.Bump = _data.GetU8(offset);
                offset += 1;
                return result;
            }
        }

}
