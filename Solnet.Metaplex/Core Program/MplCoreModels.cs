using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS1591 
namespace Solnet.Metaplex.Core.Models
{
    public class CreateV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Authority { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey Owner { get; set; }

        public PublicKey UpdateAuthority = MplCoreProgram.programId;

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class CreateCollectionV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey UpdateAuthority { get; set; }

        public PublicKey Payer { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;
    }

    public class AddPluginV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class AddCollectionPluginV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class RemovePluginV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class RemoveCollectionPluginV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class UpdatePluginV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class UpdateCollectionPluginV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class ApprovePluginAuthorityV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class ApproveCollectionPluginAuthorityV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class RevokePluginAuthorityV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class RevokeCollectionPluginAuthorityV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class BurnV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class BurnCollectionV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class TransferV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey NewOwner { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class UpdateV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class UpdateCollectionV1Accounts
    {
        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey NewUpdateAuthority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class CompressV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class DecompressV1Accounts
    {
        public PublicKey Asset { get; set; }

        public PublicKey Collection = MplCoreProgram.programId;

        public PublicKey Payer { get; set; }

        public PublicKey Authority { get; set; }

        public PublicKey SystemProgram = Programs.SystemProgram.ProgramIdKey;

        public PublicKey LogWrapper = MplCoreProgram.programId;
    }

    public class CollectAccounts
    {
        public PublicKey Recipient1 { get; set; }

        public PublicKey Recipient2 { get; set; }
    }
}
