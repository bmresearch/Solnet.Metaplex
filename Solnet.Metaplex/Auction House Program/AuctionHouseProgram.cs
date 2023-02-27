#pragma warning disable CS1591
using Solnet.Metaplex.Auctionhouse.Types;
using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Collections.Generic;

namespace Solnet.Metaplex.Auctionhouse
{
    public static class AuctionHouseProgram
    {
        public static TransactionInstruction WithdrawFromFee(WithdrawFromFeeAccounts accounts, ulong amount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Authority, true), AccountMeta.Writable(accounts.FeeWithdrawalDestination, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.AuctionHouse, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(4256943025411772595UL, offset);
            offset += 8;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction WithdrawFromTreasury(WithdrawFromTreasuryAccounts accounts, ulong amount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Authority, true), AccountMeta.Writable(accounts.TreasuryWithdrawalDestination, false), AccountMeta.Writable(accounts.AuctionHouseTreasury, false), AccountMeta.Writable(accounts.AuctionHouse, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(12253248092804391936UL, offset);
            offset += 8;
            _data.WriteU64(amount, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction UpdateAuctionHouse(UpdateAuctionHouseAccounts accounts, ushort? sellerFeeBasisPoints, bool? requiresSignOff, bool? canChangeSalePrice, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Payer, true), AccountMeta.ReadOnly(accounts.Authority, true), AccountMeta.ReadOnly(accounts.NewAuthority, false), AccountMeta.Writable(accounts.FeeWithdrawalDestination, false), AccountMeta.Writable(accounts.TreasuryWithdrawalDestination, false), AccountMeta.ReadOnly(accounts.TreasuryWithdrawalDestinationOwner, false), AccountMeta.Writable(accounts.AuctionHouse, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction CreateAuctionHouse(CreateAuctionHouseAccounts accounts, byte bump, byte feePayerBump, byte treasuryBump, ushort sellerFeeBasisPoints, bool requiresSignOff, bool canChangeSalePrice, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.Writable(accounts.Payer, true), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.Writable(accounts.FeeWithdrawalDestination, false), AccountMeta.Writable(accounts.TreasuryWithdrawalDestination, false), AccountMeta.ReadOnly(accounts.TreasuryWithdrawalDestinationOwner, false), AccountMeta.Writable(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.AuctionHouseTreasury, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction Buy(BuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerBuy(AuctioneerBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction PublicBuy(PublicBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerPublicBuy(AuctioneerPublicBuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction Cancel(CancelAccounts accounts, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Wallet, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.TradeState, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerCancel(AuctioneerCancelAccounts accounts, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Wallet, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.TradeState, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction Deposit(DepositAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerDeposit(AuctioneerDepositAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ExecuteSale(ExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Buyer, false), AccountMeta.Writable(accounts.Seller, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.AuctionHouseTreasury, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeTradeState, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ExecutePartialSale(ExecutePartialSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, ulong? partialOrderSize, ulong? partialOrderPrice, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Buyer, false), AccountMeta.Writable(accounts.Seller, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.AuctionHouseTreasury, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeTradeState, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerExecuteSale(AuctioneerExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Buyer, false), AccountMeta.Writable(accounts.Seller, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.AuctionHouseTreasury, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeTradeState, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerExecutePartialSale(AuctioneerExecutePartialSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, ulong? partialOrderSize, ulong? partialOrderPrice, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Buyer, false), AccountMeta.Writable(accounts.Seller, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.AuctionHouseTreasury, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeTradeState, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction Sell(SellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeSellerTradeState, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerSell(AuctioneerSellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, ulong tokenSize, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Wallet, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeSellerTradeState, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction Withdraw(WithdrawAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, false), AccountMeta.Writable(accounts.ReceiptAccount, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AuctioneerWithdraw(AuctioneerWithdrawAccounts accounts, byte escrowPaymentBump, ulong amount, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, false), AccountMeta.Writable(accounts.ReceiptAccount, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction CloseEscrowAccount(CloseEscrowAccountAccounts accounts, byte escrowPaymentBump, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(3103629459430845137UL, offset);
            offset += 8;
            _data.WriteU8(escrowPaymentBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction DelegateAuctioneer(DelegateAuctioneerAccounts accounts, AuthorityScope[] scopes, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.Authority, true), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.Writable(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction UpdateAuctioneer(UpdateAuctioneerAccounts accounts, AuthorityScope[] scopes, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.Authority, true), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.Writable(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
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
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction PrintListingReceipt(PrintListingReceiptAccounts accounts, byte receiptBump, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Receipt, false), AccountMeta.Writable(accounts.Bookkeeper, true), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(2000687075873811407UL, offset);
            offset += 8;
            _data.WriteU8(receiptBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction CancelListingReceipt(CancelListingReceiptAccounts accounts, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Receipt, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(818456623680469931UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction PrintBidReceipt(PrintBidReceiptAccounts accounts, byte receiptBump, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Receipt, false), AccountMeta.Writable(accounts.Bookkeeper, true), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(15727767197790697822UL, offset);
            offset += 8;
            _data.WriteU8(receiptBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction CancelBidReceipt(CancelBidReceiptAccounts accounts, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Receipt, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(3148063267756928246UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction PrintPurchaseReceipt(PrintPurchaseReceiptAccounts accounts, byte purchaseReceiptBump, PublicKey programId)
        {
            List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.PurchaseReceipt, false), AccountMeta.Writable(accounts.ListingReceipt, false), AccountMeta.Writable(accounts.BidReceipt, false), AccountMeta.Writable(accounts.Bookkeeper, true), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false), AccountMeta.ReadOnly(accounts.Instruction, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(10332445790973958883UL, offset);
            offset += 8;
            _data.WriteU8(purchaseReceiptBump, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }
    }
}
