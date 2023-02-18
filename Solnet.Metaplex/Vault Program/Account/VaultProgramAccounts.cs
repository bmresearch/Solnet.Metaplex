using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Text;


namespace Solnet.Metaplex
{


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

        static bool IsCompatible(ReadOnlySpan<byte> data)
        {
            return data.GetS8(0) == (sbyte)VaultKey.VaultV1;
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
                if (info.Data.Count != 0
                    && SafetyDepositBox.IsCompatible(Encoding.UTF8.GetBytes(info.Data[0])))
                    throw new ErrorInvalidAccountData();
                this.info = info;
            }


            static bool IsCompatible(ReadOnlySpan<byte> data)
            {
                return data.GetS8(0) == (byte)VaultKey.SafetyDepositBoxV1;
            }


        }

        public class ErrorNotOwner : Exception
        {
            public ErrorNotOwner() : base("Private key given is not a owner.") { }
        }

        public class ErrorInvalidAccountData : Exception
        {
            public ErrorInvalidAccountData() : base("Account data is not of a correct type.") { }
        }
    }



}