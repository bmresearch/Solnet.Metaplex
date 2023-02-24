#pragma warning disable CS1591

namespace Solnet.Metaplex.Auctioneer.Errors
{
    public enum AuctioneerErrorKind : uint
    {
        BumpSeedNotInHashMap = 6000U,
        AuctionNotStarted = 6001U,
        AuctionEnded = 6002U,
        AuctionActive = 6003U,
        BidTooLow = 6004U,
        SignerNotAuth = 6005U,
        NotHighestBidder = 6006U,
        BelowReservePrice = 6007U,
        BelowBidIncrement = 6008U,
        CannotCancelHighestBid = 6009U
    }
}
