<p align="center">
    <img src="https://raw.githubusercontent.com/mariomatic/Solnet.Metaplex/master/assets/solnet-metaplex-icon.png" margin="auto" height="100"/>
</p>

<p align="center">
    <a href="https://github.com/bmresearch/Solnet.Metaplex/actions/workflows/dotnet.yml">
        <img src="https://github.com/bmresearch/Solnet.Metaplex/actions/workflows/dotnet.yml/badge.svg"
            alt="GitHub Workflow Build Status (master)" ></a>
    <a href="">
        <img src="https://img.shields.io/github/license/bmresearch/solnet.Metaplex?style=flat-square"
            alt="Code License"></a>
    <a href="https://twitter.com/intent/follow?screen_name=blockmountainio">
        <img src="https://img.shields.io/twitter/follow/blockmountainio?style=flat-square&logo=twitter"
            alt="Follow on Twitter"></a>
    <a href="https://discord.gg/YHMbpuS3Tx">
       <img alt="Discord" src="https://img.shields.io/discord/849407317761064961?style=flat-square"
            alt="Join the discussion!"></a>
</p>

# What is Solnet.Metaplex?

[Solnet](https://github.com/bmresearch/Solnet) is Solana's .NET integration library, a number of packages that implement features to interact with
Solana from .Net applications. This SDK only covers NFTs and the vault program. More support for Bubblegum and the Auction house coming soon!

Solnet.Metaplex is a package within the same `Solnet.` namespace that implements a Client for [Metaplex](https://www.metaplex.com), this project is in a
separate repository so it is contained, as the goal for [Solnet](https://github.com/bmresearch/Solnet) was to be a core SDK.

## Features

## Requirements
- net 6.0

Backport to .NET standard 2.0 is available on Nuget under version 4.0.0

## Dependencies
- Solnet

## Examples

Create & Mint a Fungible, Semi-Fungible, Non-Fungible, or Programmable metadata token

            var client = ClientFactory.GetClient( Cluster.DevNet);
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

                uses = new Uses(UseMethod.Single, 5, 5),

                //If your NFT is programmable and has a ruleset then specify it here
                //programmableConfig = new ProgrammableConfig(rulesetAddress)
            }; 

            //Easily create any type of metadata token. Any nullable parameters can be overrided to provide the data needed to create complex metadata tokens or use legacy instructions
            MetaplexClient metaplexClient = new MetaplexClient(client);
           
            await metaplexClient.CreateNFT(ownerAccount, mintAccount, TokenStandard.NonFungible, tokenMetadata, false, true);

Get the metadata of a token
        
        string pk = "token address here"
        Console.WriteLine("### Get Metadata example ###");
        Console.WriteLine("Getting account {0}", pk );

        var client = ClientFactory.GetClient( Cluster.MainNet);
        var account = await MetadataAccount.GetAccount( client, new PublicKey(pk ));

        Console.WriteLine( $"Owner: {account.owner}");
        Console.WriteLine( $"Authority key: {account.updateAuthority}");
        Console.WriteLine( $"Mint key: {account.mint}");
        Console.WriteLine( $"Name: {account.metadata.name}");
        Console.WriteLine( $"Symbol: {account.metadata.symbol}");
        Console.WriteLine( $"Uri: {account.metadata.uri}");
        Console.WriteLine( $"SellerFeeBasisPoints: {account.metadata.sellerFeeBasisPoints}");

        Console.WriteLine( $"---Creators---");
        foreach( Creator c in account.metadata.creators)
        {
            Console.WriteLine( $"Creator Key: {c.key}");
            Console.WriteLine( $"Creator Share: {c.share}");
            Console.WriteLine( $"Creator is verified: {c.verified}");
        }

        Console.WriteLine(  "-------Metadata-------");
        Console.WriteLine($"Name: {account.offchainData.name}");
        Console.WriteLine($"Description: {account.offchainData.description}");
        Console.WriteLine($"Symbol: {account.offchainData.symbol}");
        Console.WriteLine($"Collection: {account.offchainData.collection}");
        Console.WriteLine($"Default Image: { account.offchainData.default_image }" );
        Console.WriteLine($"Animation url: {account.offchainData.animation_url}");

        foreach (var attribute in account.offchainData.attributes)
        {
            if(attribute != null)
               Console.WriteLine($"Attribute: { attribute.trait_type } | { attribute.value }");
        }
            
        Console.WriteLine ( "------------------");

## Contribution

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/bmresearch/Solnet.Metaplex)

We encourage everyone to contribute, submit issues, PRs, discuss. Every kind of help is welcome.

## Contributors

* **Mariomatic** - *Maintainer* - [mariomatic](https://github.com/mariomatic)

See also the list of [contributors](https://github.com/bmresearch/Solnet.Metaplex/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/bmresearch/Solnet.Metaplex/blob/master/LICENSE) file for details



