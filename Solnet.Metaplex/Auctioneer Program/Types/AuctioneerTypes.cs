using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Auctioneer.Types
{
    public class AuthorizeAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class WithdrawAccounts
    {
        public PublicKey AuctionHouseProgram { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey ReceiptAccount { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class DepositAccounts
    {
        public PublicKey AuctionHouseProgram { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey PaymentAccount { get; set; }

        public PublicKey TransferAuthority { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class CancelAccounts
    {
        public PublicKey AuctionHouseProgram { get; set; }

        public PublicKey ListingConfig { get; set; }

        public PublicKey Seller { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey TokenMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey TradeState { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ExecuteSaleAccounts
    {
        public PublicKey AuctionHouseProgram { get; set; }

        public PublicKey ListingConfig { get; set; }

        public PublicKey Buyer { get; set; }

        public PublicKey Seller { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey TokenMint { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey SellerPaymentReceiptAccount { get; set; }

        public PublicKey BuyerReceiptTokenAccount { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AuctionHouseTreasury { get; set; }

        public PublicKey BuyerTradeState { get; set; }

        public PublicKey SellerTradeState { get; set; }

        public PublicKey FreeTradeState { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class SellAccounts
    {
        public PublicKey AuctionHouseProgram { get; set; }

        public PublicKey ListingConfig { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey SellerTradeState { get; set; }

        public PublicKey FreeSellerTradeState { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class BuyAccounts
    {
        public PublicKey AuctionHouseProgram { get; set; }

        public PublicKey ListingConfig { get; set; }

        public PublicKey Seller { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey PaymentAccount { get; set; }

        public PublicKey TransferAuthority { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey BuyerTradeState { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }
    public partial class Bid
        {
            public ListingConfigVersion Version { get; set; }

            public ulong Amount { get; set; }

            public PublicKey BuyerTradeState { get; set; }

            public int Serialize(byte[] _data, int initialOffset)
            {
                int offset = initialOffset;
                _data.WriteU8((byte)Version, offset);
                offset += 1;
                _data.WriteU64(Amount, offset);
                offset += 8;
                _data.WritePubKey(BuyerTradeState, offset);
                offset += 32;
                return offset - initialOffset;
            }

            public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Bid result)
            {
                int offset = initialOffset;
                result = new Bid();
                result.Version = (ListingConfigVersion)_data.GetU8(offset);
                offset += 1;
                result.Amount = _data.GetU64(offset);
                offset += 8;
                result.BuyerTradeState = _data.GetPubKey(offset);
                offset += 32;
                return offset - initialOffset;
            }
        }

        public enum ListingConfigVersion : byte
        {
            V0
        }
    }
