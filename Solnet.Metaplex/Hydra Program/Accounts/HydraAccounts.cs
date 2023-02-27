using Solnet.Metaplex.Hydra.Types;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Hydra.Accounts
{
    public partial class Fanout
    {
        public static ulong ACCOUNT_DISCRIMINATOR => 11262111641372878244UL;
        public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 164, 101, 210, 92, 222, 14, 75, 156 };
        public static string ACCOUNT_DISCRIMINATOR_B58 => "UVs7a3vhzRR";
        public PublicKey Authority { get; set; }

        public string Name { get; set; }

        public PublicKey AccountKey { get; set; }

        public ulong TotalShares { get; set; }

        public ulong TotalMembers { get; set; }

        public ulong TotalInflow { get; set; }

        public ulong LastSnapshotAmount { get; set; }

        public byte BumpSeed { get; set; }

        public byte AccountOwnerBumpSeed { get; set; }

        public ulong TotalAvailableShares { get; set; }

        public MembershipModel MembershipModel { get; set; }

        public PublicKey MembershipMint { get; set; }

        public ulong? TotalStakedShares { get; set; }

        public static Fanout Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != ACCOUNT_DISCRIMINATOR)
            {
                return null;
            }

            Fanout result = new Fanout();
            result.Authority = _data.GetPubKey(offset);
            offset += 32;
            offset += _data.GetBorshString(offset, out var resultName);
            result.Name = resultName;
            result.AccountKey = _data.GetPubKey(offset);
            offset += 32;
            result.TotalShares = _data.GetU64(offset);
            offset += 8;
            result.TotalMembers = _data.GetU64(offset);
            offset += 8;
            result.TotalInflow = _data.GetU64(offset);
            offset += 8;
            result.LastSnapshotAmount = _data.GetU64(offset);
            offset += 8;
            result.BumpSeed = _data.GetU8(offset);
            offset += 1;
            result.AccountOwnerBumpSeed = _data.GetU8(offset);
            offset += 1;
            result.TotalAvailableShares = _data.GetU64(offset);
            offset += 8;
            result.MembershipModel = (MembershipModel)_data.GetU8(offset);
            offset += 1;
            if (_data.GetBool(offset++))
            {
                result.MembershipMint = _data.GetPubKey(offset);
                offset += 32;
            }

            if (_data.GetBool(offset++))
            {
                result.TotalStakedShares = _data.GetU64(offset);
                offset += 8;
            }

            return result;
        }
    }

    public partial class FanoutMint
    {
        public static ulong ACCOUNT_DISCRIMINATOR => 15635030446569071666UL;
        public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 50, 164, 42, 108, 90, 201, 250, 216 };
        public static string ACCOUNT_DISCRIMINATOR_B58 => "9UHTdNxQjN7";
        public PublicKey Mint { get; set; }

        public PublicKey Fanout { get; set; }

        public PublicKey TokenAccount { get; set; }

        public ulong TotalInflow { get; set; }

        public ulong LastSnapshotAmount { get; set; }

        public byte BumpSeed { get; set; }

        public static FanoutMint Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != ACCOUNT_DISCRIMINATOR)
            {
                return null;
            }

            FanoutMint result = new FanoutMint();
            result.Mint = _data.GetPubKey(offset);
            offset += 32;
            result.Fanout = _data.GetPubKey(offset);
            offset += 32;
            result.TokenAccount = _data.GetPubKey(offset);
            offset += 32;
            result.TotalInflow = _data.GetU64(offset);
            offset += 8;
            result.LastSnapshotAmount = _data.GetU64(offset);
            offset += 8;
            result.BumpSeed = _data.GetU8(offset);
            offset += 1;
            return result;
        }
    }

    public partial class FanoutMembershipVoucher
    {
        public static ulong ACCOUNT_DISCRIMINATOR => 9057475975415742137UL;
        public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 185, 62, 74, 60, 105, 158, 178, 125 };
        public static string ACCOUNT_DISCRIMINATOR_B58 => "Xz6MLQQgWtt";
        public PublicKey Fanout { get; set; }

        public ulong TotalInflow { get; set; }

        public ulong LastInflow { get; set; }

        public byte BumpSeed { get; set; }

        public PublicKey MembershipKey { get; set; }

        public ulong Shares { get; set; }

        public static FanoutMembershipVoucher Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != ACCOUNT_DISCRIMINATOR)
            {
                return null;
            }

            FanoutMembershipVoucher result = new FanoutMembershipVoucher();
            result.Fanout = _data.GetPubKey(offset);
            offset += 32;
            result.TotalInflow = _data.GetU64(offset);
            offset += 8;
            result.LastInflow = _data.GetU64(offset);
            offset += 8;
            result.BumpSeed = _data.GetU8(offset);
            offset += 1;
            result.MembershipKey = _data.GetPubKey(offset);
            offset += 32;
            result.Shares = _data.GetU64(offset);
            offset += 8;
            return result;
        }
    }

    public partial class FanoutMembershipMintVoucher
    {
        public static ulong ACCOUNT_DISCRIMINATOR => 13078016346526458297UL;
        public static ReadOnlySpan<byte> ACCOUNT_DISCRIMINATOR_BYTES => new byte[] { 185, 33, 118, 173, 147, 114, 126, 181 };
        public static string ACCOUNT_DISCRIMINATOR_B58 => "XxzzXkeD1WL";
        public PublicKey Fanout { get; set; }

        public PublicKey FanoutMint { get; set; }

        public ulong LastInflow { get; set; }

        public byte BumpSeed { get; set; }

        public static FanoutMembershipMintVoucher Deserialize(ReadOnlySpan<byte> _data)
        {
            int offset = 0;
            ulong accountHashValue = _data.GetU64(offset);
            offset += 8;
            if (accountHashValue != ACCOUNT_DISCRIMINATOR)
            {
                return null;
            }

            FanoutMembershipMintVoucher result = new FanoutMembershipMintVoucher();
            result.Fanout = _data.GetPubKey(offset);
            offset += 32;
            result.FanoutMint = _data.GetPubKey(offset);
            offset += 32;
            result.LastInflow = _data.GetU64(offset);
            offset += 8;
            result.BumpSeed = _data.GetU8(offset);
            offset += 1;
            return result;
        }
    }
}

