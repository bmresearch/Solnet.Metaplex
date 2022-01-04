using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Rpc.Utilities;
using Solnet.Wallet;
using Solnet.Wallet.Utilities;
using Solnet.Programs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Solnet.Metaplex
{
    enum VaultKey {
        Uninitialized = 0,
        VaultV1 = 3, 
        SafetyDepositBoxV1 = 1,
        ExternalPriceAccountV1 = 2,
    }
    
    enum VaultState {
        Inactive = 0,
        Active = 1,
        Combined = 2,
        Deactivated = 3,
    }
    
    class VaultAccount : Account
    {
        public VaultKey key;
        public PublicKey tokenProgram;
        public PublicKey fractionMint;
        public PublicKey authority;
        public PublicKey fractionTreasury;
        public PublicKey reedemTreasury;
        public bool allowFurtherShareCreation;
        public PublicKey pricingLookupAddress;
        public short tokenTypeCount;
        public VaultState state;

        private UInt64 lockedPricePerShare;

        VaultAccount()
        {
            this.key = VaultKey.VaultV1;
        }

        public async Task<PublicKey> getPDA(PublicKey pk)
        {
            throw new NotImplementedException();
        }

        static bool IsCompatible(ReadOnlySpan<byte> data)
        {
            return data.GetS8(0) == (sbyte) VaultKey.VaultV1;
        }

        class SafetyDepositBox : Account
        {
            private AccountInfo info;
            
            public VaultKey key;
            public PublicKey vault;
            public PublicKey tokenMint;
            public PublicKey store;
            public short order;

            SafetyDepositBox()
            {
                this.key = VaultKey.SafetyDepositBoxV1;
            }

            SafetyDepositBox(PublicKey pk, AccountInfo info)
            {
                if (VaultProgram.ProgramIdKey != info.Owner) throw new ErrorNotOwner();
                if (SafetyDepositBox.IsCompatible(info.Data[0] )) throw new ErrorInvalidAccountData(); // Encoding??
                this.info = info;
            }
            
            static byte[] getPDA(PublicKey vault, PublicKey mint)
            {
                byte[] address = new byte[32];
                int nonce;
                AddressExtensions.TryFindProgramAddress(
                    new List<byte[]>() {
                        Encoding.UTF8.GetBytes(VaultProgram.PREFIX),
                        vault,
                        mint
                    },
                    VaultProgram.ProgramIdKey,
                    out address,
                    out nonce
                );

                return address;
            }
            
            static bool IsCompatible(ReadOnlySpan<byte> data)
            {
                return data.GetS8(0) == (byte) VaultKey.SafetyDepositBoxV1;
            }
            
            
        }

        public class ErrorNotOwner : Exception
        {
            public ErrorNotOwner() : base("Private key given is not a owner.") {}
        }
        
        public class ErrorInvalidAccountData : Exception
        {
            public ErrorInvalidAccountData() : base("Account data is not of a correct type.") {}
        }
    }
    
    
    
}