using Solnet.Metaplex.Candymachine.Core.Types;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Candymachine.Core
{

    public static class CMCoreProgram
    {
        public static Solnet.Rpc.Models.TransactionInstruction AddConfigLines(AddConfigLinesAccounts accounts, uint index, ConfigLine[] configLines, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(7670484038798291679UL, offset);
            offset += 8;
            _data.WriteU32(index, offset);
            offset += 4;
            _data.WriteS32(configLines.Length, offset);
            offset += 4;
            foreach (var configLinesElement in configLines)
            {
                offset += configLinesElement.Serialize(_data, offset);
            }

            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Initialize(InitializeAccounts accounts, CandyMachineData data, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuthorityPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMasterEdition, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionUpdateAuthority, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionAuthorityRecord, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(17121445590508351407UL, offset);
            offset += 8;
            offset += data.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Mint(MintAccounts accounts, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuthorityPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.MintAuthority, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.NftMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NftMintAuthority, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.NftMetadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.NftMasterEdition, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthorityRecord, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMasterEdition, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionUpdateAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.RecentSlothashes, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(12000283993290389811UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction SetAuthority(SetAuthorityAccounts accounts, PublicKey newAuthority, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(8726466921180297861UL, offset);
            offset += 8;
            _data.WritePubKey(newAuthority, offset);
            offset += 32;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction SetCollection(SetCoreCollectionAccounts accounts, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.AuthorityPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionAuthorityRecord, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.NewCollectionUpdateAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NewCollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NewCollectionMint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NewCollectionMasterEdition, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.NewCollectionAuthorityRecord, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(16085651328043253440UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction SetMintAuthority(SetMintAuthorityAccounts accounts, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.MintAuthority, true)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(8748152548857970499UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Update(UpdateAccounts accounts, CandyMachineData data, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Authority, true)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(9222597562720635099UL, offset);
            offset += 8;
            offset += data.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static Solnet.Rpc.Models.TransactionInstruction Withdraw(WithdrawAccounts accounts, PublicKey programId)
        {
            List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.CandyMachine, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Authority, true)};
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU64(2495396153584390839UL, offset);
            offset += 8;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new Solnet.Rpc.Models.TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }
    }
}