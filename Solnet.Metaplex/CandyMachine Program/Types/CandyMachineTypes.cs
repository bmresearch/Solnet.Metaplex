using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Text;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine.Types
{

    public class InitializeCandyMachineAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class UpdateCandyMachineAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey Wallet { get; set; }
    }

    public class UpdateAuthorityAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey Wallet { get; set; }
    }

    public class AddConfigLinesAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }
    }

    public class SetCollectionAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey CollectionPda { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey Edition { get; set; }

        public PublicKey CollectionAuthorityRecord { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }
    }

    public class RemoveCollectionAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey CollectionPda { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey CollectionAuthorityRecord { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }
    }

    public class MintNftAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey CandyMachineCreator { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey MintAuthority { get; set; }

        public PublicKey UpdateAuthority { get; set; }

        public PublicKey MasterEdition { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey Clock { get; set; }

        public PublicKey RecentBlockhashes { get; set; }

        public PublicKey InstructionSysvarAccount { get; set; }
    }

    public class SetCollectionDuringMintAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey CollectionPda { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey Instructions { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey CollectionMasterEdition { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey CollectionAuthorityRecord { get; set; }
    }

    public class WithdrawFundsAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }
    }

    public class SetFreezeAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey FreezePda { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class RemoveFreezeAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey FreezePda { get; set; }
    }

    public class ThawNftAccounts
    {
        public PublicKey FreezePda { get; set; }

        public PublicKey CandyMachine { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Owner { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey Edition { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class UnlockFundsAccounts
    {
        public PublicKey CandyMachine { get; set; }

        public PublicKey Wallet { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey FreezePda { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public partial class CandyMachineData
    {
        public string Uuid { get; set; }

        public ulong Price { get; set; }

        public string Symbol { get; set; }

        public ushort SellerFeeBasisPoints { get; set; }

        public ulong MaxSupply { get; set; }

        public bool IsMutable { get; set; }

        public bool RetainAuthority { get; set; }

        public long? GoLiveDate { get; set; }

        public EndSettings EndSettings { get; set; }

        public Creator[] Creators { get; set; }

        public HiddenSettings HiddenSettings { get; set; }

        public WhitelistMintSettings WhitelistMintSettings { get; set; }

        public ulong ItemsAvailable { get; set; }

        public GatekeeperConfig Gatekeeper { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += _data.WriteBorshString(Uuid, offset);
            _data.WriteU64(Price, offset);
            offset += 8;
            offset += _data.WriteBorshString(Symbol, offset);
            _data.WriteU16(SellerFeeBasisPoints, offset);
            offset += 2;
            _data.WriteU64(MaxSupply, offset);
            offset += 8;
            _data.WriteBool(IsMutable, offset);
            offset += 1;
            _data.WriteBool(RetainAuthority, offset);
            offset += 1;
            if (GoLiveDate != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteS64(GoLiveDate.Value, offset);
                offset += 8;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (EndSettings != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += EndSettings.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            _data.WriteS32(Creators.Length, offset);
            offset += 4;
            foreach (var creatorsElement in Creators)
            {
                offset += creatorsElement.Serialize(_data, offset);
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

            if (WhitelistMintSettings != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += WhitelistMintSettings.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            _data.WriteU64(ItemsAvailable, offset);
            offset += 8;
            if (Gatekeeper != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += Gatekeeper.Serialize(_data, offset);
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
            offset += _data.GetBorshString(offset, out var resultUuid);
            result.Uuid = resultUuid;
            result.Price = _data.GetU64(offset);
            offset += 8;
            offset += _data.GetBorshString(offset, out var resultSymbol);
            result.Symbol = resultSymbol;
            result.SellerFeeBasisPoints = _data.GetU16(offset);
            offset += 2;
            result.MaxSupply = _data.GetU64(offset);
            offset += 8;
            result.IsMutable = _data.GetBool(offset);
            offset += 1;
            result.RetainAuthority = _data.GetBool(offset);
            offset += 1;
            if (_data.GetBool(offset++))
            {
                result.GoLiveDate = _data.GetS64(offset);
                offset += 8;
            }

            if (_data.GetBool(offset++))
            {
                offset += EndSettings.Deserialize(_data, offset, out var resultEndSettings);
                result.EndSettings = resultEndSettings;
            }

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
                offset += HiddenSettings.Deserialize(_data, offset, out var resultHiddenSettings);
                result.HiddenSettings = resultHiddenSettings;
            }

            if (_data.GetBool(offset++))
            {
                offset += WhitelistMintSettings.Deserialize(_data, offset, out var resultWhitelistMintSettings);
                result.WhitelistMintSettings = resultWhitelistMintSettings;
            }

            result.ItemsAvailable = _data.GetU64(offset);
            offset += 8;
            if (_data.GetBool(offset++))
            {
                offset += GatekeeperConfig.Deserialize(_data, offset, out var resultGatekeeper);
                result.Gatekeeper = resultGatekeeper;
            }

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

    public partial class EndSettings
    {
        public EndSettingType EndSettingType { get; set; }

        public ulong Number { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)EndSettingType, offset);
            offset += 1;
            _data.WriteU64(Number, offset);
            offset += 8;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out EndSettings result)
        {
            int offset = initialOffset;
            result = new EndSettings();
            result.EndSettingType = (EndSettingType)_data.GetU8(offset);
            offset += 1;
            result.Number = _data.GetU64(offset);
            offset += 8;
            return offset - initialOffset;
        }
    }

    public partial class Creator
    {
        public PublicKey Address { get; set; }

        public bool Verified { get; set; }

        public byte Share { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WritePubKey(Address, offset);
            offset += 32;
            _data.WriteBool(Verified, offset);
            offset += 1;
            _data.WriteU8(Share, offset);
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
            result.Share = _data.GetU8(offset);
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

    public partial class WhitelistMintSettings
    {
        public WhitelistMintMode Mode { get; set; }

        public PublicKey Mint { get; set; }

        public bool Presale { get; set; }

        public ulong? DiscountPrice { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)Mode, offset);
            offset += 1;
            _data.WritePubKey(Mint, offset);
            offset += 32;
            _data.WriteBool(Presale, offset);
            offset += 1;
            if (DiscountPrice != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU64(DiscountPrice.Value, offset);
                offset += 8;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out WhitelistMintSettings result)
        {
            int offset = initialOffset;
            result = new WhitelistMintSettings();
            result.Mode = (WhitelistMintMode)_data.GetU8(offset);
            offset += 1;
            result.Mint = _data.GetPubKey(offset);
            offset += 32;
            result.Presale = _data.GetBool(offset);
            offset += 1;
            if (_data.GetBool(offset++))
            {
                result.DiscountPrice = _data.GetU64(offset);
                offset += 8;
            }

            return offset - initialOffset;
        }
    }

    public partial class GatekeeperConfig
    {
        public PublicKey GatekeeperNetwork { get; set; }

        public bool ExpireOnUse { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WritePubKey(GatekeeperNetwork, offset);
            offset += 32;
            _data.WriteBool(ExpireOnUse, offset);
            offset += 1;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out GatekeeperConfig result)
        {
            int offset = initialOffset;
            result = new GatekeeperConfig();
            result.GatekeeperNetwork = _data.GetPubKey(offset);
            offset += 32;
            result.ExpireOnUse = _data.GetBool(offset);
            offset += 1;
            return offset - initialOffset;
        }
    }

    public enum EndSettingType : byte
    {
        Date,
        Amount
    }

    public enum WhitelistMintMode : byte
    {
        BurnEveryTime,
        NeverBurn
    }
}
