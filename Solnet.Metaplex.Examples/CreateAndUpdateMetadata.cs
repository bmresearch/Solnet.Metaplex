using Solnet.Wallet;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Programs;
using Solnet.Rpc.Utilities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Solnet.Metaplex.Examples
{


public class CreateAndUpdateMetadata : IRunnableExample
{
    private string MnemonicWords = "brief energy nation crew gesture nuclear skate invest pet dumb hover keen";
    public void Run()
    {
        Console.WriteLine("### Create and Update Metadata example ###");

        //var client = ClientFactory.GetClient( "http://127.0.0.1" );
        var client = ClientFactory.GetClient( Cluster.DevNet );

        Console.WriteLine(client.GetHealth().Result);

        var wallet = new Wallet.Wallet(MnemonicWords);
            
        var fromAccount = wallet.GetAccount(0);
        var mintAccount = wallet.GetAccount(223);
        var tokenAccount = wallet.GetAccount(334);

        //client.RequestAirdrop( fromAccount.PublicKey, 1_000_000_000 );
        Console.WriteLine($"Our address : { fromAccount.PublicKey } ");
        var balance = client.GetBalance( fromAccount.PublicKey);
        Console.WriteLine( $"Account balance : { balance.Result.Value }" );

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

        var c1 = new Creator( fromAccount.PublicKey, 100);
        var c2 = new Creator( wallet.GetAccount(101).PublicKey, 100, false);

        //DATA 1
        var data = new MetadataParameters()
        {
            name = "test-1",
            symbol = "TST",
            uri = "http://noop.er",
            creators = new List<Creator>() { c1 } ,
            sellerFeeBasisPoints = 55
        };

        //DATA 2
        var data2 = new MetadataParameters()
        {
            name = "test-2",
            symbol = "TST",
            uri = "http://noop.er",
            creators = new List<Creator>() { c2 } ,
            sellerFeeBasisPoints = 55
        };



        var blockHash = client.GetRecentBlockHash();
        
        var TX1 = new TransactionBuilder()
                .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
                .SetFeePayer(fromAccount)
                //create a mint account
                .AddInstruction(
                    SystemProgram.CreateAccount(
                        fromAccount,
                        mintAccount,
                        client.GetMinimumBalanceForRentExemption(
                            TokenProgram.MintAccountDataSize,
                            Rpc.Types.Commitment.Confirmed
                        ).Result ,
                        TokenProgram.MintAccountDataSize,
                        TokenProgram.ProgramIdKey
                    )
                )
                //init mint
                .AddInstruction(
                    TokenProgram.InitializeMint(
                        mintAccount.PublicKey,
                        0,
                        fromAccount.PublicKey
                    )
                )
                //create metadata
                /*
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
                */
                //update metadata
                .AddInstruction(
                    MetadataProgram.UpdateMetadataAccount(
                        new PublicKey(metadataAddress),
                        fromAccount.PublicKey,
                        fromAccount.PublicKey,
                        data2,
                        false
                    )
                )
                // TODO mint a token
                .Build(new List<Account> { fromAccount, wallet.GetAccount(101) });

                var txSim1 = client.SimulateTransaction(TX1);

                Console.WriteLine($"Transaction sim: \n { txSim1.RawRpcResponse }");

    }
}



}