using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Algorand.algonode.sdk.net.Models
{

    public class AssetHolding
    {
        public long Amount { get; set; }

        [JsonProperty("asset-id")]
        public int AssetId { get; set; }

        [JsonProperty("is-frozen")]
        public bool IsFrozen { get; set; }


    }

    public class AlgoAccountAsset
    {
        [JsonProperty("asset-holding")]
        public AssetHolding AssetHolding { get; set; }

        public int Round { get; set; }

        public double Balance { get; set; }
    }


}
