#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine.Core.Errors
{
    public enum CandyMachineCoreErrorKind : uint
        {
            IncorrectOwner = 6000U,
            Uninitialized = 6001U,
            MintMismatch = 6002U,
            IndexGreaterThanLength = 6003U,
            NumericalOverflowError = 6004U,
            TooManyCreators = 6005U,
            CandyMachineEmpty = 6006U,
            HiddenSettingsDoNotHaveConfigLines = 6007U,
            CannotChangeNumberOfLines = 6008U,
            CannotSwitchToHiddenSettings = 6009U,
            IncorrectCollectionAuthority = 6010U,
            MetadataAccountMustBeEmpty = 6011U,
            NoChangingCollectionDuringMint = 6012U,
            ExceededLengthError = 6013U,
            MissingConfigLinesSettings = 6014U,
            CannotIncreaseLength = 6015U,
            CannotSwitchFromHiddenSettings = 6016U,
            CannotChangeSequentialIndexGeneration = 6017U,
            CollectionKeyMismatch = 6018U,
            CouldNotRetrieveConfigLineData = 6019U,
            NotFullyLoaded = 6020U
        }
    }
