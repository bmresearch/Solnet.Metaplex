using Solnet.Metaplex.Core.Models;
using Solnet.Metaplex.Core.Types;
using Solnet.Programs.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
#pragma warning disable CS1591 
namespace Solnet.Metaplex.Core
{
    public static class MplCoreProgram
    {
        public static PublicKey programId = new PublicKey("CoREENxT6tW1HoK8ypY1SxRMZTcVPm7R94rH4PZNhX7d");
        public static TransactionInstruction CreateV1(CreateV1Accounts accounts, CreateV1Args createV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, true), 
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Owner, false), 
                AccountMeta.ReadOnly(accounts.UpdateAuthority, false), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(0, offset);
            offset += 1;
            offset += createV1Args.Serialize(_data, offset);
            Console.WriteLine(_data.Length + " | " + offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
           
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction CreateCollectionV1(CreateCollectionV1Accounts accounts, CreateCollectionV1Args createCollectionV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, true), 
                AccountMeta.ReadOnly(accounts.UpdateAuthority, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(1, offset);
            offset += 1;
            offset += createCollectionV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AddPluginV1(AddPluginV1Accounts accounts, AddPluginV1Args addPluginV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(2, offset);
            offset += 8;
            offset += addPluginV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction AddCollectionPluginV1(AddCollectionPluginV1Accounts accounts, AddCollectionPluginV1Args addCollectionPluginV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(3, offset);
            offset += 1;
            offset += addCollectionPluginV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction RemovePluginV1(RemovePluginV1Accounts accounts, RemovePluginV1Args removePluginV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true),
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(4, offset);
            offset += 1;
            offset += removePluginV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction RemoveCollectionPluginV1(RemoveCollectionPluginV1Accounts accounts, RemoveCollectionPluginV1Args removeCollectionPluginV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(5, offset);
            offset += 1;
            offset += removeCollectionPluginV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction UpdatePluginV1(UpdatePluginV1Accounts accounts, UpdatePluginV1Args updatePluginV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(6, offset);
            offset += 1;
            offset += updatePluginV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction UpdateCollectionPluginV1(UpdateCollectionPluginV1Accounts accounts, UpdateCollectionPluginV1Args updateCollectionPluginV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(7, offset);
            offset += 1;
            offset += updateCollectionPluginV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ApprovePluginAuthorityV1(ApprovePluginAuthorityV1Accounts accounts, ApprovePluginAuthorityV1Args approvePluginAuthorityV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(8, offset);
            offset += 1;
            offset += approvePluginAuthorityV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction ApproveCollectionPluginAuthorityV1(ApproveCollectionPluginAuthorityV1Accounts accounts, ApproveCollectionPluginAuthorityV1Args approveCollectionPluginAuthorityV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(9, offset);
            offset += 1;
            offset += approveCollectionPluginAuthorityV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction RevokePluginAuthorityV1(RevokePluginAuthorityV1Accounts accounts, RevokePluginAuthorityV1Args revokePluginAuthorityV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(10, offset);
            offset += 1;
            offset += revokePluginAuthorityV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction RevokeCollectionPluginAuthorityV1(RevokeCollectionPluginAuthorityV1Accounts accounts, RevokeCollectionPluginAuthorityV1Args revokeCollectionPluginAuthorityV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(11, offset);
            offset += 1;
            offset += revokeCollectionPluginAuthorityV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction BurnV1(BurnV1Accounts accounts, BurnV1Args burnV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(12, offset);
            offset += 1;
            offset += burnV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction BurnCollectionV1(BurnCollectionV1Accounts accounts, BurnCollectionV1Args burnCollectionV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(13, offset);
            offset += 1;
            offset += burnCollectionV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction TransferV1(TransferV1Accounts accounts, TransferV1Args transferV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.ReadOnly(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.NewOwner, false), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(14, offset);
            offset += 1;
            offset += transferV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction UpdateV1(UpdateV1Accounts accounts, UpdateV1Args updateV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.ReadOnly(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(15, offset);
            offset += 1;
            offset += updateV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction UpdateCollectionV1(UpdateCollectionV1Accounts accounts, UpdateCollectionV1Args updateCollectionV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.NewUpdateAuthority, false), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(16, offset);
            offset += 1;
            offset += updateCollectionV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction CompressV1(CompressV1Accounts accounts, CompressV1Args compressV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.ReadOnly(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false), 
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(17, offset);
            offset += 1;
            offset += compressV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction DecompressV1(DecompressV1Accounts accounts, DecompressV1Args decompressV1Args)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Asset, false), 
                AccountMeta.ReadOnly(accounts.Collection, false), 
                AccountMeta.Writable(accounts.Payer, true), 
                AccountMeta.ReadOnly(accounts.Authority, true), 
                AccountMeta.ReadOnly(accounts.SystemProgram, false),
                AccountMeta.ReadOnly(accounts.LogWrapper, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(18, offset);
            offset += 1;
            offset += decompressV1Args.Serialize(_data, offset);
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }

        public static TransactionInstruction Collect(CollectAccounts accounts)
        {
            List<AccountMeta> keys = new()
            {
                AccountMeta.Writable(accounts.Recipient1, false), 
                AccountMeta.Writable(accounts.Recipient2, false)
            };
            byte[] _data = new byte[1200];
            int offset = 0;
            _data.WriteU8(19, offset);
            offset += 1;
            byte[] resultData = new byte[offset];
            Array.Copy(_data, resultData, offset);
            return new TransactionInstruction { Keys = keys, ProgramId = programId.KeyBytes, Data = resultData };
        }
    }
}
