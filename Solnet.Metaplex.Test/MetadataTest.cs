using Microsoft.VisualStudio.TestTools.UnitTesting;

using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Utilities;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using Solnet.Wallet.Bip39;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading;

using Solnet.Metaplex;


namespace Solnet.Metaplex.Test
{
    

    [TestClass]
    public class MetatadaProgramTest
    {

        private string MnemonicWords = "volcano denial gloom bid lounge answer gas prevent deer magnet enrich message divide page slab category outer idle foster journey panel furnace brand leave";
        public static void PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("\nnew byte[] { ");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}\n");
            Console.WriteLine(sb.ToString());
        }
        
        //[TestMethod]
        public void MintToken()
        {
            var rpcClient = ClientFactory.GetClient(Cluster.DevNet); //, logger);

            //1. connect to wallet
            var wallet = new Wallet.Wallet(MnemonicWords);
            
            var fromAccount = wallet.Account;
            var mintAccount = wallet.GetAccount(223);
            var tokenAccount = wallet.GetAccount(334);

            Console.WriteLine($"Wallet key : { fromAccount.PublicKey } ");

            var balance = rpcClient.GetBalance( wallet.Account.PublicKey );
            Console.WriteLine($"Balance: {0} ", balance.Result.Value);

            Console.WriteLine($"Mint key : { mintAccount.PublicKey.ToString() } ");

            var blockHash = rpcClient.GetRecentBlockHash();
            var rentMint = rpcClient.GetMinimumBalanceForRentExemption(
                    TokenProgram.MintAccountDataSize,
                    Rpc.Types.Commitment.Confirmed
                );
            var rentToken = rpcClient.GetMinimumBalanceForRentExemption(
                TokenProgram.TokenAccountDataSize,
                Rpc.Types.Commitment.Confirmed
            );   

            Console.WriteLine($"Token key : { tokenAccount.PublicKey.ToString() } ");

            //2. create a mint and a token
            var instr1 = SystemProgram.CreateAccount(
                fromAccount,
                mintAccount,
                rentMint.Result,
                TokenProgram.MintAccountDataSize,
                TokenProgram.ProgramIdKey
            );
            var instr2 = TokenProgram.InitializeMint(
                mintAccount.PublicKey,
                0,
                fromAccount.PublicKey
            );
            var instr3 = SystemProgram.CreateAccount(
                fromAccount,
                tokenAccount,
                rentToken.Result,
                TokenProgram.TokenAccountDataSize,
                TokenProgram.ProgramIdKey
            );
            var instr4 = TokenProgram.InitializeAccount(
                tokenAccount.PublicKey,
                mintAccount.PublicKey,
                fromAccount.PublicKey
            );
            var instr5 = TokenProgram.MintTo(
                mintAccount.PublicKey,
                tokenAccount,
                1,
                fromAccount.PublicKey
            );
            
            
            byte[] TX1 = new TransactionBuilder()
                .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
                .SetFeePayer(fromAccount)
                .AddInstruction(instr1) // create
                .AddInstruction(instr2) // initMint
                .AddInstruction(instr3) // createaccount
                .AddInstruction(instr4) // initAccount
                .AddInstruction(instr5) // mintTo
                //.AddInstruction(instr6) // Create Metadata
                .Build(new List<Account> { fromAccount, mintAccount, tokenAccount });

            Console.WriteLine($"TX1.Length { TX1.Length }");

            var txSim = rpcClient.SimulateTransaction(TX1);

            Console.WriteLine($"Simulation: \n { txSim.RawRpcResponse } ");
            var tx = rpcClient.SendTransaction(TX1);

            Console.WriteLine($"Send: \n { tx.RawRpcResponse } ");

        }
    
        //[TestMethod]
        public void TestCreateMetadataAccount()
        {
            var rpcClient = ClientFactory.GetClient(Cluster.DevNet); //, logger);
            var blockHash = rpcClient.GetRecentBlockHash();

            var wallet = new Wallet.Wallet(MnemonicWords);
            
            var fromAccount = wallet.Account;
            var mintAccount = wallet.GetAccount(223);
            var tokenAccount = wallet.GetAccount(334);

            //PDA METADATA

            byte[] metadataAddress = new byte[32];
            int nonce;
            AddressExtensions.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mintAccount.PublicKey
                },
                MetadataProgram.ProgramIdKey,
                out metadataAddress,
                out nonce
            );


            Console.WriteLine($"PDA METADATA: {new PublicKey(metadataAddress)}");

            //PDA MASTER EDITION
            byte[] masterEditionAddress = new byte[32];
            //int nonce;
            AddressExtensions.TryFindProgramAddress(
                new List<byte[]>() {
                    Encoding.UTF8.GetBytes("metadata"),
                    MetadataProgram.ProgramIdKey,
                    mintAccount.PublicKey,
                    Encoding.UTF8.GetBytes("edition")
                },
                MetadataProgram.ProgramIdKey,
                out masterEditionAddress,
                out nonce
            );
            Console.WriteLine($"PDA MASTER: {new PublicKey(masterEditionAddress)}");
            
            //CREATORS

            var c1 = new Creator( fromAccount.PublicKey, 50);
            var c2 = new Creator( wallet.GetAccount(101).PublicKey, 50, false);

            //DATA
            var data = new MetadataParameters()
            {
                name = "ja sam test",
                symbol = "A B C",
                uri = "http://lutrija.hr",
                creators = new List<Creator>() { c1 , c2 } ,
                sellerFeeBasisPoints = 77
            };

            var TX2 = new TransactionBuilder()
                .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
                .SetFeePayer(fromAccount)
                .AddInstruction(
                    MetadataProgram.CreateMetadataAccount(
                        new PublicKey(metadataAddress), //PDA
                        mintAccount.PublicKey,  //MINT
                        fromAccount.PublicKey,  //mint AUTHORITY
                        fromAccount.PublicKey,  //PAYER
                        fromAccount.PublicKey,  //update Authority 
                        data,                   //DATA
                        true,
                        true                    //ISMUTABLE
                    )
                )
                // .AddInstruction(
                //     MetadataProgram.SignMetada(
                //         new PublicKey(metadataAddress),
                //         c2.key
                //     )
                // )
                .AddInstruction(
                    MetadataProgram.PuffMetada(
                        new PublicKey(metadataAddress)
                    )
                )
                .AddInstruction(
                    MetadataProgram.CreateMasterEdition(
                        1,
                        new PublicKey(masterEditionAddress),
                        mintAccount.PublicKey,
                        fromAccount.PublicKey,
                        fromAccount.PublicKey,
                        fromAccount.PublicKey,
                        new PublicKey(metadataAddress)
                    )
                )
            .Build(new List<Account> { fromAccount, wallet.GetAccount(101) });

            //var txSim2 = rpcClient.SimulateTransaction(TX2);

            //InstructionDecoder.Register(MetadataProgram.ProgramIdKey, MetadataProgram.Decode);
            //List<DecodedInstruction> decodedInstructions = InstructionDecoder.DecodeInstructions( TX2 );

            //Console.WriteLine($"Transaction sim: \n { txSim2.RawRpcResponse }");

        }

        [TestMethod]
        public void TestGetAndDecodeMessage() 
        {
            var client = Solnet.Rpc.ClientFactory.GetClient(Solnet.Rpc.Cluster.MainNet);
            var res = client.GetConfirmedTransaction("3tpv4udpeQ9NZhCXRkVdPz7aJqqakLPurFTLtTR6Z7UEo9gtr7UCu9rLgFEfizYwB8sQHci9CTJdZex7qSsUr2EV");


            //Thread.Sleep(3000);
            InstructionDecoder.Register(MetadataProgram.ProgramIdKey, MetadataProgram.Decode);
            List<DecodedInstruction> decodedInstructions = InstructionDecoder.DecodeInstructions(res.Result);

            foreach ( DecodedInstruction di in decodedInstructions )
            {
                Console.WriteLine("\n Instruction: " + di.InstructionName);
                Console.WriteLine("Program: " + di.ProgramName );


                foreach ( KeyValuePair<string,object> kv in di.Values)
                {
                Console.WriteLine("\t" + kv.Key + ": " + kv.Value.ToString());
                if ( kv.Value is List<Creator> )
                {
                    foreach ( Creator c in (List<Creator>) kv.Value )
                    {
                        Console.WriteLine( "\t\tCreator Key : " + c.key.ToString());
                        Console.WriteLine( "\t\tCreator Share : " + c.share.ToString());
                        Console.WriteLine( "\t\tCreator Verified : " + c.verified.ToString());
                    }
                }
                }
            }
        }
    }
}