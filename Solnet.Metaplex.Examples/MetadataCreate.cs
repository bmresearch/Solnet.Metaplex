using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Programs;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Text;
using Solnet.Rpc.Utilities;


namespace Solnet.Metaplex.Examples
{
    /// <summary>
    /// Defines functionality for an example.
    /// </summary>
    public class MetadataCreate : IRunnableExample
    {
        private static string pk = "volcano denial gloom bid lounge answer gas prevent deer magnet enrich message divide page slab category outer idle foster journey panel furnace brand leave";

        /// <summary>
        /// Run the example.
        /// </summary>
        public void Run() 
        {

            var client = ClientFactory.GetClient( Cluster.DevNet);

            var wallet = new Wallet.Wallet(pk);

            Console.WriteLine("Using account public key : {0}", wallet.Account.PublicKey );

            var balanceRes = client.GetBalance(wallet.Account.PublicKey);
            if ( balanceRes.WasSuccessful )
                Console.WriteLine("Account balance: {0}", balanceRes.Result.Value);

            var mint = wallet.GetAccount(59);

                        //PDA METADATA

            PublicKey metadataAddress;
            byte nonce;
            PublicKey.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mint.PublicKey
                },
                MetadataProgram.ProgramIdKey,
                out metadataAddress,
                out nonce
            );


            Console.WriteLine($"PDA METADATA: {metadataAddress}");

            //PDA MASTER EDITION
            PublicKey masterEditionAddress;
            //int nonce;
            PublicKey.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mint.PublicKey,
                    Encoding.UTF8.GetBytes("edition")
                },
                MetadataProgram.ProgramIdKey,
                out masterEditionAddress,
                out nonce
            );
            Console.WriteLine($"PDA MASTER: {masterEditionAddress}");
            
            //CREATORS

            var c1 = new Creator( wallet.Account.PublicKey , 50, true );
            var c2 = new Creator( wallet.GetAccount(101).PublicKey , 50 , false );

            //DATA
            var data = new MetadataParameters()
            {
                name = "ja sam test",
                symbol = "ABC",
                uri = "http://no.op",
                creators = new List<Creator>() { new Creator( wallet.Account.PublicKey , 100 , true ) } ,
                sellerFeeBasisPoints = 77
            };

            var data2 = new MetadataParameters()
            {
                name = "ti si test",
                symbol = "CBA",
                uri = "http://yes.op",
                creators = new List<Creator>() { c1,c2 } ,
                sellerFeeBasisPoints = 55
            };


            var blockHash = client.GetRecentBlockHash().Result.Value.Blockhash;
            var rentMint = client.GetMinimumBalanceForRentExemption(
                    TokenProgram.MintAccountDataSize,
                    Rpc.Types.Commitment.Confirmed
                );

            var TX = new TransactionBuilder()
                .SetFeePayer(wallet.Account.PublicKey)
                .SetRecentBlockHash(blockHash)
                .AddInstruction(
                    SystemProgram.CreateAccount(
                        wallet.Account.PublicKey,
                        mint.PublicKey,
                        rentMint.Result,
                        TokenProgram.MintAccountDataSize,
                        TokenProgram.ProgramIdKey
                    )
                )
                .AddInstruction(
                    TokenProgram.InitializeMint(
                        mint,
                        0,
                        wallet.Account.PublicKey,
                        wallet.Account.PublicKey
                    )
                )
                .AddInstruction(
                    MetadataProgram.CreateMetadataAccount(
                        metadataAddress,
                        mint,
                        wallet.Account.PublicKey,
                        wallet.Account.PublicKey,
                        wallet.Account.PublicKey,
                        data,
                        true,
                        true
                    )
                )
                // .AddInstruction(
                //     MetadataProgram.UpdateMetadataAccount(
                //         new PublicKey(metadataAddress),
                //         wallet.Account.PublicKey,
                //         null,
                //         data2,
                //         null
                //     )
                // )
                .Build(new List<Account> { wallet.Account , mint});

            var simTX = client.SimulateTransactionAsync(TX).Result;

            Console.WriteLine(simTX.RawRpcResponse);
        }

    }
}