using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;

namespace Solnet.Metaplex.NFT.Library

{    /// <summary>
     /// ProgrammableConfig class
     /// </summary>
    public class ProgrammableConfig
    {
        /// <summary>
        /// ProgrammableConfig Ruleset public key.
        /// </summary>
        public PublicKey key;


        /// <summary>
        ///  ProgrammableConfig data byte length
        /// </summary>
        public static int length = 32;

        /// <summary>
        /// ProgrammableConfig Constructor
        /// </summary>
        /// <param name="key"> Ruleset Mint Address/Public Key</param>
        public ProgrammableConfig(PublicKey key)
        {
            this.key = key;

        }
        /// <summary>
        ///  Encode ProgrammableConfig data ( serialize ).
        /// </summary>
        /// <returns></returns>
        public byte[] Encode()
        {
            byte[] encodedBuffer = new byte[length];
            encodedBuffer.WritePubKey(key, 0);

            return encodedBuffer;
        }
    }


    /// <summary>
    /// Uses class - Consumable NFT
    /// </summary>
    public class Uses
    {

        /// <summary>
        ///  useMethod Enum (0 = burn, 1 = multiple, 2 = single)
        /// </summary>
        public byte useMethod;

        /// <summary>
        ///  remaining uses - usually the same as total on creation
        /// </summary>
        public string remaining;

        /// <summary>
        ///  total NFT uses
        /// </summary>
        public string total;

        /// <summary>
        ///  usage length
        /// </summary>
        public static int length = 17;

        /// <summary>
        /// Uses Constructor
        /// </summary>
        /// <param name="_useMethod"> useMethod - 0 = burn, 1 = multiple, 2 = single</param>
        /// <param name="_remaining"> remaining</param>
        /// <param name="_total"> total</param>
        public Uses(UseMethod _useMethod, int _remaining, int _total)
        {
            useMethod = (byte)_useMethod;
            remaining = _remaining.ToString();
            total = _total.ToString();
        }
        /// <summary>
        ///  Encode Usage data ( serialize ).
        /// </summary>
        /// <returns></returns>
        public byte[] Encode()
        {
            byte[] encodedBuffer = new byte[length];

            encodedBuffer.WriteSpan(new byte[] { useMethod }, 0);
            encodedBuffer.WriteU64(Convert.ToUInt64(remaining), 1);
            encodedBuffer.WriteU64(Convert.ToUInt64(total), 9);
            return encodedBuffer;
        }
    }
    /// <summary>
    /// Collection class
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Collection public key.
        /// </summary>
        public PublicKey key;

        /// <summary>
        ///  Did the collection sign?
        /// </summary>
        public bool verified;


        /// <summary>
        ///  Collection data byte length in an account data.
        /// </summary>
        public static int length = 33;

        /// <summary>
        ///  Collection constructor.
        /// </summary>
        /// <param name="key"> Public key of the collection</param>
        /// <param name="verified"> Did the collection sign?</param>/
        public Collection(PublicKey key, bool verified = false)
        {
            this.key = key;
            this.verified = verified;

        }

        /// <summary>
        ///  Construct a Collection from a byte array ( deserialize ).
        /// </summary>
        /// <param name="encoded"></param>
        public Collection(ReadOnlySpan<byte> encoded)
        {
            this.key = encoded.GetPubKey(1);
            this.verified = Convert.ToBoolean(encoded.GetU8(0));

        }

