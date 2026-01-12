#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Solnet.Metaplex.Core;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using Solnet.Metaplex.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solnet.Metaplex.Core.Accounts
{
    public partial class PluginHeaderV1
    {
        public Key Key { get; set; }

        public ulong PluginRegistryOffset { get; set; }

        public static PluginHeaderV1 Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            PluginHeaderV1 result = new PluginHeaderV1();
            result.Key = (Key)_data.GetU8(offset);
            offset += 1;
            result.PluginRegistryOffset = _data.GetU64(offset);
            offset += 8;
            return result;
        }
    }

    public partial class PluginRegistryV1
    {
        public Key Key { get; set; }

        public RegistryRecord[] Registry { get; set; }

        public ExternalPluginRecord[] ExternalPlugins { get; set; }

        public static PluginRegistryV1 Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            PluginRegistryV1 result = new PluginRegistryV1();
            result.Key = (Key)_data.GetU8(offset);
            offset += 1;
            int resultRegistryLength = (int)_data.GetU32(offset);
            offset += 4;
            result.Registry = new RegistryRecord[resultRegistryLength];
            for (uint resultRegistryIdx = 0; resultRegistryIdx < resultRegistryLength; resultRegistryIdx++)
            {
                offset += RegistryRecord.Deserialize(_data, offset, out var resultRegistryresultRegistryIdx);
                result.Registry[resultRegistryIdx] = resultRegistryresultRegistryIdx;
            }

            int resultExternalPluginsLength = (int)_data.GetU32(offset);
            offset += 4;
            result.ExternalPlugins = new ExternalPluginRecord[resultExternalPluginsLength];
            for (uint resultExternalPluginsIdx = 0; resultExternalPluginsIdx < resultExternalPluginsLength; resultExternalPluginsIdx++)
            {
                offset += ExternalPluginRecord.Deserialize(_data, offset, out var resultExternalPluginsresultExternalPluginsIdx);
                result.ExternalPlugins[resultExternalPluginsIdx] = resultExternalPluginsresultExternalPluginsIdx;
            }

            return result;
        }
    }

    public partial class AssetV1
    {
        public Key Key { get; set; }

        public PublicKey Owner { get; set; }

        public PublicKey UpdateAuthority { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public ulong? Seq { get; set; }

        public static AssetV1 Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            AssetV1 result = new AssetV1();
            result.Key = (Key)_data.GetU8(offset);
            offset += 1;
            result.Owner = _data.GetPubKey(offset);
            offset += 32;
            offset += 1;
            result.UpdateAuthority = _data.GetPubKey(offset);
            offset += 32;
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            offset += _data.GetBorshString(offset, out var resultUri);
            result.Uri = resultUri;
            if (_data.GetBool(offset++))
            {
                result.Seq = _data.GetU64(offset);
                offset += 8;
            }

            return result;
        }
    }

    public partial class CollectionV1
    {
        public Key Key { get; set; }

        public PublicKey UpdateAuthority { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public uint NumMinted { get; set; }

        public uint CurrentSize { get; set; }

        public static CollectionV1 Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            CollectionV1 result = new CollectionV1();
            result.Key = (Key)_data.GetU8(offset);
            offset += 1;
            result.UpdateAuthority = _data.GetPubKey(offset);
            offset += 32;
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            offset += _data.GetBorshString(offset, out var resultUri);
            result.Uri = resultUri;
            result.NumMinted = _data.GetU32(offset);
            offset += 4;
            result.CurrentSize = _data.GetU32(offset);
            offset += 4;
            return result;
        }
    }

    public partial class HashedAssetV1
    {
        public Key Key { get; set; }

        public byte[] Hash { get; set; }

        public static HashedAssetV1 Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            HashedAssetV1 result = new HashedAssetV1();
            result.Key = (Key)_data.GetU8(offset);
            offset += 1;
            result.Hash = _data.GetBytes(offset, 32);
            offset += 32;
            return result;
        }
    }
}
