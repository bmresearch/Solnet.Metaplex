#pragma warning disable CS1591
using Solnet.Wallet;

namespace Solnet.Metaplex.Auctionhouse.Types
{
    public enum AuthorityScope : byte
    {
        Deposit,
        Buy,
        PublicBuy,
        ExecuteSale,
        Sell,
        Cancel,
        Withdraw
    }

    public class WithdrawFromFeeAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey FeeWithdrawalDestination { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class WithdrawFromTreasuryAccounts
    {
        public PublicKey TreasuryMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey TreasuryWithdrawalDestination { get; set; }

        public PublicKey AuctionHouseTreasury { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class UpdateAuctionHouseAccounts
    {
        public PublicKey TreasuryMint { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey NewAuthority { get; set; }

        public PublicKey FeeWithdrawalDestination { get; set; }

        public PublicKey TreasuryWithdrawalDestination { get; set; }

        public PublicKey TreasuryWithdrawalDestinationOwner { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class CreateAuctionHouseAccounts
    {
        public PublicKey TreasuryMint { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey FeeWithdrawalDestination { get; set; }

        public PublicKey TreasuryWithdrawalDestination { get; set; }

        public PublicKey TreasuryWithdrawalDestinationOwner { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AuctionHouseTreasury { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class BuyAccounts
    {
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

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class AuctioneerBuyAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey PaymentAccount { get; set; }

        public PublicKey TransferAuthority { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey BuyerTradeState { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class PublicBuyAccounts
    {
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

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class AuctioneerPublicBuyAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey PaymentAccount { get; set; }

        public PublicKey TransferAuthority { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey BuyerTradeState { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class CancelAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey TokenMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey TradeState { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class AuctioneerCancelAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey TokenMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey TradeState { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class DepositAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey PaymentAccount { get; set; }

        public PublicKey TransferAuthority { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class AuctioneerDepositAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey PaymentAccount { get; set; }

        public PublicKey TransferAuthority { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class ExecuteSaleAccounts
    {
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

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class ExecutePartialSaleAccounts
    {
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

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class AuctioneerExecuteSaleAccounts
    {
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

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AuctionHouseTreasury { get; set; }

        public PublicKey BuyerTradeState { get; set; }

        public PublicKey SellerTradeState { get; set; }

        public PublicKey FreeTradeState { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class AuctioneerExecutePartialSaleAccounts
    {
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

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AuctionHouseTreasury { get; set; }

        public PublicKey BuyerTradeState { get; set; }

        public PublicKey SellerTradeState { get; set; }

        public PublicKey FreeTradeState { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class SellAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey SellerTradeState { get; set; }

        public PublicKey FreeSellerTradeState { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class AuctioneerSellAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey SellerTradeState { get; set; }

        public PublicKey FreeSellerTradeState { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey ProgramAsSigner { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class WithdrawAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey ReceiptAccount { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class AuctioneerWithdrawAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey ReceiptAccount { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey TreasuryMint { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey AuctionHouseFeeAccount { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey AtaProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class CloseEscrowAccountAccounts
    {
        public PublicKey Wallet { get; set; }

        public PublicKey EscrowPaymentAccount { get; set; }

        public PublicKey AuctionHouse { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class DelegateAuctioneerAccounts
    {
        public PublicKey AuctionHouse { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class UpdateAuctioneerAccounts
    {
        public PublicKey AuctionHouse { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuctioneerAuthority { get; set; }

        public PublicKey AhAuctioneerPda { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class PrintListingReceiptAccounts
    {
        public PublicKey Receipt { get; set; }

        public PublicKey Bookkeeper { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey Instruction { get; set; }
    }

    public class CancelListingReceiptAccounts
    {
        public PublicKey Receipt { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Instruction { get; set; }
    }

    public class PrintBidReceiptAccounts
    {
        public PublicKey Receipt { get; set; }

        public PublicKey Bookkeeper { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey Instruction { get; set; }
    }

    public class CancelBidReceiptAccounts
    {
        public PublicKey Receipt { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Instruction { get; set; }
    }

    public class PrintPurchaseReceiptAccounts
    {
        public PublicKey PurchaseReceipt { get; set; }

        public PublicKey ListingReceipt { get; set; }

        public PublicKey BidReceipt { get; set; }

        public PublicKey Bookkeeper { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey Instruction { get; set; }
    }
}
