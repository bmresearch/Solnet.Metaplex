using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solnet.Metaplex.Auctionhouse.Errors
{

    public enum AuctionHouseErrorKind : uint
    {
        PublicKeyMismatch = 6000U,
        InvalidMintAuthority = 6001U,
        UninitializedAccount = 6002U,
        IncorrectOwner = 6003U,
        PublicKeysShouldBeUnique = 6004U,
        StatementFalse = 6005U,
        NotRentExempt = 6006U,
        NumericalOverflow = 6007U,
        ExpectedSolAccount = 6008U,
        CannotExchangeSOLForSol = 6009U,
        SOLWalletMustSign = 6010U,
        CannotTakeThisActionWithoutAuctionHouseSignOff = 6011U,
        NoPayerPresent = 6012U,
        DerivedKeyInvalid = 6013U,
        MetadataDoesntExist = 6014U,
        InvalidTokenAmount = 6015U,
        BothPartiesNeedToAgreeToSale = 6016U,
        CannotMatchFreeSalesWithoutAuctionHouseOrSellerSignoff = 6017U,
        SaleRequiresSigner = 6018U,
        OldSellerNotInitialized = 6019U,
        SellerATACannotHaveDelegate = 6020U,
        BuyerATACannotHaveDelegate = 6021U,
        NoValidSignerPresent = 6022U,
        InvalidBasisPoints = 6023U,
        TradeStateDoesntExist = 6024U,
        TradeStateIsNotEmpty = 6025U,
        ReceiptIsEmpty = 6026U,
        InstructionMismatch = 6027U,
        InvalidAuctioneer = 6028U,
        MissingAuctioneerScope = 6029U,
        MustUseAuctioneerHandler = 6030U,
        NoAuctioneerProgramSet = 6031U,
        TooManyScopes = 6032U,
        AuctionHouseNotDelegated = 6033U,
        BumpSeedNotInHashMap = 6034U,
        EscrowUnderRentExemption = 6035U,
        InvalidSeedsOrAuctionHouseNotDelegated = 6036U,
        BuyerTradeStateNotValid = 6037U,
        MissingElementForPartialOrder = 6038U,
        NotEnoughTokensAvailableForPurchase = 6039U,
        PartialPriceMismatch = 6040U,
        AuctionHouseAlreadyDelegated = 6041U,
        AuctioneerAuthorityMismatch = 6042U,
        InsufficientFunds = 6043U
    }

}
