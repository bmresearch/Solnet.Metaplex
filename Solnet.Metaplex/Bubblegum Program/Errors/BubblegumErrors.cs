namespace Solnet.Metaplex.Bubblegum.Errors
{
    #pragma warning disable CS1591
    public enum BubblegumErrorKind : uint
        {
            AssetOwnerMismatch = 6000U,
            PublicKeyMismatch = 6001U,
            HashingMismatch = 6002U,
            UnsupportedSchemaVersion = 6003U,
            CreatorShareTotalMustBe100 = 6004U,
            DuplicateCreatorAddress = 6005U,
            CreatorDidNotVerify = 6006U,
            CreatorNotFound = 6007U,
            NoCreatorsPresent = 6008U,
            CreatorHashMismatch = 6009U,
            DataHashMismatch = 6010U,
            CreatorsTooLong = 6011U,
            MetadataNameTooLong = 6012U,
            MetadataSymbolTooLong = 6013U,
            MetadataUriTooLong = 6014U,
            MetadataBasisPointsTooHigh = 6015U,
            TreeAuthorityIncorrect = 6016U,
            InsufficientMintCapacity = 6017U,
            NumericalOverflowError = 6018U,
            IncorrectOwner = 6019U,
            CollectionCannotBeVerifiedInThisInstruction = 6020U,
            CollectionNotFound = 6021U,
            AlreadyVerified = 6022U,
            AlreadyUnverified = 6023U,
            UpdateAuthorityIncorrect = 6024U,
            LeafAuthorityMustSign = 6025U
        }
    }

