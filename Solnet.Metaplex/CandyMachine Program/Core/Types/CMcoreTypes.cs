using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Text;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine.Core.Types
{
    public class AddConfigLinesAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }
    }

    public class InitializeAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey AuthorityPda { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMasterEdition { get; set; }

        public PublicKey CollectionUpdateAuthority { get; set; }

        public PublicKey CollectionAuthorityRecord { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class MintAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey AuthorityPda { get; set; }

        public PublicKey MintAuthority { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey NftMint { get; set; }

        public PublicKey NftMintAuthority { get; set; }

        public PublicKey NftMetadata { get; set; }

        public PublicKey NftMasterEdition { get; set; }

        public PublicKey CollectionAuthorityRecord { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey CollectionMasterEdition { get; set; }

        public PublicKey CollectionUpdateAuthority { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey RecentSlothashes { get; set; }
    }

    public class SetAuthorityAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }
    }

    public class SetCoreCollectionAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey AuthorityPda { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey CollectionAuthorityRecord { get; set; }

        public PublicKey NewCollectionUpdateAuthority { get; set; }

        public PublicKey NewCollectionMetadata { get; set; }

        public PublicKey NewCollectionMint { get; set; }

        public PublicKey NewCollectionMasterEdition { get; set; }

        public PublicKey NewCollectionAuthorityRecord { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class SetMintAuthorityAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey MintAuthority { get; set; }
    }

    public class UpdateAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }
    }

    public class WithdrawAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }
    }
    public partial class CandyMachineData
    {
        public ulong ItemsAvailable { get; set; }

        public string Symbol { get; set; }

        public ushort SellerFeeBasisPoints { get; set; }

        public ulong MaxSupply { get; set; }

        public bool IsMutable { get; set; }

        public Creator[] Creators { get; set; }

        public ConfigLineSettings ConfigLineSettings { get; set; }

        public HiddenSettings HiddenSettings { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU64(ItemsAvailable, offset);
            offset += 8;
            offset += _data.WriteBorshString(Symbol, offset);
            _data.WriteU16(SellerFeeBasisPoints, offset);
            offset += 2;
            _data.WriteU64(MaxSupply, offset);
            offset += 8;
            _data.WriteBool(IsMutable, offset);
            offset += 1;
            _data.WriteS32(Creators.Length, offset);
            offset += 4;
            foreach (var creatorsElement in Creators)
            {
                offset += creatorsElement.Serialize(_data, offset);
            }

            if (ConfigLineSettings != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += ConfigLineSettings.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (HiddenSettings != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += HiddenSettings.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out CandyMachineData result)
        {
            int offset = initialOffset;
            result = new CandyMachineData();
            result.ItemsAvailable = _data.GetU64(offset);
            offset += 8;
            offset += _data.GetBorshString(offset, out var resultSymbol);
            result.Symbol = resultSymbol;
            result.SellerFeeBasisPoints = _data.GetU16(offset);
            offset += 2;
            result.MaxSupply = _data.GetU64(offset);
            offset += 8;
            result.IsMutable = _data.GetBool(offset);
            offset += 1;
            int resultCreatorsLength = (int)_data.GetU32(offset);
            offset += 4;
            result.Creators = new Creator[resultCreatorsLength];
            for (uint resultCreatorsIdx = 0; resultCreatorsIdx < resultCreatorsLength; resultCreatorsIdx++)
            {
                offset += Creator.Deserialize(_data, offset, out var resultCreatorsresultCreatorsIdx);
                result.Creators[resultCreatorsIdx] = resultCreatorsresultCreatorsIdx;
            }

            if (_data.GetBool(offset++))
            {
                offset += ConfigLineSettings.Deserialize(_data, offset, out var resultConfigLineSettings);
                result.ConfigLineSettings = resultConfigLineSettings;
            }

            if (_data.GetBool(offset++))
            {
                offset += HiddenSettings.Deserialize(_data, offset, out var resultHiddenSettings);
                result.HiddenSettings = resultHiddenSettings;
            }

            return offset - initialOffset;
        }
    }

    public partial class Creator
    {
        public PublicKey Address { get; set; }

        public bool Verified { get; set; }

        public byte PercentageShare { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WritePubKey(Address, offset);
            offset += 32;
            _data.WriteBool(Verified, offset);
            offset += 1;
            _data.WriteU8(PercentageShare, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Creator result)
        {
            int offset = initialOffset;
            result = new Creator();
            result.Address = _data.GetPubKey(offset);
            offset += 32;
            result.Verified = _data.GetBool(offset);
            offset += 1;
            result.PercentageShare = _data.GetU8(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class HiddenSettings
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public byte[] Hash { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += _data.WriteBorshString(Name, offset);
            offset += _data.WriteBorshString(Uri, offset);
            _data.WriteSpan(Hash, offset);
            offset += Hash.Length;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out HiddenSettings result)
        {
            int offset = initialOffset;
            result = new HiddenSettings();
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            offset += _data.GetBorshString(offset, out var resultUri);
            result.Uri = resultUri;
            result.Hash = _data.GetBytes(offset, 32);
            offset += 32;
            return offset - initialOffset;
        }
    }

    public partial class ConfigLineSettings
    {
        public string PrefixName { get; set; }

        public uint NameLength { get; set; }

        public string PrefixUri { get; set; }

        public uint UriLength { get; set; }

        public bool IsSequential { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += _data.WriteBorshString(PrefixName, offset);
            _data.WriteU32(NameLength, offset);
            offset += 4;
            offset += _data.WriteBorshString(PrefixUri, offset);
            _data.WriteU32(UriLength, offset);
            offset += 4;
            _data.WriteBool(IsSequential, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ConfigLineSettings result)
        {
            int offset = initialOffset;
            result = new ConfigLineSettings();
            offset += _data.GetBorshString(offset, out var resultPrefixName);
            result.PrefixName = resultPrefixName;
            result.NameLength = _data.GetU32(offset);
            offset += 4;
            offset += _data.GetBorshString(offset, out var resultPrefixUri);
            result.PrefixUri = resultPrefixUri;
            result.UriLength = _data.GetU32(offset);
            offset += 4;
            result.IsSequential = _data.GetBool(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public partial class ConfigLine
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += _data.WriteBorshString(Name, offset);
            offset += _data.WriteBorshString(Uri, offset);
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out ConfigLine result)
        {
            int offset = initialOffset;
            result = new ConfigLine();
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            offset += _data.GetBorshString(offset, out var resultUri);
            result.Uri = resultUri;
            return offset - initialOffset;
        }
    }
}

