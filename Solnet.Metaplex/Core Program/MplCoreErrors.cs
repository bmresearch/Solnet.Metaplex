using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Solnet.Metaplex.Core
{
    public enum MplCoreError : uint
    {
        InvalidSystemProgram = 0U,
        DeserializationError = 1U,
        SerializationError = 2U,
        PluginsNotInitialized = 3U,
        PluginNotFound = 4U,
        NumericalOverflow = 5U,
        IncorrectAccount = 6U,
        IncorrectAssetHash = 7U,
        InvalidPlugin = 8U,
        InvalidAuthority = 9U,
        AssetIsFrozen = 10U,
        MissingCompressionProof = 11U,
        CannotMigrateMasterWithSupply = 12U,
        CannotMigratePrints = 13U,
        CannotBurnCollection = 14U,
        PluginAlreadyExists = 15U,
        NumericalOverflowError = 16U,
        AlreadyCompressed = 17U,
        AlreadyDecompressed = 18U,
        InvalidCollection = 19U,
        MissingUpdateAuthority = 20U,
        MissingNewOwner = 21U,
        MissingSystemProgram = 22U,
        NotAvailable = 23U,
        InvalidAsset = 24U,
        MissingCollection = 25U,
        NoApprovals = 26U,
        CannotRedelegate = 27U,
        InvalidPluginSetting = 28U,
        ConflictingAuthority = 29U,
        InvalidLogWrapperProgram = 30U
    }
}
