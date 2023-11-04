using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorand.algonode.sdk.net.Models
{
    public class Asset
    {
        public long Index { get; set; }
        public AssetParams Params { get; set; }
    }

    public class AssetParams
    {
        public string Clawback { get; set; }
        public string Creator { get; set; }
        public int Decimals { get; set; }
        [JsonProperty("defalult-frozen")]
        public bool DefaultFrozen { get; set; }
        public string Freeze { get; set; }
        public string Manager { get; set; }
        public string Name { get; set; }
        [JsonProperty("name-b64")]
        public string NameB64 { get; set; }
        public string Reserve { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("unit-name")]
        public string UnitName { get; set; }

        [JsonProperty("unit-name-b64")]
        public string UnitNameB64 { get; set; }
    }
}
