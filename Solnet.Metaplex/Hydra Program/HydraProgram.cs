using Solnet.Metaplex.Hydra.Types;
using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Hydra
{
    public static class HydraProgram
    {
        public static TransactionInstruction ProcessInit(ProcessInitAccounts accounts, InitializeFanoutArgs args, MembershipModel model, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Authority, true), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.HoldingAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.Writable(accounts.MembershipMint, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(17091898729950414252UL, offset);
            offset += 8;
            offset += args.Serialize(_data, offset);
            _data.WriteU8((byte)model, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessInitForMint(ProcessInitForMintAccounts accounts, byte bumpSeed, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Authority, true), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.FanoutForMint, false), AccountMeta.Writable(accounts.MintHoldingAccount, false), AccountMeta.ReadOnly(accounts.Mint, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(12259883806397863564UL, offset);
            offset += 8;
            _data.WriteU8(bumpSeed, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessAddMemberWallet(ProcessAddMemberWalletAccounts accounts, AddMemberArgs args, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Authority, true), AccountMeta.ReadOnly(accounts.Member, false), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.MembershipAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(16995588035153955273UL, offset);
            offset += 8;
            offset += args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessAddMemberNft(ProcessAddMemberNftAccounts accounts, AddMemberArgs args, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Authority, true), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.MembershipAccount, false), AccountMeta.ReadOnly(accounts.Mint, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(505292774059933532UL, offset);
            offset += 8;
            offset += args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessSetTokenMemberStake(ProcessSetTokenMemberStakeAccounts accounts, ulong shares, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Member, true), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.MembershipVoucher, false), AccountMeta.Writable(accounts.MembershipMint, false), AccountMeta.Writable(accounts.MembershipMintTokenAccount, false), AccountMeta.Writable(accounts.MemberStakeAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(10302478017813552551UL, offset);
            offset += 8;
            _data.WriteU64(shares, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessSetForTokenMemberStake(ProcessSetForTokenMemberStakeAccounts accounts, ulong shares, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Authority, true), AccountMeta.ReadOnly(accounts.Member, false), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.MembershipVoucher, false), AccountMeta.Writable(accounts.MembershipMint, false), AccountMeta.Writable(accounts.MembershipMintTokenAccount, false), AccountMeta.Writable(accounts.MemberStakeAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(7897712870329559250UL, offset);
            offset += 8;
            _data.WriteU64(shares, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessDistributeNft(ProcessDistributeNftAccounts accounts, bool distributeForMint, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Payer, true), AccountMeta.Writable(accounts.Member, false), AccountMeta.Writable(accounts.MembershipMintTokenAccount, false), AccountMeta.ReadOnly(accounts.MembershipKey, false), AccountMeta.Writable(accounts.MembershipVoucher, false), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.HoldingAccount, false), AccountMeta.Writable(accounts.FanoutForMint, false), AccountMeta.Writable(accounts.FanoutForMintMembershipVoucher, false), AccountMeta.ReadOnly(accounts.FanoutMint, false), AccountMeta.Writable(accounts.FanoutMintMemberTokenAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(11041229315756060780UL, offset);
            offset += 8;
            _data.WriteBool(distributeForMint, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessDistributeWallet(ProcessDistributeWalletAccounts accounts, bool distributeForMint, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Payer, true), AccountMeta.Writable(accounts.Member, false), AccountMeta.Writable(accounts.MembershipVoucher, false), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.HoldingAccount, false), AccountMeta.Writable(accounts.FanoutForMint, false), AccountMeta.Writable(accounts.FanoutForMintMembershipVoucher, false), AccountMeta.ReadOnly(accounts.FanoutMint, false), AccountMeta.Writable(accounts.FanoutMintMemberTokenAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(11796837448697751804UL, offset);
            offset += 8;
            _data.WriteBool(distributeForMint, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessDistributeToken(ProcessDistributeTokenAccounts accounts, bool distributeForMint, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Payer, true), AccountMeta.Writable(accounts.Member, false), AccountMeta.Writable(accounts.MembershipMintTokenAccount, false), AccountMeta.Writable(accounts.MembershipVoucher, false), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.HoldingAccount, false), AccountMeta.Writable(accounts.FanoutForMint, false), AccountMeta.Writable(accounts.FanoutForMintMembershipVoucher, false), AccountMeta.ReadOnly(accounts.FanoutMint, false), AccountMeta.Writable(accounts.FanoutMintMemberTokenAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.Writable(accounts.MembershipMint, false), AccountMeta.Writable(accounts.MemberStakeAccount, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(15309182213261519230UL, offset);
            offset += 8;
            _data.WriteBool(distributeForMint, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessSignMetadata(ProcessSignMetadataAccounts accounts, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Authority, true), AccountMeta.ReadOnly(accounts.Fanout, false), AccountMeta.ReadOnly(accounts.HoldingAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(6431023720485307324UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessTransferShares(ProcessTransferSharesAccounts accounts, ulong shares, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Authority, true), AccountMeta.ReadOnly(accounts.Member, false), AccountMeta.ReadOnly(accounts.MembershipKey, false), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.ReadOnly(accounts.FromMembershipAccount, false), AccountMeta.ReadOnly(accounts.ToMembershipAccount, false), AccountMeta.ReadOnly(accounts.Instructions, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(6276916604536401859UL, offset);
            offset += 8;
            _data.WriteU64(shares, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ProcessUnstake(ProcessUnstakeAccounts accounts, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Member, true), AccountMeta.Writable(accounts.Fanout, false), AccountMeta.Writable(accounts.MembershipVoucher, false), AccountMeta.Writable(accounts.MembershipMint, false), AccountMeta.Writable(accounts.MembershipMintTokenAccount, false), AccountMeta.Writable(accounts.MemberStakeAccount, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.Instructions, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(9605965342803796185UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }
    }
}
