using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Solnet.Metaplex.Core.Types
{
    public partial class PluginAuthorityPair
    {
        public Plugin Plugin { get; set; }

        public Authority Authority { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += Plugin.Serialize(_data, offset);
            if (Authority != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += Authority.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PluginAuthorityPair result)
        {
            int offset = initialOffset;
            result = new PluginAuthorityPair();
            offset += Plugin.Deserialize(_data, offset, out var resultPlugin);
            result.Plugin = resultPlugin;
            if (_data.GetBool(offset++))
            {
                offset += Authority.Deserialize(_data, offset, out var resultAuthority);
                result.Authority = resultAuthority;
            }

            return offset - initialOffset;
        }
    }

    public partial class Attribute
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += _data.WriteBorshString(Key, offset);
            offset += _data.WriteBorshString(Value, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Attribute result)
        {
            int offset = initialOffset;
            result = new Attribute();
            offset += _data.GetBorshString(offset, out var resultKey);
            result.Key = resultKey;
            offset += _data.GetBorshString(offset, out var resultValue);
            result.Value = resultValue;
            return offset - initialOffset;
        }
    }

    public partial class Attributes
    {
        public Attribute[] AttributeList { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteS32(AttributeList.Length, offset);
            offset += 4;
            foreach (var attributeListElement in AttributeList)
            {
                offset += attributeListElement.Serialize(_data, offset);
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Attributes result)
        {
            int offset = initialOffset;
            result = new Attributes();
            int resultAttributeListLength = (int)_data.GetU32(offset);
            offset += 4;
            result.AttributeList = new Attribute[resultAttributeListLength];
            for (uint resultAttributeListIdx = 0; resultAttributeListIdx < resultAttributeListLength; resultAttributeListIdx++)
            {
                offset += Attribute.Deserialize(_data, offset, out var resultAttributeListresultAttributeListIdx);
                result.AttributeList[resultAttributeListIdx] = resultAttributeListresultAttributeListIdx;
            }

            return offset - initialOffset;
        }
    }

    public partial class BurnDelegate
    {
        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BurnDelegate result)
        {
            int offset = initialOffset;
            result = new BurnDelegate();
            return offset - initialOffset;
        }
    }

    public partial class Edition
    {
        public uint Number { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU32(Number, offset);
            offset += 4;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Edition result)
        {
            int offset = initialOffset;
            result = new Edition();
            result.Number = _data.GetU32(offset);
            offset += 4;
            return offset - initialOffset;
        }
    }

    public partial class FreezeDelegate
    {
        public bool Frozen { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteBool(Frozen, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out FreezeDelegate result)
        {
            int offset = initialOffset;
            result = new FreezeDelegate();
            result.Frozen = _data.GetBool(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class MasterEdition
    {
        public uint? MaxSupply { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            if (MaxSupply != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU32(MaxSupply.Value, offset);
                offset += 4;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (Name != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += _data.WriteBorshString(Name, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (Uri != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += _data.WriteBorshString(Uri, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out MasterEdition result)
        {
            int offset = initialOffset;
            result = new MasterEdition();
            if (_data.GetBool(offset++))
            {
                result.MaxSupply = _data.GetU32(offset);
                offset += 4;
            }

            if (_data.GetBool(offset++))
            {
                offset += _data.GetBorshString(offset, out var resultName);
                result.Name = resultName;
            }

            if (_data.GetBool(offset++))
            {
                offset += _data.GetBorshString(offset, out var resultUri);
                result.Uri = resultUri;
            }

            return offset - initialOffset;
        }
    }

    public partial class PermanentBurnDelegate
    {
        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PermanentBurnDelegate result)
        {
            int offset = initialOffset;
            result = new PermanentBurnDelegate();
            return offset - initialOffset;
        }
    }

    public partial class PermanentFreezeDelegate
    {
        public bool Frozen { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteBool(Frozen, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PermanentFreezeDelegate result)
        {
            int offset = initialOffset;
            result = new PermanentFreezeDelegate();
            result.Frozen = _data.GetBool(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class PermanentTransferDelegate
    {
        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out PermanentTransferDelegate result)
        {
            int offset = initialOffset;
            result = new PermanentTransferDelegate();
            return offset - initialOffset;
        }
    }

    public partial class RegistryRecord
    {
        public PluginType PluginType { get; set; }

        public Authority Authority { get; set; }

        public ulong Offset { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)PluginType, offset);
            offset += 1;
            offset += Authority.Serialize(_data, offset);
            _data.WriteU64(Offset, offset);
            offset += 8;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RegistryRecord result)
        {
            int offset = initialOffset;
            result = new RegistryRecord();
            result.PluginType = (PluginType)_data.GetU8(offset);
            offset += 1;
            offset += Authority.Deserialize(_data, offset, out var resultAuthority);
            result.Authority = resultAuthority;
            result.Offset = _data.GetU64(offset);
            offset += 8;
            return offset - initialOffset;
        }
    }

    public partial class ExternalPluginRecord
    {
        public Authority Authority { get; set; }

        public ulong Offset { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += Authority.Serialize(_data, offset);
            _data.WriteU64(Offset, offset);
            offset += 8;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ExternalPluginRecord result)
        {
            int offset = initialOffset;
            result = new ExternalPluginRecord();
            offset += Authority.Deserialize(_data, offset, out var resultAuthority);
            result.Authority = resultAuthority;
            result.Offset = _data.GetU64(offset);
            offset += 8;
            return offset - initialOffset;
        }
    }

    public partial class Creator
    {
        public PublicKey Address { get; set; }

        public byte Percentage { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WritePubKey(Address, offset);
            offset += 32;
            _data.WriteU8(Percentage, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Creator result)
        {
            int offset = initialOffset;
            result = new Creator();
            result.Address = _data.GetPubKey(offset);
            offset += 32;
            result.Percentage = _data.GetU8(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class Royalties
    {
        public ushort BasisPoints { get; set; }

        public Creator[] Creators { get; set; }

        public RuleSet RuleSet { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU16(BasisPoints, offset);
            offset += 2;
            _data.WriteS32(Creators.Length, offset);
            offset += 4;
            foreach (var creatorsElement in Creators)
            {
                offset += creatorsElement.Serialize(_data, offset);
            }

            offset += RuleSet.Serialize(_data, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Royalties result)
        {
            int offset = initialOffset;
            result = new Royalties();
            result.BasisPoints = _data.GetU16(offset);
            offset += 2;
            int resultCreatorsLength = (int)_data.GetU32(offset);
            offset += 4;
            result.Creators = new Creator[resultCreatorsLength];
            for (uint resultCreatorsIdx = 0; resultCreatorsIdx < resultCreatorsLength; resultCreatorsIdx++)
            {
                offset += Creator.Deserialize(_data, offset, out var resultCreatorsresultCreatorsIdx);
                result.Creators[resultCreatorsIdx] = resultCreatorsresultCreatorsIdx;
            }

            offset += RuleSet.Deserialize(_data, offset, out var resultRuleSet);
            result.RuleSet = resultRuleSet;
            return offset - initialOffset;
        }
    }

    public partial class TransferDelegate
    {
        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out TransferDelegate result)
        {
            int offset = initialOffset;
            result = new TransferDelegate();
            return offset - initialOffset;
        }
    }

    public partial class UpdateDelegate
    {
        public PublicKey[] AdditionalDelegates { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteS32(AdditionalDelegates.Length, offset);
            offset += 4;
            foreach (var additionalDelegatesElement in AdditionalDelegates)
            {
                _data.WritePubKey(additionalDelegatesElement, offset);
                offset += 32;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateDelegate result)
        {
            int offset = initialOffset;
            result = new UpdateDelegate();
            int resultAdditionalDelegatesLength = (int)_data.GetU32(offset);
            offset += 4;
            result.AdditionalDelegates = new PublicKey[resultAdditionalDelegatesLength];
            for (uint resultAdditionalDelegatesIdx = 0; resultAdditionalDelegatesIdx < resultAdditionalDelegatesLength; resultAdditionalDelegatesIdx++)
            {
                result.AdditionalDelegates[resultAdditionalDelegatesIdx] = _data.GetPubKey(offset);
                offset += 32;
            }

            return offset - initialOffset;
        }
    }

    public partial class AddPluginV1Args
    {
        public Plugin Plugin { get; set; }

        public Authority InitAuthority { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += Plugin.Serialize(_data, offset);
            if (InitAuthority != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += InitAuthority.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out AddPluginV1Args result)
        {
            int offset = initialOffset;
            result = new AddPluginV1Args();
            offset += Plugin.Deserialize(_data, offset, out var resultPlugin);
            result.Plugin = resultPlugin;
            if (_data.GetBool(offset++))
            {
                offset += Authority.Deserialize(_data, offset, out var resultInitAuthority);
                result.InitAuthority = resultInitAuthority;
            }

            return offset - initialOffset;
        }
    }

    public partial class AddCollectionPluginV1Args
    {
        public Plugin Plugin { get; set; }

        public Authority InitAuthority { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += Plugin.Serialize(_data, offset);
            if (InitAuthority != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += InitAuthority.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out AddCollectionPluginV1Args result)
        {
            int offset = initialOffset;
            result = new AddCollectionPluginV1Args();
            offset += Plugin.Deserialize(_data, offset, out var resultPlugin);
            result.Plugin = resultPlugin;
            if (_data.GetBool(offset++))
            {
                offset += Authority.Deserialize(_data, offset, out var resultInitAuthority);
                result.InitAuthority = resultInitAuthority;
            }

            return offset - initialOffset;
        }
    }

    public partial class ApprovePluginAuthorityV1Args
    {
        public PluginType PluginType { get; set; }

        public Authority NewAuthority { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)PluginType, offset);
            offset += 1;
            offset += NewAuthority.Serialize(_data, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ApprovePluginAuthorityV1Args result)
        {
            int offset = initialOffset;
            result = new ApprovePluginAuthorityV1Args();
            result.PluginType = (PluginType)_data.GetU8(offset);
            offset += 1;
            offset += Authority.Deserialize(_data, offset, out var resultNewAuthority);
            result.NewAuthority = resultNewAuthority;
            return offset - initialOffset;
        }
    }

    public partial class ApproveCollectionPluginAuthorityV1Args
    {
        public PluginType PluginType { get; set; }

        public Authority NewAuthority { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)PluginType, offset);
            offset += 1;
            offset += NewAuthority.Serialize(_data, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ApproveCollectionPluginAuthorityV1Args result)
        {
            int offset = initialOffset;
            result = new ApproveCollectionPluginAuthorityV1Args();
            result.PluginType = (PluginType)_data.GetU8(offset);
            offset += 1;
            offset += Authority.Deserialize(_data, offset, out var resultNewAuthority);
            result.NewAuthority = resultNewAuthority;
            return offset - initialOffset;
        }
    }

    public partial class BurnV1Args
    {
        public CompressionProof CompressionProof { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            if (CompressionProof != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += CompressionProof.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BurnV1Args result)
        {
            int offset = initialOffset;
            result = new BurnV1Args();
            if (_data.GetBool(offset++))
            {
                offset += CompressionProof.Deserialize(_data, offset, out var resultCompressionProof);
                result.CompressionProof = resultCompressionProof;
            }

            return offset - initialOffset;
        }
    }

    public partial class BurnCollectionV1Args
    {
        public CompressionProof CompressionProof { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            if (CompressionProof != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += CompressionProof.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out BurnCollectionV1Args result)
        {
            int offset = initialOffset;
            result = new BurnCollectionV1Args();
            if (_data.GetBool(offset++))
            {
                offset += CompressionProof.Deserialize(_data, offset, out var resultCompressionProof);
                result.CompressionProof = resultCompressionProof;
            }

            return offset - initialOffset;
        }
    }

    public partial class CompressV1Args
    {
        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CompressV1Args result)
        {
            int offset = initialOffset;
            result = new CompressV1Args();
            return offset - initialOffset;
        }
    }

    public partial class CreateV1Args
    {
        public DataState DataState { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public PluginAuthorityPair[] Plugins { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            byte[] encodedName = Encoding.UTF8.GetBytes(Name);
            byte[] encodedUri = Encoding.UTF8.GetBytes(Uri);
            int offset = initialOffset;
            _data.WriteU8((byte)DataState, offset);

            offset += 1;
            offset += _data.WriteBorshString(Name, offset);
            offset += _data.WriteBorshString(Uri, offset);
            if (Plugins != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteS32(Plugins.Length, offset);
                offset += 4;
                foreach (var pluginsElement in Plugins)
                {
                    offset += pluginsElement.Serialize(_data, offset);
                }
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CreateV1Args result)
        {
            int offset = initialOffset;
            result = new CreateV1Args();
            result.DataState = (DataState)_data.GetU8(offset);
            offset += 1;
            int nameLength = 0;
            nameLength = _data.GetU8(offset);
            offset += 1;
            result.Name = Encoding.UTF8.GetString(_data.Slice(offset, offset+ nameLength));


            offset += nameLength;

            if (_data.GetBool(offset++))
            {
                int resultPluginsLength = (int)_data.GetU32(offset);
                offset += 4;
                result.Plugins = new PluginAuthorityPair[resultPluginsLength];
                for (uint resultPluginsIdx = 0; resultPluginsIdx < resultPluginsLength; resultPluginsIdx++)
                {
                    offset += PluginAuthorityPair.Deserialize(_data, offset, out var resultPluginsresultPluginsIdx);
                    result.Plugins[resultPluginsIdx] = resultPluginsresultPluginsIdx;
                }
            }

            return offset - initialOffset;
        }
    }

    public partial class CreateCollectionV1Args
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public PluginAuthorityPair[] Plugins { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += _data.WriteBorshString(Name, offset);
            offset += _data.WriteBorshString(Uri, offset);
            if (Plugins != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteS32(Plugins.Length, offset);
                offset += 4;
                foreach (var pluginsElement in Plugins)
                {
                    offset += pluginsElement.Serialize(_data, offset);
                }
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CreateCollectionV1Args result)
        {
            int offset = initialOffset;
            result = new CreateCollectionV1Args();
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            offset += _data.GetBorshString(offset, out var resultUri);
            result.Uri = resultUri;
            if (_data.GetBool(offset++))
            {
                int resultPluginsLength = (int)_data.GetU32(offset);
                offset += 4;
                result.Plugins = new PluginAuthorityPair[resultPluginsLength];
                for (uint resultPluginsIdx = 0; resultPluginsIdx < resultPluginsLength; resultPluginsIdx++)
                {
                    offset += PluginAuthorityPair.Deserialize(_data, offset, out var resultPluginsresultPluginsIdx);
                    result.Plugins[resultPluginsIdx] = resultPluginsresultPluginsIdx;
                }
            }

            return offset - initialOffset;
        }
    }

    public partial class DecompressV1Args
    {
        public CompressionProof CompressionProof { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += CompressionProof.Serialize(_data, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out DecompressV1Args result)
        {
            int offset = initialOffset;
            result = new DecompressV1Args();
            offset += CompressionProof.Deserialize(_data, offset, out var resultCompressionProof);
            result.CompressionProof = resultCompressionProof;
            return offset - initialOffset;
        }
    }

    public partial class RemovePluginV1Args
    {
        public PluginType PluginType { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)PluginType, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RemovePluginV1Args result)
        {
            int offset = initialOffset;
            result = new RemovePluginV1Args();
            result.PluginType = (PluginType)_data.GetU8(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class RemoveCollectionPluginV1Args
    {
        public PluginType PluginType { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)PluginType, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RemoveCollectionPluginV1Args result)
        {
            int offset = initialOffset;
            result = new RemoveCollectionPluginV1Args();
            result.PluginType = (PluginType)_data.GetU8(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class RevokePluginAuthorityV1Args
    {
        public PluginType PluginType { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)PluginType, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RevokePluginAuthorityV1Args result)
        {
            int offset = initialOffset;
            result = new RevokePluginAuthorityV1Args();
            result.PluginType = (PluginType)_data.GetU8(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class RevokeCollectionPluginAuthorityV1Args
    {
        public PluginType PluginType { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)PluginType, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RevokeCollectionPluginAuthorityV1Args result)
        {
            int offset = initialOffset;
            result = new RevokeCollectionPluginAuthorityV1Args();
            result.PluginType = (PluginType)_data.GetU8(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class TransferV1Args
    {
        public CompressionProof CompressionProof { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            if (CompressionProof != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += CompressionProof.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out TransferV1Args result)
        {
            int offset = initialOffset;
            result = new TransferV1Args();
            if (_data.GetBool(offset++))
            {
                offset += CompressionProof.Deserialize(_data, offset, out var resultCompressionProof);
                result.CompressionProof = resultCompressionProof;
            }

            return offset - initialOffset;
        }
    }

    public partial class UpdateV1Args
    {
        public string NewName { get; set; }

        public string NewUri { get; set; }

        public UpdateAuthority NewUpdateAuthority { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            if (NewName != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += _data.WriteBorshString(NewName, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (NewUri != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += _data.WriteBorshString(NewUri, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (NewUpdateAuthority != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += NewUpdateAuthority.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateV1Args result)
        {
            int offset = initialOffset;
            result = new UpdateV1Args();
            if (_data.GetBool(offset++))
            {
                offset += _data.GetBorshString(offset, out var resultNewName);
                result.NewName = resultNewName;
            }

            if (_data.GetBool(offset++))
            {
                offset += _data.GetBorshString(offset, out var resultNewUri);
                result.NewUri = resultNewUri;
            }

            if (_data.GetBool(offset++))
            {
                offset += UpdateAuthority.Deserialize(_data, offset, out var resultNewUpdateAuthority);
                result.NewUpdateAuthority = resultNewUpdateAuthority;
            }

            return offset - initialOffset;
        }
    }

    public partial class UpdateCollectionV1Args
    {
        public string NewName { get; set; }

        public string NewUri { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            if (NewName != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += _data.WriteBorshString(NewName, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (NewUri != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += _data.WriteBorshString(NewUri, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateCollectionV1Args result)
        {
            int offset = initialOffset;
            result = new UpdateCollectionV1Args();
            if (_data.GetBool(offset++))
            {
                offset += _data.GetBorshString(offset, out var resultNewName);
                result.NewName = resultNewName;
            }

            if (_data.GetBool(offset++))
            {
                offset += _data.GetBorshString(offset, out var resultNewUri);
                result.NewUri = resultNewUri;
            }

            return offset - initialOffset;
        }
    }

    public partial class UpdatePluginV1Args
    {
        public Plugin Plugin { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += Plugin.Serialize(_data, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdatePluginV1Args result)
        {
            int offset = initialOffset;
            result = new UpdatePluginV1Args();
            offset += Plugin.Deserialize(_data, offset, out var resultPlugin);
            result.Plugin = resultPlugin;
            return offset - initialOffset;
        }
    }

    public partial class UpdateCollectionPluginV1Args
    {
        public Plugin Plugin { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += Plugin.Serialize(_data, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateCollectionPluginV1Args result)
        {
            int offset = initialOffset;
            result = new UpdateCollectionPluginV1Args();
            offset += Plugin.Deserialize(_data, offset, out var resultPlugin);
            result.Plugin = resultPlugin;
            return offset - initialOffset;
        }
    }

    public partial class CompressionProof
    {
        public PublicKey Owner { get; set; }

        public UpdateAuthority UpdateAuthority { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public ulong Seq { get; set; }

        public HashablePluginSchema[] Plugins { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WritePubKey(Owner, offset);
            offset += 32;
            offset += UpdateAuthority.Serialize(_data, offset);
            offset += _data.WriteBorshString(Name, offset);
            offset += _data.WriteBorshString(Uri, offset);
            _data.WriteU64(Seq, offset);
            offset += 8;
            _data.WriteS32(Plugins.Length, offset);
            offset += 4;
            foreach (var pluginsElement in Plugins)
            {
                offset += pluginsElement.Serialize(_data, offset);
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CompressionProof result)
        {
            int offset = initialOffset;
            result = new CompressionProof();
            result.Owner = _data.GetPubKey(offset);
            offset += 32;
            offset += UpdateAuthority.Deserialize(_data, offset, out var resultUpdateAuthority);
            result.UpdateAuthority = resultUpdateAuthority;
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            offset += _data.GetBorshString(offset, out var resultUri);
            result.Uri = resultUri;
            result.Seq = _data.GetU64(offset);
            offset += 8;
            int resultPluginsLength = (int)_data.GetU32(offset);
            offset += 4;
            result.Plugins = new HashablePluginSchema[resultPluginsLength];
            for (uint resultPluginsIdx = 0; resultPluginsIdx < resultPluginsLength; resultPluginsIdx++)
            {
                offset += HashablePluginSchema.Deserialize(_data, offset, out var resultPluginsresultPluginsIdx);
                result.Plugins[resultPluginsIdx] = resultPluginsresultPluginsIdx;
            }

            return offset - initialOffset;
        }
    }

    public partial class HashablePluginSchema
    {
        public ulong Index { get; set; }

        public Authority Authority { get; set; }

        public Plugin Plugin { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU64(Index, offset);
            offset += 8;
            offset += Authority.Serialize(_data, offset);
            offset += Plugin.Serialize(_data, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out HashablePluginSchema result)
        {
            int offset = initialOffset;
            result = new HashablePluginSchema();
            result.Index = _data.GetU64(offset);
            offset += 8;
            offset += Authority.Deserialize(_data, offset, out var resultAuthority);
            result.Authority = resultAuthority;
            offset += Plugin.Deserialize(_data, offset, out var resultPlugin);
            result.Plugin = resultPlugin;
            return offset - initialOffset;
        }
    }

    public enum PluginType : byte
    {
        Royalties,
        FreezeDelegate,
        BurnDelegate,
        TransferDelegate,
        UpdateDelegate,
        PermanentFreezeDelegate,
        Attributes,
        PermanentTransferDelegate,
        PermanentBurnDelegate,
        Edition,
        MasterEdition
    }

    public partial class Plugin
    {
        public Tuple<Royalties> RoyaltiesValue { get; set; }

        public Tuple<FreezeDelegate> FreezeDelegateValue { get; set; }

        public Tuple<BurnDelegate> BurnDelegateValue { get; set; }

        public Tuple<TransferDelegate> TransferDelegateValue { get; set; }

        public Tuple<UpdateDelegate> UpdateDelegateValue { get; set; }

        public Tuple<PermanentFreezeDelegate> PermanentFreezeDelegateValue { get; set; }

        public Tuple<Attributes> AttributesValue { get; set; }

        public Tuple<PermanentTransferDelegate> PermanentTransferDelegateValue { get; set; }

        public Tuple<PermanentBurnDelegate> PermanentBurnDelegateValue { get; set; }

        public Tuple<Edition> EditionValue { get; set; }

        public Tuple<MasterEdition> MasterEditionValue { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)Type, offset);
            offset += 1;
            switch (Type)
            {
                case PluginType.Royalties:
                    offset += RoyaltiesValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.FreezeDelegate:
                    offset += FreezeDelegateValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.BurnDelegate:
                    offset += BurnDelegateValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.TransferDelegate:
                    offset += TransferDelegateValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.UpdateDelegate:
                    offset += UpdateDelegateValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.PermanentFreezeDelegate:
                    offset += PermanentFreezeDelegateValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.Attributes:
                    offset += AttributesValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.PermanentTransferDelegate:
                    offset += PermanentTransferDelegateValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.PermanentBurnDelegate:
                    offset += PermanentBurnDelegateValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.Edition:
                    offset += EditionValue.Item1.Serialize(_data, offset);
                    break;
                case PluginType.MasterEdition:
                    offset += MasterEditionValue.Item1.Serialize(_data, offset);
                    break;
            }

            return offset - initialOffset;
        }

        public PluginType Type { get; set; }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Plugin result)
        {
            int offset = initialOffset;
            result = new Plugin();
            result.Type = (PluginType)_data.GetU8(offset);
            offset += 1;
            switch (result.Type)
            {
                case PluginType.Royalties:
                    {
                        Royalties RoyaltiesItem1;
                        offset += Royalties.Deserialize(_data, offset, out var royaltiesItem1);
                        RoyaltiesItem1 = royaltiesItem1;
                        result.RoyaltiesValue = Tuple.Create(RoyaltiesItem1);
                        break;
                    }

                case PluginType.FreezeDelegate:
                    {
                        FreezeDelegate FreezeDelegateItem1;
                        offset += FreezeDelegate.Deserialize(_data, offset, out var freezeDelegateItem1);
                        FreezeDelegateItem1 = freezeDelegateItem1;
                        result.FreezeDelegateValue = Tuple.Create(FreezeDelegateItem1);
                        break;
                    }

                case PluginType.BurnDelegate:
                    {
                        BurnDelegate BurnDelegateItem1;
                        offset += BurnDelegate.Deserialize(_data, offset, out var burnDelegateItem1);
                        BurnDelegateItem1 = burnDelegateItem1;
                        result.BurnDelegateValue = Tuple.Create(BurnDelegateItem1);
                        break;
                    }

                case PluginType.TransferDelegate:
                    {
                        TransferDelegate TransferDelegateItem1;
                        offset += TransferDelegate.Deserialize(_data, offset, out var transferDelegateItem1);
                        TransferDelegateItem1 = transferDelegateItem1;
                        result.TransferDelegateValue = Tuple.Create(TransferDelegateItem1);
                        break;
                    }

                case PluginType.UpdateDelegate:
                    {
                        UpdateDelegate UpdateDelegateItem1;
                        offset += UpdateDelegate.Deserialize(_data, offset, out var updateDelegateItem1);
                        UpdateDelegateItem1 = updateDelegateItem1;
                        result.UpdateDelegateValue = Tuple.Create(UpdateDelegateItem1);
                        break;
                    }

                case PluginType.PermanentFreezeDelegate:
                    {
                        PermanentFreezeDelegate PermanentFreezeDelegateItem1;
                        offset += PermanentFreezeDelegate.Deserialize(_data, offset, out var permanentFreezeDelegateItem1);
                        PermanentFreezeDelegateItem1 = permanentFreezeDelegateItem1;
                        result.PermanentFreezeDelegateValue = Tuple.Create(PermanentFreezeDelegateItem1);
                        break;
                    }

                case PluginType.Attributes:
                    {
                        Attributes AttributesItem1;
                        offset += Attributes.Deserialize(_data, offset, out var attributesItem1);
                        AttributesItem1 = attributesItem1;
                        result.AttributesValue = Tuple.Create(AttributesItem1);
                        break;
                    }

                case PluginType.PermanentTransferDelegate:
                    {
                        PermanentTransferDelegate PermanentTransferDelegateItem1;
                        offset += PermanentTransferDelegate.Deserialize(_data, offset, out var permanentTransferDelegateItem1);
                        PermanentTransferDelegateItem1 = permanentTransferDelegateItem1;
                        result.PermanentTransferDelegateValue = Tuple.Create(PermanentTransferDelegateItem1);
                        break;
                    }

                case PluginType.PermanentBurnDelegate:
                    {
                        PermanentBurnDelegate PermanentBurnDelegateItem1;
                        offset += PermanentBurnDelegate.Deserialize(_data, offset, out var permanentBurnDelegateItem1);
                        PermanentBurnDelegateItem1 = permanentBurnDelegateItem1;
                        result.PermanentBurnDelegateValue = Tuple.Create(PermanentBurnDelegateItem1);
                        break;
                    }

                case PluginType.Edition:
                    {
                        Edition EditionItem1;
                        offset += Edition.Deserialize(_data, offset, out var editionItem1);
                        EditionItem1 = editionItem1;
                        result.EditionValue = Tuple.Create(EditionItem1);
                        break;
                    }

                case PluginType.MasterEdition:
                    {
                        MasterEdition MasterEditionItem1;
                        offset += MasterEdition.Deserialize(_data, offset, out var masterEditionItem1);
                        MasterEditionItem1 = masterEditionItem1;
                        result.MasterEditionValue = Tuple.Create(MasterEditionItem1);
                        break;
                    }
            }

            return offset - initialOffset;
        }
    }

    public enum RuleSetType : byte
    {
        None,
        ProgramAllowList,
        ProgramDenyList
    }

    public partial class RuleSet
    {
        public Tuple<PublicKey[]> ProgramAllowListValue { get; set; }

        public Tuple<PublicKey[]> ProgramDenyListValue { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)Type, offset);
            offset += 1;
            switch (Type)
            {
                case RuleSetType.ProgramAllowList:
                    _data.WriteS32(ProgramAllowListValue.Item1.Length, offset);
                    offset += 4;
                    foreach (var programAllowListValue in ProgramAllowListValue.Item1)
                        {
                        _data.WritePubKey(programAllowListValue, offset);
                        offset += 32;
                    }

                    break;
                case RuleSetType.ProgramDenyList:
                    _data.WriteS32(ProgramDenyListValue.Item1.Length, offset);
                    offset += 4;
                    foreach (var programDenyListValue in ProgramDenyListValue.Item1)
                        {
                        _data.WritePubKey(programDenyListValue, offset);
                        offset += 32;
                    }

                    break;
            }

            return offset - initialOffset;
        }

        public RuleSetType Type { get; set; }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out RuleSet result)
        {
            int offset = initialOffset;
            result = new RuleSet();
            result.Type = (RuleSetType)_data.GetU8(offset);
            offset += 1;
            switch (result.Type)
            {
                case RuleSetType.ProgramAllowList:
                    {
                        PublicKey[] ProgramAllowListItem1;
                        int programAllowListItem1Length = (int)_data.GetU32(offset);
                        offset += 4;
                        ProgramAllowListItem1 = new PublicKey[programAllowListItem1Length];
                        for (uint programAllowListItem1Idx = 0; programAllowListItem1Idx < programAllowListItem1Length; programAllowListItem1Idx++)
                        {
                            ProgramAllowListItem1[programAllowListItem1Idx] = _data.GetPubKey(offset);
                            offset += 32;
                        }

                        result.ProgramAllowListValue = Tuple.Create(ProgramAllowListItem1);
                        break;
                    }

                case RuleSetType.ProgramDenyList:
                    {
                        PublicKey[] ProgramDenyListItem1;
                        int programDenyListItem1Length = (int)_data.GetU32(offset);
                        offset += 4;
                        ProgramDenyListItem1 = new PublicKey[programDenyListItem1Length];
                        for (uint programDenyListItem1Idx = 0; programDenyListItem1Idx < programDenyListItem1Length; programDenyListItem1Idx++)
                        {
                            ProgramDenyListItem1[programDenyListItem1Idx] = _data.GetPubKey(offset);
                            offset += 32;
                        }

                        result.ProgramDenyListValue = Tuple.Create(ProgramDenyListItem1);
                        break;
                    }
            }

            return offset - initialOffset;
        }
    }

    public enum DataState : byte
    {
        AccountState,
        LedgerState
    }

    public enum AuthorityType : byte
    {
        None,
        Owner,
        UpdateAuthority,
        Address
    }

    public partial class AddressType
    {
        public PublicKey Address { get; set; }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out AddressType result)
        {
            int offset = initialOffset;
            result = new AddressType();
            result.Address = _data.GetPubKey(offset);
            offset += 32;
            return offset - initialOffset;
        }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WritePubKey(Address, offset);
            offset += 32;
            return offset - initialOffset;
        }
    }

    public partial class Authority
    {
        public AddressType AddressValue { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)Type, offset);
            offset += 1;
            switch (Type)
            {
                case AuthorityType.Address:
                    offset += AddressValue.Serialize(_data, offset);
                    break;
            }

            return offset - initialOffset;
        }

        public AuthorityType Type { get; set; }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Authority result)
        {
            int offset = initialOffset;
            result = new Authority();
            result.Type = (AuthorityType)_data.GetU8(offset);
            offset += 1;
            switch (result.Type)
            {
                case AuthorityType.Address:
                    {
                        AddressType tmpAddressValue = new AddressType();
                        offset += AddressType.Deserialize(_data, offset, out tmpAddressValue);
                        result.AddressValue = tmpAddressValue;
                        break;
                    }
            }

            return offset - initialOffset;
        }
    }

    public enum Key : byte
    {
        Uninitialized,
        AssetV1,
        HashedAssetV1,
        PluginHeaderV1,
        PluginRegistryV1,
        CollectionV1
    }

    public enum UpdateAuthorityType : byte
    {
        None,
        Address,
        Collection
    }

    public partial class UpdateAuthority
    {
        public Tuple<PublicKey> AddressValue { get; set; }

        public Tuple<PublicKey> CollectionValue { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)Type, offset);
            offset += 1;
            switch (Type)
            {
                case UpdateAuthorityType.Address:
                    _data.WritePubKey(AddressValue.Item1, offset);
                    offset += 32;
                    break;
                case UpdateAuthorityType.Collection:
                    _data.WritePubKey(CollectionValue.Item1, offset);
                    offset += 32;
                    break;
            }

            return offset - initialOffset;
        }

        public UpdateAuthorityType Type { get; set; }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out UpdateAuthority result)
        {
            int offset = initialOffset;
            result = new UpdateAuthority();
            result.Type = (UpdateAuthorityType)_data.GetU8(offset);
            offset += 1;
            switch (result.Type)
            {
                case UpdateAuthorityType.Address:
                    {
                        PublicKey AddressItem1;
                        AddressItem1 = _data.GetPubKey(offset);
                        offset += 32;
                        result.AddressValue = Tuple.Create(AddressItem1);
                        break;
                    }

                case UpdateAuthorityType.Collection:
                    {
                        PublicKey CollectionItem1;
                        CollectionItem1 = _data.GetPubKey(offset);
                        offset += 32;
                        result.CollectionValue = Tuple.Create(CollectionItem1);
                        break;
                    }
            }

            return offset - initialOffset;
        }
    }
}
