#pragma warning disable CS1591
using Solnet.Metaplex.Auctioneer.Types;
using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Collections.Generic;

namespace Solnet.Metaplex.Auctioneer
{
    public static class AuctioneerProgram
        {
            public static TransactionInstruction Authorize(AuthorizeAccounts accounts, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.Writable(accounts.Wallet, true), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctioneerAuthority, false), AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(8678869534140449197UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static TransactionInstruction Withdraw(WithdrawAccounts accounts, byte escrowPaymentBump, byte auctioneerAuthorityBump, ulong amount, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.AuctionHouseProgram, false), AccountMeta.ReadOnly(accounts.Wallet, false), AccountMeta.Writable(accounts.ReceiptAccount, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(2495396153584390839UL, offset);
                offset += 8;
                _data.WriteU8(escrowPaymentBump, offset);
                offset += 1;
                _data.WriteU8(auctioneerAuthorityBump, offset);
                offset += 1;
                _data.WriteU64(amount, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static TransactionInstruction Deposit(DepositAccounts accounts, byte escrowPaymentBump, byte auctioneerAuthorityBump, ulong amount, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.AuctionHouseProgram, false), AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13182846803881894898UL, offset);
                offset += 8;
                _data.WriteU8(escrowPaymentBump, offset);
                offset += 1;
                _data.WriteU8(auctioneerAuthorityBump, offset);
                offset += 1;
                _data.WriteU64(amount, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static TransactionInstruction Cancel(CancelAccounts accounts, byte auctioneerAuthorityBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.AuctionHouseProgram, false), AccountMeta.Writable(accounts.ListingConfig, false), AccountMeta.ReadOnly(accounts.Seller, false), AccountMeta.Writable(accounts.Wallet, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.TradeState, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13753127788127181800UL, offset);
                offset += 8;
                _data.WriteU8(auctioneerAuthorityBump, offset);
                offset += 1;
                _data.WriteU64(buyerPrice, offset);
                offset += 8;
                _data.WriteU64(tokenSize, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static TransactionInstruction ExecuteSale(ExecuteSaleAccounts accounts, byte escrowPaymentBump, byte freeTradeStateBump, byte programAsSignerBump, byte auctioneerAuthorityBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.AuctionHouseProgram, false), AccountMeta.Writable(accounts.ListingConfig, false), AccountMeta.Writable(accounts.Buyer, false), AccountMeta.Writable(accounts.Seller, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.TokenMint, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.Writable(accounts.SellerPaymentReceiptAccount, false), AccountMeta.Writable(accounts.BuyerReceiptTokenAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.AuctionHouseTreasury, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeTradeState, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.AtaProgram, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
                _data.WriteU8(auctioneerAuthorityBump, offset);
                offset += 1;
                _data.WriteU64(buyerPrice, offset);
                offset += 8;
                _data.WriteU64(tokenSize, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static TransactionInstruction Sell(SellAccounts accounts, byte tradeStateBump, byte freeTradeStateBump, byte programAsSignerBump, byte auctioneerAuthorityBump, ulong tokenSize, DateTime startTime, DateTime endTime, ulong? reservePrice, ulong? minBidIncrement, uint? timeExtPeriod, uint? timeExtDelta, bool? allowHighBidCancel, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.AuctionHouseProgram, false), AccountMeta.Writable(accounts.ListingConfig, false), AccountMeta.Writable(accounts.Wallet, false), AccountMeta.Writable(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.SellerTradeState, false), AccountMeta.Writable(accounts.FreeSellerTradeState, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.ProgramAsSigner, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
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
                _data.WriteU8(auctioneerAuthorityBump, offset);
                offset += 1;
                _data.WriteU64(tokenSize, offset);
                offset += 8;
                _data.WriteS64(((DateTimeOffset)startTime).ToUnixTimeSeconds(), offset);
                offset += 8;
                _data.WriteS64(((DateTimeOffset)endTime).ToUnixTimeSeconds(), offset);
                offset += 8;
                if (reservePrice != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(reservePrice.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (minBidIncrement != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU64(minBidIncrement.Value, offset);
                    offset += 8;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (timeExtPeriod != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU32(timeExtPeriod.Value, offset);
                    offset += 4;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (timeExtDelta != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteU32(timeExtDelta.Value, offset);
                    offset += 4;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                if (allowHighBidCancel != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteBool(allowHighBidCancel.Value, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static TransactionInstruction Buy(BuyAccounts accounts, byte tradeStateBump, byte escrowPaymentBump, byte auctioneerAuthorityBump, ulong buyerPrice, ulong tokenSize, PublicKey programId)
            {
                List<AccountMeta> keys = new()
                {AccountMeta.ReadOnly(accounts.AuctionHouseProgram, false), AccountMeta.Writable(accounts.ListingConfig, false), AccountMeta.ReadOnly(accounts.Seller, false), AccountMeta.ReadOnly(accounts.Wallet, true), AccountMeta.Writable(accounts.PaymentAccount, false), AccountMeta.ReadOnly(accounts.TransferAuthority, false), AccountMeta.ReadOnly(accounts.TreasuryMint, false), AccountMeta.ReadOnly(accounts.TokenAccount, false), AccountMeta.ReadOnly(accounts.Metadata, false), AccountMeta.Writable(accounts.EscrowPaymentAccount, false), AccountMeta.ReadOnly(accounts.Authority, false), AccountMeta.ReadOnly(accounts.AuctionHouse, false), AccountMeta.Writable(accounts.AuctionHouseFeeAccount, false), AccountMeta.Writable(accounts.BuyerTradeState, false), AccountMeta.ReadOnly(accounts.AuctioneerAuthority, false), AccountMeta.ReadOnly(accounts.AhAuctioneerPda, false), AccountMeta.ReadOnly(accounts.TokenProgram, false), AccountMeta.ReadOnly(accounts.SystemProgram, false), AccountMeta.ReadOnly(accounts.Rent, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(16927863322537952870UL, offset);
                offset += 8;
                _data.WriteU8(tradeStateBump, offset);
                offset += 1;
                _data.WriteU8(escrowPaymentBump, offset);
                offset += 1;
                _data.WriteU8(auctioneerAuthorityBump, offset);
                offset += 1;
                _data.WriteU64(buyerPrice, offset);
                offset += 8;
                _data.WriteU64(tokenSize, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }