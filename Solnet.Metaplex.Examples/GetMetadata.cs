using Solnet.Metaplex;
using Solnet.Wallet;
using Solnet.Rpc;
using System;


namespace Solnet.Metaplex.Examples
{


public class GetMetadataExample : IRunnableExample
{
    string pk = "5CEeeHkyezrVpexdKjGkMv18dDRRW2tbF45yr5YfmAHt";
    public async void Run()
    {
        Console.WriteLine("### Get Metadata example ###");
        Console.WriteLine("Getting account {0}", pk );

        var client = ClientFactory.GetClient( Cluster.MainNet);
        var account = await MetadataAccount.GetAccount( client, new PublicKey(pk ));

        Console.WriteLine( $"Owner: {account.owner}");
        Console.WriteLine( $"Authority key: {account.updateAuthority}");
        Console.WriteLine( $"Mint key: {account.mint}");
        Console.WriteLine( $"Name: {account.data.name}");
        Console.WriteLine( $"Symbol: {account.data.symbol}");
        Console.WriteLine( $"Uri: {account.data.uri}");
        Console.WriteLine( $"SellerFeeBasisPoints: {account.data.sellerFeeBasisPoints}");

        Console.WriteLine( $"---Creators---");
        foreach( Creator c in account.data.creators)
        {
            Console.WriteLine( $"Creator Key: {c.key}");
            Console.WriteLine( $"Creator Share: {c.share}");
            Console.WriteLine( $"Creator is verified: {c.verified}");
        }
        Console.WriteLine(  "---Metadata-------");

        Console.Write( await account.data.FetchMetadata() );

        Console.WriteLine ( "------------------");
    }
}



}