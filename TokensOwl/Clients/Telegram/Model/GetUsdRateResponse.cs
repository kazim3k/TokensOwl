using System;
using System.Text.Json.Serialization;

namespace TokensOwl.Clients.Telegram.Model
{
    public class GetUsdRateResponse
    {
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("asset_id_base")]
        public string AssetIdBase { get; set; }

        [JsonPropertyName("asset_id_quote")]
        public string AssetIdQuote { get; set; }

        [JsonPropertyName("rate")]
        public double Rate { get; set; }
    }
}