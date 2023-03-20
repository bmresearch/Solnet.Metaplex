using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Programs;
using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Text;
using Solnet.Rpc.Utilities;
using Solnet.Metaplex.NFT.Library;
using Solnet.Metaplex.NFT;
using System.Threading.Tasks;

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
        public async Task Run() 
        {
            //Get a RPC provider link from QuickNode or use a public one provided by a community -- The default clusters are only for lite testing
            var client = ClientFactory.GetClient( Cluster.MainNet);
            var exampleWallet = new Wallet.Wallet(pk);

            //Get your account either by the wallet or directly from the private key
            Account ownerAccount = exampleWallet.Account;
            Account mintAccount = exampleWallet.GetAccount(75);

            Console.WriteLine("Using account public key : {0}", ownerAccount.PublicKey );

            //Create the creator list
            List<Creator> creatorList = new List<Creator>
            {
                new Creator(ownerAccount.PublicKey, 100, true)
            };

            //If there is more than one then you can add more

            //creatorList.Add(new Creator(new PublicKey("")))

            Metadata tokenMetadata = new Metadata
            {
                name = "SolNET NFT",
                symbol = "SOLNET",
                sellerFeeBasisPoints = 500,
                uri = "arweave link",
                creators = creatorList,

                //If your NFT has a parent collection NFT. You can specify it here
                //collection = new Collection(collectionAddress),

                //uses = new Uses(UseMethod.Single, 5, 5),

                //If your NFT is programmable and has a ruleset then specify it here
                //programmableConfig = new ProgrammableConfig(rulesetAddress)
            }; 

            //Easily create any type of metadata token. Any nullable parameters can be overrided to provide the data needed to create complex metadata tokens or use legacy instructions
            MetadataClient metaplexClient = new MetadataClient(client);
           
           var tx = await metaplexClient.CreateNFT(ownerAccount, mintAccount, TokenStandard.NonFungible, tokenMetadata, false, true);
            Console.WriteLine(tx.RawRpcResponse);
            Console.ReadKey();
        }

    }
}