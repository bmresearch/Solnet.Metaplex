using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Text;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Bubblegum.Types
{

    public class CreateTreeAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey TreeCreator { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class SetTreeDelegateAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey TreeCreator { get; set; }

        public PublicKey NewTreeDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class MintV1Accounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey TreeDelegate { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class MintToCollectionV1Accounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey TreeDelegate { get; set; }

        public PublicKey CollectionAuthority { get; set; }

        public PublicKey CollectionAuthorityRecordPda { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey EditionAccount { get; set; }

        public PublicKey BubblegumSigner { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class VerifyCreatorAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey Creator { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class UnverifyCreatorAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey Creator { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class VerifyCollectionAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey TreeDelegate { get; set; }

        public PublicKey CollectionAuthority { get; set; }

        public PublicKey CollectionAuthorityRecordPda { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey EditionAccount { get; set; }

        public PublicKey BubblegumSigner { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class UnverifyCollectionAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey TreeDelegate { get; set; }

        public PublicKey CollectionAuthority { get; set; }

        public PublicKey CollectionAuthorityRecordPda { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey EditionAccount { get; set; }

        public PublicKey BubblegumSigner { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class SetAndVerifyCollectionAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey TreeDelegate { get; set; }

        public PublicKey CollectionAuthority { get; set; }

        public PublicKey CollectionAuthorityRecordPda { get; set; }

        public PublicKey CollectionMint { get; set; }

        public PublicKey CollectionMetadata { get; set; }

        public PublicKey EditionAccount { get; set; }

        public PublicKey BubblegumSigner { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class TransferAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey NewLeafOwner { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class DelegateAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey PreviousLeafDelegate { get; set; }

        public PublicKey NewLeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class BurnAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class RedeemAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Voucher { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class CancelRedeemAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey Voucher { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
    }

    public class DecompressV1Accounts
    {
        public PublicKey Voucher { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey MintAuthority { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey MasterEdition { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey SysvarRent { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey AssociatedTokenProgram { get; set; }

        public PublicKey LogWrapper { get; set; }
    }

    public class CompressAccounts
    {
        public PublicKey TreeAuthority { get; set; }

        public PublicKey LeafOwner { get; set; }

        public PublicKey LeafDelegate { get; set; }

        public PublicKey MerkleTree { get; set; }

        public PublicKey TokenAccount { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey MasterEdition { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey LogWrapper { get; set; }

        public PublicKey CompressionProgram { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey TokenMetadataProgram { get; set; }

        public PublicKey SystemProgram { get; set; }
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

    public partial class Uses
    {
        public UseMethod UseMethod { get; set; }

        public ulong Remaining { get; set; }

        public ulong Total { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)UseMethod, offset);
            offset += 1;
            _data.WriteU64(Remaining, offset);
            offset += 8;
            _data.WriteU64(Total, offset);
            offset += 8;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Uses result)
        {
            int offset = initialOffset;
            result = new Uses();
            result.UseMethod = (UseMethod)_data.GetU8(offset);
            offset += 1;
            result.Remaining = _data.GetU64(offset);
            offset += 8;
            result.Total = _data.GetU64(offset);
            offset += 8;
            return offset - initialOffset;
        }
    }

    public partial class Collection
    {
        public bool Verified { get; set; }

        public PublicKey Key { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteBool(Verified, offset);
            offset += 1;
            _data.WritePubKey(Key, offset);
            offset += 32;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out Collection result)
        {
            int offset = initialOffset;
            result = new Collection();
            result.Verified = _data.GetBool(offset);
            offset += 1;
            result.Key = _data.GetPubKey(offset);
            offset += 32;
            return offset - initialOffset;
        }
    }

    public partial class MetadataArgs
    {
        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Uri { get; set; }

        public ushort SellerFeeBasisPoints { get; set; }

        public bool PrimarySaleHappened { get; set; }

        public bool IsMutable { get; set; }

        public byte? EditionNonce { get; set; }

        public TokenStandard? TokenStandard { get; set; }

        public Collection Collection { get; set; }

        public Uses Uses { get; set; }

        public TokenProgramVersion TokenProgramVersion { get; set; }

        public Creator[] Creators { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            offset += _data.WriteBorshString(Name, offset);
            offset += _data.WriteBorshString(Symbol, offset);
            offset += _data.WriteBorshString(Uri, offset);
            _data.WriteU16(SellerFeeBasisPoints, offset);
            offset += 2;
            _data.WriteBool(PrimarySaleHappened, offset);
            offset += 1;
            _data.WriteBool(IsMutable, offset);
            offset += 1;
            if (EditionNonce != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU8(EditionNonce.Value, offset);
                offset += 1;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (TokenStandard != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU8((byte)TokenStandard, offset);
                offset += 1;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (Collection != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += Collection.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (Uses != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                offset += Uses.Serialize(_data, offset);
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            _data.WriteU8((byte)TokenProgramVersion, offset);
            offset += 1;
            _data.WriteS32(Creators.Length, offset);
            offset += 4;
            foreach (var creatorsElement in Creators)
            {
                offset += creatorsElement.Serialize(_data, offset);
            }

            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out MetadataArgs result)
        {
            int offset = initialOffset;
            result = new MetadataArgs();
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            offset += _data.GetBorshString(offset, out var resultSymbol);
            result.Symbol = resultSymbol;
            offset += _data.GetBorshString(offset, out var resultUri);
            result.Uri = resultUri;
            result.SellerFeeBasisPoints = _data.GetU16(offset);
            offset += 2;
            result.PrimarySaleHappened = _data.GetBool(offset);
            offset += 1;
            result.IsMutable = _data.GetBool(offset);
            offset += 1;
            if (_data.GetBool(offset++))
            {
                result.EditionNonce = _data.GetU8(offset);
                offset += 1;
            }

            if (_data.GetBool(offset++))
            {
                result.TokenStandard = (TokenStandard)_data.GetU8(offset);
                offset += 1;
            }

            if (_data.GetBool(offset++))
            {
                offset += Collection.Deserialize(_data, offset, out var resultCollection);
                result.Collection = resultCollection;
            }

            if (_data.GetBool(offset++))
            {
                offset += Uses.Deserialize(_data, offset, out var resultUses);
                result.Uses = resultUses;
            }

            result.TokenProgramVersion = (TokenProgramVersion)_data.GetU8(offset);
            offset += 1;
            int resultCreatorsLength = (int)_data.GetU32(offset);
            offset += 4;
            result.Creators = new Creator[resultCreatorsLength];
            for (uint resultCreatorsIdx = 0; resultCreatorsIdx < resultCreatorsLength; resultCreatorsIdx++)
            {
                offset += Creator.Deserialize(_data, offset, out var resultCreatorsresultCreatorsIdx);
                result.Creators[resultCreatorsIdx] = resultCreatorsresultCreatorsIdx;
            }

            return offset - initialOffset;
        }
    }

    public enum LeafSchemaType : byte
    {
        V1
    }

    public partial class V1Type
    {
        public PublicKey Id { get; set; }

        public PublicKey Owner { get; set; }

        public PublicKey Delegate { get; set; }

        public ulong Nonce { get; set; }

        public byte[] DataHash { get; set; }

        public byte[] CreatorHash { get; set; }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out V1Type result)
        {
            int offset = initialOffset;
            result = new V1Type();
            result.Id = _data.GetPubKey(offset);
            offset += 32;
            result.Owner = _data.GetPubKey(offset);
            offset += 32;
            result.Delegate = _data.GetPubKey(offset);
            offset += 32;
            result.Nonce = _data.GetU64(offset);
            offset += 8;
            result.DataHash = _data.GetBytes(offset, 32);
            offset += 32;
            result.CreatorHash = _data.GetBytes(offset, 32);
            offset += 32;
            return offset - initialOffset;
        }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WritePubKey(Id, offset);
            offset += 32;
            _data.WritePubKey(Owner, offset);
            offset += 32;
            _data.WritePubKey(Delegate, offset);
            offset += 32;
            _data.WriteU64(Nonce, offset);
            offset += 8;
            _data.WriteSpan(DataHash, offset);
            offset += DataHash.Length;
            _data.WriteSpan(CreatorHash, offset);
            offset += CreatorHash.Length;
            return offset - initialOffset;
        }
    }

    public partial class LeafSchema
    {
        public V1Type V1Value { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8((byte)Type, offset);
            offset += 1;
            switch (Type)
            {
                case LeafSchemaType.V1:
                    offset += V1Value.Serialize(_data, offset);
                    break;
            }

            return offset - initialOffset;
        }

        public LeafSchemaType Type { get; set; }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out LeafSchema result)
        {
            int offset = initialOffset;
            result = new LeafSchema();
            result.Type = (LeafSchemaType)_data.GetU8(offset);
            offset += 1;
            switch (result.Type)
            {
                case LeafSchemaType.V1:
                    {
                        V1Type tmpV1Value = new V1Type();
                        offset += V1Type.Deserialize(_data, offset, out tmpV1Value);
                        result.V1Value = tmpV1Value;
                        break;
                    }
            }

            return offset - initialOffset;
        }
    }

    public enum TokenProgramVersion : byte
    {
        Original,
        Token2022
    }

    public enum TokenStandard : byte
    {
        NonFungible,
        FungibleAsset,
        Fungible,
        NonFungibleEdition
    }

    public enum UseMethod : byte
    {
        Burn,
        Multiple,
        Single
    }
}