        /// <summary>
        ///  Encode Collection data ( serialize ).
        /// </summary>
        /// <returns></returns>
        public byte[] Encode()
        {
            byte[] encodedBuffer = new byte[length];

            encodedBuffer.WriteU8(Convert.ToByte(verified), 0);
            encodedBuffer.WritePubKey(key, 1);

            return encodedBuffer;
        }
    }

    /// <summary>
    /// Creator class.
    /// </summary>
    public class Creator
    {
        /// <summary>
        /// Creators public key.
        /// </summary>
        public PublicKey key;

        /// <summary>
        ///  Did the creator sign?
        /// </summary>
        public bool verified;

        /// <summary>
        /// Creators share in percentages.
        /// </summary>
        public byte share;

        /// <summary>
        ///  Creator data byte lenght in an account data.
        /// </summary>
        public static int length = 34;

        /// <summary>
        ///  Creator constructor.
        /// </summary>
        /// <param name="key"> Public key of the creator</param>
        /// <param name="share"> Creators share in percentages</param>
        /// <param name="verified"> Did the creator sign?</param>/
        public Creator(PublicKey key, byte share, bool verified = false)
        {
            this.key = key;
            this.verified = verified;
            this.share = share;
        }

        /// <summary>
        ///  Construct a Creator from a byte array ( deserialize ).
        /// </summary>
        /// <param name="encoded"></param>
        public Creator(ReadOnlySpan<byte> encoded)
        {
            this.key = encoded.GetPubKey(0);
            this.verified = Convert.ToBoolean(encoded.GetU8(32));
            this.share = encoded.GetU8(33);
        }

        /// <summary>
        ///  Encode Creators data ( serialize ).
        /// </summary>
        /// <returns></returns>
        public byte[] Encode()
        {
            byte[] encodedBuffer = new byte[34];

            encodedBuffer.WritePubKey(key, 0);
            encodedBuffer.WriteU8(Convert.ToByte(verified), 32);
            encodedBuffer.WriteU8((byte)share, 33);

            return encodedBuffer;
        }
    }


    /// <summary>Dynamic Metadata Class thats supports all 3 metadata versions</summary>
    public class OnChainData
    {
        /// <summary>
        /// Metadata Version
        /// </summary>
        public MetadataVersion version;
        /// <summary> name </summary>
        public string name;
        /// <summary> short symbol </summary>
        public string symbol;
        /// <summary> uri of metadata </summary>
        public string uri;
        /// <summary> Seller cut </summary>
        public uint sellerFeeBasisPoints;
        /// <summary> Has Creators </summary>
        public bool hasCreators;
        /// <summary> Creators array </summary>
        public IList<Creator> creators { get; set; }
        ///<summary> Collection link </summary>
        public Collection collectionLink { get; set; }
        ///<summary> USEs </summary>
        public Uses uses { get; set; }
        ///<summary> Program ruleset </summary>
        public ProgrammableConfig programmableConfig { get; set; }
        ///<summary> isMutable </summary>
        public bool isMutable;
        ///<summary> Edition Type </summary>
        public int editionNonce;
        ///<summary> Token Standard - Fungible / non-fungible </summary>
        public int tokenStandard;
        ///<summary> metadata json </summary>
        public string metadata;

        /// <summary> Constructor </summary>
        public OnChainData(string _name, string _symbol, string _uri, uint _sellerFee, IList<Creator> _creators, int _editionNonce, int _tokenStandard, Collection _collection, Uses useInfo, ProgrammableConfig programmableconfig, bool _isMutable)
        {
            name = _name;
            symbol = _symbol;
            uri = _uri;
            sellerFeeBasisPoints = _sellerFee;
            creators = _creators;
            collectionLink = _collection;
            uses = useInfo;
            isMutable = _isMutable;
            editionNonce = _editionNonce;
            tokenStandard = _tokenStandard;
            programmableConfig = programmableconfig;
            hasCreators = _creators?.Count > 0;
        }
    }

    /// <summary>
    /// Metadata V3 Data class for instructions
    /// </summary>
    public class Metadata
    {
        /// <summary>  Name or discription. Max 32 bytes. </summary>
        public string name;
        /// <summary>  Symbol. Max 10 bytes. </summary>
        public string symbol;
        /// <summary>  Uri. Max 100 bytes. </summary>
        public string uri;
        /// <summary>  Seller fee basis points for secondary sales. </summary>
        public uint sellerFeeBasisPoints;
        /// <summary>  List of creators. </summary>
        public List<Creator> creators { get; set; }
        /// <summary>  Collection Address and Verification </summary>
        public Collection collection { get; set; }
        /// <summary> NFT Uses </summary>
        public Uses uses { get; set; }

        /// <summary>ProgrammableConfig</summary>
        public ProgrammableConfig programmableConfig { get; set; }
    }
    /// <summary>
    /// Use method function type
    /// </summary>
    public enum UseMethod
    {
        /// <summary>
        /// Burn Usage Function
        /// </summary>
        Burn,
        /// <summary>
        /// Multi-Usage Function
        /// </summary>
        Multiple,
        /// <summary>
        /// Single-Usage Function
        /// </summary>
        Single
    }

    /// <summary>
    /// Metadata Versions - Used to override the version when creating the metadata. Use the latest version when minting a new collection
    /// </summary>
    public enum MetadataVersion
    {
        /// <summary>  Original metadata version 1 </summary>
        V1,
        /// <summary>  Enhanced metadata version 3 </summary>
        V3,
        /// <summary>  Programmabled metadata version 4 </summary>
        V4
    }

    /// <summary>
    /// Metadata Versions - Used to override the version when creating the metadata. Use the latest version when minting a new collection
    /// </summary>
    public enum TokenStandard
    {
        /// <summary>  This is a master edition.</summary>
        NonFungible,
        /// <summary> A token with metadata that can also have attrributes.</summary>
        FungibleAsset,
        /// <summary>  A token with simple metadata.</summary>
        Fungible,
        /// <summary> This is a limited edition.</summary>
        NonFungibleEdition,
        /// <summary>  [NEW] An NFT with customizale behaviour for lifecycle events
        /// (e.g. transfers, updates, etc.). </summary>
        ProgrammableNonFungible,
    }
    /// <summary>
    /// A dictionary collection with all the instructions defined
    /// </summary>
    public class MetadataInstructionBook
    {
        /// <summary>
        /// Represents the user-friendly names for the instruction discriminator types for the <see cref="MetadataProgram"/>.
        /// </summary>
        internal static readonly Dictionary<InstructionID, string> Names = new()
        {
            { InstructionID.CreateMetadataAccount, "Create MetadataAccount" },
            { InstructionID.UpdateMetadataAccount, "Update MetadataAccount" },
            { InstructionID.DeprecatedCreateMasterEdition, "Create MasterEdition (deprecated) " },
            { InstructionID.DeprecatedMintNewEditionFromMasterEditionViaPrintingToken, "Mint new Edition from MasterEdition via PrintingToken (deprecated)" },
            { InstructionID.UpdatePrimarySaleHappenedViaToken, "Update PrimarySaleHappened" },
            { InstructionID.DeprecatedSetReservationList, "Set ReservationList (deprecated)" },
            { InstructionID.DeprecatedCreateReservationList, "Create Reservation List (deprecated)" },
            { InstructionID.SignMetadata, "Sign Metadata" },
            { InstructionID.DeprecatedMintPrintingTokensViaToken, "Mint PrintingTokens via token (deprecated)" },
            { InstructionID.DeprecatedMintPrintingTokens, "Mint PrintingTokens (deprecated)" },
            { InstructionID.CreateMasterEdition, "Create MasterEdition" },
            { InstructionID.MintNewEditionFromMasterEditionViaToken, "Mint new Edition from MasterEdition via token" },
            { InstructionID.ConvertMasterEditionV1ToV2, "Convert Master Edition from V1 to V2" },
            { InstructionID.MintNewEditionFromMasterEditionViaVaultProxy, "Mint new Edition from MasterEdition via VaultProxy" },
            { InstructionID.PuffMetadata, "Puff metadata" },
            { InstructionID.OmniCreate, "Create MetadataAccount that supports pNFTS" },
            { InstructionID.OmniMint, "Mint Tokens that supports pNFTs" },
            { InstructionID.VerifyV4, "Verify V4 metadata" },
        };
    }
    /// <summary>
    /// Represents the instruction types for the <see cref="MetadataProgram"/>. Values are defined by the discriminator of each instruction labeled in Metaplex Docs.
    /// </summary>
    internal enum InstructionID : byte
    {
        /// <summary>
        /// 
        /// </summary>
        CreateMetadataAccount = 0,

        /// <summary>
        /// 
        /// </summary>
        UpdateMetadataAccount = 1,

        /// <summary>
        ///
        /// </summary>
        DeprecatedCreateMasterEdition = 2,

        /// <summary>
        /// 
        /// </summary>
        DeprecatedMintNewEditionFromMasterEditionViaPrintingToken = 3,

        /// <summary>
        /// 
        /// </summary>
        UpdatePrimarySaleHappenedViaToken = 4,

        /// <summary>
        /// 
        /// </summary>
        DeprecatedSetReservationList = 5,

        /// <summary>
        /// 
        /// </summary>
        DeprecatedCreateReservationList = 6,

        /// <summary>
        /// 
        /// </summary>
        SignMetadata = 7,

        /// <summary>
        /// 
        /// </summary>
        DeprecatedMintPrintingTokensViaToken = 8,
        /// <summary>
        /// 
        /// </summary>
        DeprecatedMintPrintingTokens = 9,

        /// <summary>
        /// 
        /// </summary>
        CreateMasterEdition = 10,

        /// <summary>
        /// 
        /// </summary>
        MintNewEditionFromMasterEditionViaToken = 11,

        /// <summary>
        /// 
        /// </summary>
        ConvertMasterEditionV1ToV2 = 12,

        /// <summary>
        /// 
        /// </summary>
        MintNewEditionFromMasterEditionViaVaultProxy = 13,

        /// <summary>
        /// 
        /// </summary>
        PuffMetadata = 14,

        /// <summary>
        /// 
        /// </summary>
        CreateMetadataAccountV3 = 33,

        /// <summary>
        /// Omni Create Instruction
        /// </summary>
        OmniCreate = 42,
        /// <summary>
        /// Omni Mint Instruction
        /// </summary>
        OmniMint = 43,
        /// <summary>
        /// Verify Instruction V4
        /// </summary>
        VerifyV4 = 44
    }
    /// <summary>
    /// Delegate Role Variants
    /// </summary>
    public enum MetadataDelegateRole
    {
        /// <summary>
        /// Authority Role
        /// </summary>
        Authority,
        /// <summary>
        /// Collection Role
        /// </summary>
        Collection,
        /// <summary>
        /// Use Role
        /// </summary>
        Use,
        /// <summary>
        /// Update Role
        /// </summary>
        Update
    }
}
