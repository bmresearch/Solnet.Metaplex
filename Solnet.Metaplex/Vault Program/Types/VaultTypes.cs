using System.Collections.Generic;

namespace Solnet.Metaplex.NFT.Library
{
    /// <summary>
    /// Vault Key Enum
    /// </summary>
    public enum VaultKey
    {
        /// <summary>
        /// Uninitialized
        /// </summary>
        Uninitialized = 0,
        /// <summary>
        /// Vault V1 Key
        /// </summary>
        VaultV1 = 3,
        /// <summary>
        /// Saftey Deposit Box Key
        /// </summary>
        SafetyDepositBoxV1 = 1,
        /// <summary>
        /// External Price Account Key
        /// </summary>
        ExternalPriceAccountV1 = 2,
    }
    /// <summary>
    /// Vault State Enum
    /// </summary>
    public enum VaultState
    {
        /// <summary>
        /// Inactive vault
        /// </summary>
        Inactive = 0,
        /// <summary>
        /// Active Vault
        /// </summary>
        Active = 1,
        /// <summary>
        ///  Hybrid State Vault
        /// </summary>
        Combined = 2,
        /// <summary>
        /// Deactivated Vault
        /// </summary>
        Deactivated = 3,
    }
    internal static class VaultInstructionBook
    {
        /// <summary>
        /// Represents the user-friendly names for the instruction types for the <see cref="VaultProgram"/>.
        /// </summary>
        internal static readonly Dictionary<InstructionID, string> Names = new()
        {
            { InstructionID.InitVault, "InitVault" },
            { InstructionID.AddTokenToInactiveVault, "AddTokenToInactiveVault" },
            { InstructionID.ActivateVault, "ActivateVault" },
            { InstructionID.CombineVault, "CombineVault" },
            { InstructionID.RedeemShares, "RedeemShares" },
            { InstructionID.WithdrawTokenFromSafetyDepositBox, "WithdrawTokenFromSafetyDepositBox" },
            { InstructionID.MintFractionalShares, "MintFractionalShares" },
            { InstructionID.WithdrawSharesFromTreasury, "WithdrawSharesFromTreasury" },
            { InstructionID.AddSharesToTreasury, "AddSharesToTreasury" },
            { InstructionID.UpdateExternalPriceAccount, "UpdateExternalPriceAccount" },
            { InstructionID.SetAuthority, "SetAuthority" }
        };
        /// <summary>
        /// Instruction IDs for the Vault Program
        /// </summary>
        internal enum InstructionID
        {
            /// <summary>
            /// Initialize vault
            /// </summary>
            InitVault = 0,
            /// <summary>
            /// Add a token to an inactive vault
            /// </summary>
            AddTokenToInactiveVault = 1,
            /// <summary>
            /// Activate the vault
            /// </summary>
            ActivateVault = 2,
            /// <summary>
            /// Combine the vault into a hybrid
            /// </summary>
            CombineVault = 3,
            /// <summary>
            /// Redeem vault shares
            /// </summary>
            RedeemShares = 4,
            /// <summary>
            /// Withdraw Token from Safety Deposit Box
            /// </summary>
            WithdrawTokenFromSafetyDepositBox = 5,
            /// <summary>
            /// Mint Fractional Shares
            /// </summary>
            MintFractionalShares = 6,
            /// <summary>
            /// Withdraw Shares from Treasury
            /// </summary>
            WithdrawSharesFromTreasury = 7,
            /// <summary>
            /// Add Shares to Treasury
            /// </summary>
            AddSharesToTreasury = 8,
            /// <summary>
            /// Update External Price 
            /// </summary>
            UpdateExternalPriceAccount = 9,
            /// <summary>
            /// Set Authority
            /// </summary>
            SetAuthority = 10
        }
    }
}