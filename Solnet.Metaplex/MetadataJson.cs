using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solnet.Metaplex.Json
{
    public class MetaplexTokenStandard
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("symbol")]
        public string symbol { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("seller_fee_basis_points")]
        public int seller_fee_basis_points { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("animation_url")]
        public string animation_url { get; set; }

        [JsonProperty("external_url")]
        public string external_url { get; set; }

        [JsonProperty("attributes")]
        public List<Attribute> attributes { get; set; }

        [JsonProperty("collection")]
        public Collection collection { get; set; }

        [JsonProperty("properties")]
        public Properties properties { get; set; }
    }

    public class Attribute
    {
        [JsonProperty("trait_type")]
        public string trait_type { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

    }
    public class Collection
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("family")]
        public string family { get; set; }

    }
    public class Files
    {
        [JsonProperty("0")]
        public FileType file { get; set; }

        [JsonProperty("data")]
        public string category { get; set; }
    }
    public class FileType
    {
        [JsonProperty("uri")]
        public string uri { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }
    public class Creators
    {
        [JsonProperty("0")]
        public Creator creator { get; set; }
    }
    public class Creator
    {
        [JsonProperty("address")]
        public string address { get; set; }

        [JsonProperty("share")]
        public int share { get; set; }
    }
    public class Properties
    {
        [JsonProperty("files")]
        public List<FileType> files { get; set; }

        [JsonProperty("creators")]
        public List<Creator> creators { get; set; }
    }
}
