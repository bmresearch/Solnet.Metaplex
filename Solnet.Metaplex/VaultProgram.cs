using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Rpc.Utilities;
using Solnet.Wallet;
using Solnet.Wallet.Utilities;
using Solnet.Programs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;
using System.Linq;

namespace Solnet.Metaplex
{
    ///
    public static class VaultProgram
    {
        /// <summary>
        /// The public key of the Vault Program.
        /// </summary>
        public static readonly PublicKey ProgramIdKey = new("vau1zxA2LbssAUEF7Gpw91zMM1LvXrvpzJtmZ58rPsn");

        /// <summary>
        /// The program's name.
        /// </summary>
        private const string ProgramName = "Vault Program";

        public static TransactionInstruction InitVault(
            PublicKey ShareMint,
            PublicKey RedeemTreasuryTokenAccount,
            PublicKey FractionTreasuryTokenAccount,
            PublicKey Vault,
            PublicKey VaultAuthority,
            PublicKey PriceLookupAddress,
            bool allowFurtherShareCreation
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( ShareMint, false ),
                AccountMeta.Writable( RedeemTreasuryTokenAccount, false),
                AccountMeta.Writable( FractionTreasuryTokenAccount, false),
                AccountMeta.Writable( Vault, false),
                AccountMeta.ReadOnly( VaultAuthority, false),
                AccountMeta.ReadOnly( PriceLookupAddress, false),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SystemProgram.SysVarRentKey, false)
            };

            return new TransactionInstruction{
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = new byte[] { 
                    (byte) VaultProgramInstructions.Values.InitVault,
                    Convert.ToByte( allowFurtherShareCreation )
                }
            };
        }
        
        public static TransactionInstruction AddTokenToInactiveVault(
            PublicKey SafetyDepositBox,
            PublicKey TokenAccount,
            PublicKey TokenStoreAccount,
            PublicKey FractionTreasuryTokenAccount,
            PublicKey VaultAuthority,
            PublicKey Payer,
            PublicKey TransferAuthority,
            UInt64 amount
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( SafetyDepositBox, false ),
                AccountMeta.Writable( TokenAccount, false),
                AccountMeta.Writable( TokenStoreAccount, false),
                AccountMeta.Writable( FractionTreasuryTokenAccount, false),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( Payer, true),
                AccountMeta.ReadOnly( TransferAuthority, true),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SystemProgram.SysVarRentKey, false),
                AccountMeta.ReadOnly( SystemProgram.SysVarRentKey , false)
            };

            return new TransactionInstruction{
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeAddTokenToInactiveVault( amount )
            };
        }
    }
}