using Solnet.Metaplex.Auctionhouse.Accounts;
using Solnet.Metaplex.Auctionhouse.Errors;
using Solnet.Metaplex.Auctionhouse.Types;
using Solnet.Programs.Abstract;
using Solnet.Programs.Utilities;
using Solnet.Rpc;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Core.Sockets;
using Solnet.Rpc.Types;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solnet.Metaplex.Auctionhouse
{
    public static class AuctionHouseProgram
    {
        public static Solnet.Rpc.Models.TransactionInstruction WithdrawFromFee(WithdrawFromFeeAccounts accounts, ulong amount, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FeeWithdrawalDestination, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(4256943025411772595UL, offset);
            offset += 8;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction WithdrawFromTreasury(WithdrawFromTreasuryAccounts accounts, ulong amount, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TreasuryWithdrawalDestination, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseTreasury, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(12253248092804391936UL, offset);
            offset += 8;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction UpdateAuctionHouse(UpdateAuctionHouseAccounts accounts, ushort? sellerFeeBasisPoints, bool? requiresSignOff, bool? canChangeSalePrice, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NewAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FeeWithdrawalDestination, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TreasuryWithdrawalDestination, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryWithdrawalDestinationOwner, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(15849575501573314388UL, offset);
            offset += 8;
            if (sellerFeeBasisPoints != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU16(sellerFeeBasisPoints.Value, offset);
                offset += 2;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (requiresSignOff != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteBool(requiresSignOff.Value, offset);
                offset += 1;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (canChangeSalePrice != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteBool(canChangeSalePrice.Value, offset);
                offset += 1;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction CreateAuctionHouse(CreateAuctionHouseAccounts accounts, byte bump, byte feePayerBump, byte treasuryBump, ushort sellerFeeBasisPoints, bool requiresSignOff, bool canChangeSalePrice, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FeeWithdrawalDestination, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TreasuryWithdrawalDestination, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryWithdrawalDestinationOwner, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseTreasury, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(17403825381545493213UL, offset);
            offset += 8;
            _data.WriteU8(bump, offset);
            offset += 1;
            _data.WriteU8(feePayerBump, offset);
            offset += 1;
            _data.WriteU8(treasuryBump, offset);
            offset += 1;
            _data.WriteU16(sellerFeeBasisPoints, offset);
            offset += 2;
            _data.WriteBool(requiresSignOff, offset);
            offset += 1;
            _data.WriteBool(canChangeSalePrice, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Buy(BuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.PaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TransferAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(16927863322537952870UL, offset);
            offset += 8;
            _data.WriteU8(tradeStateBump, offset);
            offset += 1;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerBuy(AuctioneerBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.PaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TransferAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(15000699694727129617UL, offset);
            offset += 8;
            _data.WriteU8(tradeStateBump, offset);
            offset += 1;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction PublicBuy(PublicBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.PaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TransferAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(12326578860498506921UL, offset);
            offset += 8;
            _data.WriteU8(tradeStateBump, offset);
            offset += 1;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerPublicBuy(AuctioneerPublicBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.PaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TransferAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(9139261969753436125UL, offset);
            offset += 8;
            _data.WriteU8(tradeStateBump, offset);
            offset += 1;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Cancel(CancelAccounts accounts, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Wallet, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(13753127788127181800UL, offset);
            offset += 8;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerCancel(AuctioneerCancelAccounts accounts, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Wallet, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(15510621914255614405UL, offset);
            offset += 8;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Deposit(DepositAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.PaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TransferAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(13182846803881894898UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerDeposit(AuctioneerDepositAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.PaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TransferAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(9167549250117401167UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction ExecuteSale(ExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Buyer, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Seller, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseTreasury, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FreeTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(442251406432881189UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU8(freeTradeStateBump, offset);
            offset += 1;
            _data.WriteU8(programAsSignerBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction ExecutePartialSale(ExecutePartialSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, ulong? partialOrderSize, ulong? partialOrderPrice, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Buyer, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Seller, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseTreasury, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FreeTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(9640979960313352867UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU8(freeTradeStateBump, offset);
            offset += 1;
            _data.WriteU8(programAsSignerBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            if (partialOrderSize != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU64(partialOrderSize.Value, offset);
                offset += 8;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (partialOrderPrice != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU64(partialOrderPrice.Value, offset);
                offset += 8;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerExecuteSale(AuctioneerExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Buyer, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Seller, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseTreasury, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FreeTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(3828952466324487492UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU8(freeTradeStateBump, offset);
            offset += 1;
            _data.WriteU8(programAsSignerBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerExecutePartialSale(AuctioneerExecutePartialSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, ulong? partialOrderSize, ulong? partialOrderPrice, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Buyer, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Seller, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseTreasury, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BuyerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FreeTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(3897178974466223113UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU8(freeTradeStateBump, offset);
            offset += 1;
            _data.WriteU8(programAsSignerBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            if (partialOrderSize != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU64(partialOrderSize.Value, offset);
                offset += 8;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            if (partialOrderPrice != null)
            {
                _data.WriteU8(1, offset);
                offset += 1;
                _data.WriteU64(partialOrderPrice.Value, offset);
                offset += 8;
            }
            else
            {
                _data.WriteU8(0, offset);
                offset += 1;
            }

            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Sell(SellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FreeSellerTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(12502976635542562355UL, offset);
            offset += 8;
            _data.WriteU8(tradeStateBump, offset);
            offset += 1;
            _data.WriteU8(freeTradeStateBump, offset);
            offset += 1;
            _data.WriteU8(programAsSignerBump, offset);
            offset += 1;
            _data.WriteU64(buyerPrice, offset);
            offset += 8;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerSell(AuctioneerSellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, ulong tokenSize, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Wallet, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.SellerTradeState, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.FreeSellerTradeState, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(13194081782167649531UL, offset);
            offset += 8;
            _data.WriteU8(tradeStateBump, offset);
            offset += 1;
            _data.WriteU8(freeTradeStateBump, offset);
            offset += 1;
            _data.WriteU8(programAsSignerBump, offset);
            offset += 1;
            _data.WriteU64(tokenSize, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Withdraw(WithdrawAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.ReceiptAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(2495396153584390839UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction AuctioneerWithdraw(AuctioneerWithdrawAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.ReceiptAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreasuryMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AtaProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(17056415642336077397UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction CloseEscrowAccount(CloseEscrowAccountAccounts accounts, byte escrowPaymentBump, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Wallet, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.EscrowPaymentAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(3103629459430845137UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction DelegateAuctioneer(DelegateAuctioneerAccounts accounts, AuthorityScope[] scopes, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Authority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(16067626630961214058UL, offset);
            offset += 8;
            _data.WriteS32(scopes.Length, offset);
            offset += 4;
            foreach (var scopesElement in scopes)
            {
                _data.WriteU8((byte)scopesElement, offset);
                offset += 1;
            }

            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction UpdateAuctioneer(UpdateAuctioneerAccounts accounts, AuthorityScope[] scopes, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuctionHouse, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Authority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AhAuctioneerPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(15035329336285658983UL, offset);
            offset += 8;
            _data.WriteS32(scopes.Length, offset);
            offset += 4;
            foreach (var scopesElement in scopes)
            {
                _data.WriteU8((byte)scopesElement, offset);
                offset += 1;
            }

            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction PrintListingReceipt(PrintListingReceiptAccounts accounts, byte receiptBump, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Receipt, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Bookkeeper, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(2000687075873811407UL, offset);
            offset += 8;
            _data.WriteU8(receiptBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction CancelListingReceipt(CancelListingReceiptAccounts accounts, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Receipt, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(818456623680469931UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction PrintBidReceipt(PrintBidReceiptAccounts accounts, byte receiptBump, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Receipt, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Bookkeeper, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(15727767197790697822UL, offset);
            offset += 8;
            _data.WriteU8(receiptBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction CancelBidReceipt(CancelBidReceiptAccounts accounts, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Receipt, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(3148063267756928246UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction PrintPurchaseReceipt(PrintPurchaseReceiptAccounts accounts, byte purchaseReceiptBump, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.PurchaseReceipt, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.ListingReceipt, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.BidReceipt, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Bookkeeper, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Rent, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(10332445790973958883UL, offset);
            offset += 8;
            _data.WriteU8(purchaseReceiptBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }
    }
}
