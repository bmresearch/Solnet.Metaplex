using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solnet.Metaplex.Json
{
    /// <summary> Metaplex Token Standard Offchain JSON Structure class </summary>
    public class MetaplexTokenStandard
    {
        /// <summary> Metadata token name - Wallets recognize this as the true name </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary> Metadata collection symbol </summary>
        [JsonProperty("symbol")]
        public string symbol { get; set; }
        /// <summary> Metadata token description </summary>
        [JsonProperty("description")]
        public string description { get; set; }

        /// <summary>Creators secondary market fee</summary>
        [JsonProperty("seller_fee_basis_points")]
        public int seller_fee_basis_points { get; set; }

        /// <summary> Primary display image for the token </summary>
        [JsonProperty("image")]
        public string default_image { get; set; }

        /// <summary> Secondary display for the token </summary>
        [JsonProperty("animation_url")]
        public string animation_url { get; set; }

        /// <summary> Brand website </summary>
        [JsonProperty("external_url")]
        public string external_url { get; set; }

        /// <summary> Metadata token attributes used to compare NFTs without looking at them</summary>
        [JsonProperty("attributes")]
        public List<Attribute> attributes { get; set; }

        /// <summary> Metadata token collection family </summary>
        [JsonProperty("collection")]
        public Collection collection { get; set; }

        /// <summary> Metadata token content receipt</summary>
        [JsonProperty("properties")]
        public Properties properties { get; set; }
    }
    /// <summary> JSON class for the Attribute object </summary>
    public class Attribute
    {
        /// <summary>Attribute trait type. Usually an object type</summary>
        [JsonProperty("trait_type")]
        public string trait_type { get; set; }

        /// <summary>The value of the attribute object</summary>
        [JsonProperty("value")]
        public string value { get; set; }

    }
    /// <summary> JSON class for the Collection object </summary>
    public class Collection
    {
        /// <summary> Collection name</summary>
        [JsonProperty("name")]
        public string name { get; set; }
        /// <summary> Collection family</summary>
        [JsonProperty("family")]
        public string family { get; set; }

    }
    /// <summary> JSON class for the Properties FileType object </summary>
    public class FileType
    {
        /// <summary> Offsite file URI link </summary>
        [JsonProperty("uri")]
        public string uri { get; set; }
        /// <summary> File type used to know how to render the content </summary>
        [JsonProperty("type")]
        public string type { get; set; }
    }
    /// <summary> JSON class for the Creator object </summary>
    public class Creator
    {
        /// <summary> Creator account address</summary>
        [JsonProperty("address")]
        public string address { get; set; }

        /// <summary> Creators share </summary>
        [JsonProperty("share")]
        public int share { get; set; }
    }
    /// <summary> JSON class for the Properties object</summary>
    public class Properties
    {
        /// <summary> Files linked to token. Core link between the token and its content</summary>
        [JsonProperty("files")]
        public List<FileType> files { get; set; }

        /// <summary> Creators of the token and content. Should always be signed</summary>
        [JsonProperty("creators")]
        public List<Creator> creators { get; set; }
    }
}
