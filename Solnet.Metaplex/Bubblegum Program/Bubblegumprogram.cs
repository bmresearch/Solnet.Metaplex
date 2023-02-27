using Solnet.Metaplex.Bubblegum.Types;
using Solnet.Programs.Utilities;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
#pragma warning disable CS1591
namespace Solnet.Metaplex.Bubblegum
{

    public static class BubblegumProgram
        {
            public static Solnet.Rpc.Models.TransactionInstruction CreateTree(CreateTreeAccounts accounts, uint maxDepth, uint maxBufferSize, bool? _public, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeCreator, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(15866122498241745829UL, offset);
                offset += 8;
                _data.WriteU32(maxDepth, offset);
                offset += 4;
                _data.WriteU32(maxBufferSize, offset);
                offset += 4;
                if (_public != null)
                {
                    _data.WriteU8(1, offset);
                    offset += 1;
                    _data.WriteBool(_public.Value, offset);
                    offset += 1;
                }
                else
                {
                    _data.WriteU8(0, offset);
                    offset += 1;
                }

                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction SetTreeDelegate(SetTreeDelegateAccounts accounts, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeCreator, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NewTreeDelegate, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(7393276431020750589UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction MintV1(MintV1Accounts accounts, MetadataArgs message, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeDelegate, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(7527366247671947921UL, offset);
                offset += 8;
                offset += message.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction MintToCollectionV1(MintToCollectionV1Accounts accounts, MetadataArgs metadataArgs, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeDelegate, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthorityRecordPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.EditionAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.BubblegumSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(1105245328311980697UL, offset);
                offset += 8;
                offset += metadataArgs.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction VerifyCreator(VerifyCreatorAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Creator, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(14003103321588502836UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                offset += message.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction UnverifyCreator(UnverifyCreatorAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Creator, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(10984406386623492715UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                offset += message.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction VerifyCollection(VerifyCollectionAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeDelegate, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthorityRecordPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.EditionAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.BubblegumSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(12212134156261749048UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                offset += message.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction UnverifyCollection(UnverifyCollectionAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeDelegate, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthorityRecordPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.EditionAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.BubblegumSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(12158180955007941626UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                offset += message.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction SetAndVerifyCollection(SetAndVerifyCollectionAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, MetadataArgs message, PublicKey collection, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeDelegate, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthority, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionAuthorityRecordPda, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CollectionMint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.CollectionMetadata, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.EditionAccount, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.BubblegumSigner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(16912400468640658155UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                offset += message.Serialize(_data, offset);
                _data.WritePubKey(collection, offset);
                offset += 32;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction Transfer(TransferAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NewLeafOwner, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(13422138168166593699UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction Delegate(DelegateAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.PreviousLeafDelegate, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.NewLeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(9873113408189731674UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction Burn(BurnAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(6713419448098582132UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction Redeem(RedeemAccounts accounts, byte[] root, byte[] dataHash, byte[] creatorHash, ulong nonce, uint index, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.LeafOwner, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Voucher, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(16240477538706918584UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                _data.WriteSpan(dataHash, offset);
                offset += dataHash.Length;
                _data.WriteSpan(creatorHash, offset);
                offset += creatorHash.Length;
                _data.WriteU64(nonce, offset);
                offset += 8;
                _data.WriteU32(index, offset);
                offset += 4;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction CancelRedeem(CancelRedeemAccounts accounts, byte[] root, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.LeafOwner, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Voucher, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(17451641138953342063UL, offset);
                offset += 8;
                _data.WriteSpan(root, offset);
                offset += root.Length;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction DecompressV1(DecompressV1Accounts accounts, MetadataArgs metadata, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.Writable(accounts.Voucher, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.LeafOwner, true), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Mint, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.MintAuthority, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MasterEdition, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SysvarRent, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.AssociatedTokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(5883102871591605558UL, offset);
                offset += 8;
                offset += metadata.Serialize(_data, offset);
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }

            public static Solnet.Rpc.Models.TransactionInstruction Compress(CompressAccounts accounts, PublicKey programId)
            {
                List<Solnet.Rpc.Models.AccountMeta> keys = new()
                {Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TreeAuthority, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafOwner, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LeafDelegate, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.MerkleTree, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.TokenAccount, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Mint, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Metadata, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.MasterEdition, false), Solnet.Rpc.Models.AccountMeta.Writable(accounts.Payer, true), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.LogWrapper, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.CompressionProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.TokenMetadataProgram, false), Solnet.Rpc.Models.AccountMeta.ReadOnly(accounts.SystemProgram, false)};
                byte[] _data = new byte[1200];
                int offset = 0;
                _data.WriteU64(18262964761550438738UL, offset);
                offset += 8;
                byte[] resultData = new byte[offset];
                Array.Copy(_data, resultData, offset);
                return new Solnet.Rpc.Models.TransactionInstruction{Keys = keys, ProgramId = programId.KeyBytes, Data = resultData};
            }
        }
    }