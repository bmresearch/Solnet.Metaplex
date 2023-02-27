using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Hydra.Types
{

    public class ProcessInitAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey HoldingAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey MembershipMint { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ProcessInitForMintAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey FanoutForMint { get; set; }

        public PublicKey MintHoldingAccount { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }
    }

    public class ProcessAddMemberWalletAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey Member { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey MembershipAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ProcessAddMemberNftAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey MembershipAccount { get; set; }

        public PublicKey Mint { get; set; }

        public PublicKey Metadata { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ProcessSetTokenMemberStakeAccounts
    {
        public PublicKey Member { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey MembershipVoucher { get; set; }

        public PublicKey MembershipMint { get; set; }

        public PublicKey MembershipMintTokenAccount { get; set; }

        public PublicKey MemberStakeAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ProcessSetForTokenMemberStakeAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey Member { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey MembershipVoucher { get; set; }

        public PublicKey MembershipMint { get; set; }

        public PublicKey MembershipMintTokenAccount { get; set; }

        public PublicKey MemberStakeAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ProcessDistributeNftAccounts
    {
        public PublicKey Payer { get; set; }

        public PublicKey Member { get; set; }

        public PublicKey MembershipMintTokenAccount { get; set; }

        public PublicKey MembershipKey { get; set; }

        public PublicKey MembershipVoucher { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey HoldingAccount { get; set; }

        public PublicKey FanoutForMint { get; set; }

        public PublicKey FanoutForMintMembershipVoucher { get; set; }

        public PublicKey FanoutMint { get; set; }

        public PublicKey FanoutMintMemberTokenAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ProcessDistributeWalletAccounts
    {
        public PublicKey Payer { get; set; }

        public PublicKey Member { get; set; }

        public PublicKey MembershipVoucher { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey HoldingAccount { get; set; }

        public PublicKey FanoutForMint { get; set; }

        public PublicKey FanoutForMintMembershipVoucher { get; set; }

        public PublicKey FanoutMint { get; set; }

        public PublicKey FanoutMintMemberTokenAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey TokenProgram { get; set; }
    }

    public class ProcessDistributeTokenAccounts
    {
        public PublicKey Payer { get; set; }

        public PublicKey Member { get; set; }

        public PublicKey MembershipMintTokenAccount { get; set; }

        public PublicKey MembershipVoucher { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey HoldingAccount { get; set; }

        public PublicKey FanoutForMint { get; set; }

        public PublicKey FanoutForMintMembershipVoucher { get; set; }

        public PublicKey FanoutMint { get; set; }

        public PublicKey FanoutMintMemberTokenAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey Rent { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey MembershipMint { get; set; }

        public PublicKey MemberStakeAccount { get; set; }
    }

    public class ProcessSignMetadataAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey HoldingAccount { get; set; }

        public PublicKey Metadata { get; set; }
    }

    public class ProcessTransferSharesAccounts
    {
        public PublicKey Authority { get; set; }

        public PublicKey Member { get; set; }

        public PublicKey MembershipKey { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey FromMembershipAccount { get; set; }

        public PublicKey ToMembershipAccount { get; set; }

        public PublicKey Instructions { get; set; }
    }

    public class ProcessUnstakeAccounts
    {
        public PublicKey Member { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey MembershipVoucher { get; set; }

        public PublicKey MembershipMint { get; set; }

        public PublicKey MembershipMintTokenAccount { get; set; }

        public PublicKey MemberStakeAccount { get; set; }

        public PublicKey SystemProgram { get; set; }

        public PublicKey TokenProgram { get; set; }

        public PublicKey Instructions { get; set; }
    }
    public enum HydraErrorKind : uint
    {
    }
    public partial class AddMemberArgs
    {
        public ulong Shares { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU64(Shares, offset);
            offset += 8;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out AddMemberArgs result)
        {
            int offset = initialOffset;
            result = new AddMemberArgs();
            result.Shares = _data.GetU64(offset);
            offset += 8;
            return offset - initialOffset;
        }
    }

    public partial class InitializeFanoutArgs
    {
        public byte BumpSeed { get; set; }

        public byte NativeAccountBumpSeed { get; set; }

        public string Name { get; set; }

        public ulong TotalShares { get; set; }

        public int Serialize(byte[] _data, int initialOffset)
        {
            int offset = initialOffset;
            _data.WriteU8(BumpSeed, offset);
            offset += 1;
            _data.WriteU8(NativeAccountBumpSeed, offset);
            offset += 1;
            offset += _data.WriteBorshString(Name, offset);
            _data.WriteU64(TotalShares, offset);
            offset += 8;
            return offset - initialOffset;
        }

        public static int Deserialize(ReadOnlySpan<byte> _data, int initialOffset, out InitializeFanoutArgs result)
        {
            int offset = initialOffset;
            result = new InitializeFanoutArgs();
            result.BumpSeed = _data.GetU8(offset);
            offset += 1;
            result.NativeAccountBumpSeed = _data.GetU8(offset);
            offset += 1;
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            result.TotalShares = _data.GetU64(offset);
            offset += 8;
            return offset - initialOffset;
        }
    }

    public enum MembershipModel : byte
    {
        Wallet,
        Token,
        NFT
    }
}

