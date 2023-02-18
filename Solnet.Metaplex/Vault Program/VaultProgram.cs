using Solnet.Programs;
using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Collections.Generic;

namespace Solnet.Metaplex
{
    ///
    public static class VaultProgram
    {
        /// <summary>
        /// The public key of the Vault Program.
        /// </summary>
        public static readonly PublicKey ProgramIdKey = new("vau1zxA2LbssAUEF7Gpw91zMM1LvXrvpzJtmZ58rPsn");

        internal const string PREFIX = "vault";

        /// <summary>
        /// The program's name.
        /// </summary>
        public const string ProgramName = "Vault Program";

        /// <summary>
        ///  Inititialize the vault
        /// </summary>
        /// <param name="ShareMint"></param>
        /// <param name="RedeemTreasuryTokenAccount"></param>
        /// <param name="FractionTreasuryTokenAccount"></param>
        /// <param name="Vault"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="PriceLookupAddress"></param>
        /// <param name="allowFurtherShareCreation"></param>
        /// <returns></returns>
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
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = new byte[] {
                    (byte) VaultInstructionBook.InstructionID.InitVault,
                    Convert.ToByte( allowFurtherShareCreation )
                }
            };
        }

        /// <summary>
        /// Adds a token to a inactive vault
        /// </summary>
        /// <param name="SafetyDepositBox"></param>
        /// <param name="TokenAccount"></param>
        /// <param name="TokenStoreAccount"></param>
        /// <param name="FractionTreasuryTokenAccount"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="Payer"></param>
        /// <param name="TransferAuthority"></param>
        /// <param name="amount" type="UInt64"></param>
        /// <returns> TransactionInstruction </returns>
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
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeAddTokenToInactiveVault(amount)
            };
        }

        /// <summary>
        /// Activate a vault
        /// </summary>
        /// <param name="Vault"></param>
        /// <param name="FractionMint"></param>
        /// <param name="FractionTreasury"></param>
        /// <param name="FractionMintAuthority"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="numberOfInitialShares"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction ActivateVault(
            PublicKey Vault,
            PublicKey FractionMint,
            PublicKey FractionTreasury,
            PublicKey FractionMintAuthority,
            PublicKey VaultAuthority,
            UInt64 numberOfInitialShares
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( Vault, false ),
                AccountMeta.Writable( FractionMint, false),
                AccountMeta.Writable( FractionTreasury, false),
                AccountMeta.ReadOnly( FractionMintAuthority, true),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeActivateVault(numberOfInitialShares)
            };
        }

        /// <summary>
        /// Combines the vault
        /// </summary>
        /// <param name="Vault"></param>
        /// <param name="TokenAccountShares"></param>
        /// <param name="TokenAccountReedem"></param>  
        /// <param name="FractionMint"></param>
        /// <param name="FractionTreasury"></param>
        /// <param name="ReedemTreasury"></param>
        /// <param name="NewVaultAuthority"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="TransferAuthority"></param>
        /// <param name="BurnAuthority"></param>
        /// <param name="PricingOracle"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction CombineVault(
            PublicKey Vault,
            PublicKey TokenAccountShares,
            PublicKey TokenAccountReedem,
            PublicKey FractionMint,
            PublicKey FractionTreasury,
            PublicKey ReedemTreasury,
            PublicKey NewVaultAuthority,
            PublicKey VaultAuthority,
            PublicKey TransferAuthority,
            PublicKey BurnAuthority,
            PublicKey PricingOracle
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( Vault, false ),
                AccountMeta.Writable( TokenAccountShares, false),
                AccountMeta.Writable( TokenAccountReedem, false),
                AccountMeta.Writable( FractionMint, false),
                AccountMeta.Writable( FractionTreasury, false),
                AccountMeta.Writable( ReedemTreasury, false),
                AccountMeta.ReadOnly( NewVaultAuthority, false),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( TransferAuthority, true),
                AccountMeta.ReadOnly( BurnAuthority, false),
                AccountMeta.ReadOnly( PricingOracle, false),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = new byte[] { (byte)VaultInstructionBook.InstructionID.CombineVault }
            };
        }

        /// <summary> Redeem shares </summary>
        /// <param name="TokenAccountShares"></param>
        /// <param name="TokenAccountReedem"></param>
        /// <param name="FractionMint"></param>
        /// <param name="ReedemTreasury"></param>
        /// <param name="TransferAuthority"></param>
        /// <param name="BurnAuthority"></param>
        /// <param name="Vault"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction RedeemShares(
            PublicKey TokenAccountShares,
            PublicKey TokenAccountReedem,
            PublicKey FractionMint,
            PublicKey ReedemTreasury,
            PublicKey TransferAuthority,
            PublicKey BurnAuthority,
            PublicKey Vault
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( TokenAccountShares, false),
                AccountMeta.Writable( TokenAccountReedem, false),
                AccountMeta.Writable( FractionMint, false),
                AccountMeta.Writable( ReedemTreasury, false),
                AccountMeta.ReadOnly( TransferAuthority, false),
                AccountMeta.ReadOnly( BurnAuthority, true),
                AccountMeta.ReadOnly( Vault, false),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = new byte[] { (byte)VaultInstructionBook.InstructionID.RedeemShares }
            };
        }

        /// <summary> Withdraw tokens from the safety deposit box </summary>    
        /// <param name="Destination"></param>
        /// <param name="SafetyDepositBox"></param>
        /// <param name="Store"></param>
        /// <param name="Vault"></param>
        /// <param name="FractionMint"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="TransferAuthority"></param>
        /// <param name="Amount"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction WithdrawTokenFromSafetyDepositBox(
            PublicKey Destination,
            PublicKey SafetyDepositBox,
            PublicKey Store,
            PublicKey Vault,
            PublicKey FractionMint,
            PublicKey VaultAuthority,
            PublicKey TransferAuthority,
            UInt64 Amount
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( Destination, false),
                AccountMeta.Writable( SafetyDepositBox, false),
                AccountMeta.Writable( Store, false),
                AccountMeta.Writable( Vault, false),
                AccountMeta.Writable( FractionMint, false),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( TransferAuthority, true),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeWithdrawTokenFromSafetyDepositBox(Amount)
            };
        }

        /// <summary> mint more fractional shares if the vault is configured to allow such </summary>
        /// <param name="FractionTreasury"></param>
        /// <param name="FractionMint"></param>
        /// <param name="Vault"></param>
        /// <param name="MintAuthority"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="Amount"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction MintFractionalShares(
            PublicKey FractionTreasury,
            PublicKey FractionMint,
            PublicKey Vault,
            PublicKey MintAuthority,
            PublicKey VaultAuthority,
            UInt64 Amount
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( FractionTreasury, false),
                AccountMeta.Writable( FractionMint, false),
                AccountMeta.Writable( Vault, false),
                AccountMeta.ReadOnly( MintAuthority, true),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeMintFractionalShares(Amount)
            };
        }


        /// <summary> Withdraws shares from the treasury to a desired account </summary>
        /// <param name="Destination"></param>
        /// <param name="FractionTreasury"></param>
        /// <param name="Vault"></param>
        /// <param name="TransferAuthority"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="Amount"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction WithdrawSharesFromTreasury(
            PublicKey Destination,
            PublicKey FractionTreasury,
            PublicKey Vault,
            PublicKey TransferAuthority,
            PublicKey VaultAuthority,
            UInt64 Amount
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( Destination, false),
                AccountMeta.Writable( FractionTreasury, false),
                AccountMeta.Writable( Vault, false),
                AccountMeta.ReadOnly( TransferAuthority, true),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeWithdrawSharesFromTreasury(Amount)
            };
        }


        /// <summary> Returns shares to the vault if you wish to remove them from circulation </summary>
        /// <param name="Source"></param>
        /// <param name="FractionTreasury"></param>
        /// <param name="Vault"></param>
        /// <param name="TransferAuthority"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="Amount"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction AddSharesToTreasury(
            PublicKey Source,
            PublicKey FractionTreasury,
            PublicKey Vault,
            PublicKey TransferAuthority,
            PublicKey VaultAuthority,
            UInt64 Amount
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( Source, false),
                AccountMeta.Writable( FractionTreasury, false),
                AccountMeta.Writable( Vault, false),
                AccountMeta.ReadOnly( TransferAuthority, true),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( TokenProgram.ProgramIdKey, false),
                AccountMeta.ReadOnly( SysVars.RentKey, false)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeAddSharesToTreasury(Amount)
            };
        }

        /// <summary> Helpful method that isn't necessary to use for main users of the app, but allows one to create/update existing external price account fields if they are signers of this account. Useful for testing purposes, and the CLI makes use of it as well so that you can verify logic. </summary>
        /// <param name="ExternalPriceAccount" type="PublicKey"></param>
        /// <param name="PricePerShare" type="UInt64"></param>
        /// <param name="PriceMint" type="PublicKey"></param>
        /// <param name="AllowedToCombine" type="bool"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction UpdateExternalPriceAccount(
            PublicKey ExternalPriceAccount,
            UInt64 PricePerShare,
            PublicKey PriceMint,
            bool AllowedToCombine
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( ExternalPriceAccount, true)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = VaultProgramData.EncodeUpdateExternalPriceAccount(PricePerShare, PriceMint, AllowedToCombine)
            };
        }

        /// <summary> Sets the authority of the vault to a new authority </summary>
        /// <param name="Vault"></param>
        /// <param name="VaultAuthority"></param>
        /// <param name="NewAuthority"></param>
        /// <returns> TransactionInstruction </returns>
        public static TransactionInstruction SetAuthority(
            PublicKey Vault,
            PublicKey VaultAuthority,
            PublicKey NewAuthority
        )
        {
            List<AccountMeta> keys = new() {
                AccountMeta.Writable( Vault, false),
                AccountMeta.ReadOnly( VaultAuthority, true),
                AccountMeta.ReadOnly( NewAuthority, true)
            };

            return new TransactionInstruction
            {
                ProgramId = ProgramIdKey.KeyBytes,
                Keys = keys,
                Data = new byte[] { (byte)VaultInstructionBook.InstructionID.SetAuthority }
            };
        }


        /// <summary>
        /// Decodes an instruction created by the Vault Program.
        /// </summary>
        /// <param name="data">The instruction data to decode.</param>
        /// <param name="keys">The account keys present in the transaction.</param>
        /// <param name="keyIndices">The indices of the account keys for the instruction as they appear in the transaction.</param>
        /// <returns>A decoded instruction.</returns>
        public static DecodedInstruction Decode(
            ReadOnlySpan<byte> data,
            IList<PublicKey> keys,
            byte[] keyIndices)
        {
            uint instruction = data.GetU8(VaultProgramData.MethodOffset);

            VaultInstructionBook.InstructionID instructionValue =
                (VaultInstructionBook.InstructionID)Enum.Parse(typeof(VaultInstructionBook.InstructionID), instruction.ToString());

            DecodedInstruction decodedInstruction = new()
            {
                PublicKey = ProgramIdKey,
                InstructionName = VaultInstructionBook.Names[instructionValue],
                ProgramName = ProgramName,
                Values = new Dictionary<string, object>(),
                InnerInstructions = new List<DecodedInstruction>()
            };

            switch (instructionValue)
            {
                case VaultInstructionBook.InstructionID.InitVault:
                    VaultProgramData.DecodeInitVault(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.AddTokenToInactiveVault:
                    VaultProgramData.DecodeAddTokenToInactiveVault(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.ActivateVault:
                    VaultProgramData.DecodeActivateVault(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.CombineVault:
                    VaultProgramData.DecodeCombineVault(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.RedeemShares:
                    VaultProgramData.DecodeRedeemShares(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.WithdrawTokenFromSafetyDepositBox:
                    VaultProgramData.DecodeWidthdrawTokenFromSafetyDepositBox(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.MintFractionalShares:
                    VaultProgramData.DecodeMintFractionalShares(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.WithdrawSharesFromTreasury:
                    VaultProgramData.DecodeWidthdrawSharesFromTreasury(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.AddSharesToTreasury:
                    VaultProgramData.DecodeAddSharesToTreasury(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.UpdateExternalPriceAccount:
                    VaultProgramData.DecodeUpdateExternalPriceAccount(decodedInstruction, data, keys, keyIndices);
                    break;
                case VaultInstructionBook.InstructionID.SetAuthority:
                    VaultProgramData.DecodeSetAuthority(decodedInstruction, data, keys, keyIndices);
                    break;
            }

            return decodedInstruction;
        }


    }
}