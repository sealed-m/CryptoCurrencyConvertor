using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.Models
{

    public class CoinMarketQuotesLatestResponseModel
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
        public string Result { get; set; }
    }
    public static class CoinMarketQuotesLatestResponseMaker
    {
        public static CoinMarketQuotesLatestResponseModel OK(string res)
        {
            var model = new CoinMarketQuotesLatestResponseModel();
            model.Result = res;
            model.HasError = false;
            return model;
        }

        public static CoinMarketQuotesLatestResponseModel Fail(string error)
        {
            var model = new CoinMarketQuotesLatestResponseModel();
            model.Error = error;
            model.HasError = true;
            return model;
        }
    }

 
    public class QuotesLatest
    {
        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, Data> Data { get; set; }
    }
    public class Status
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_message")]
        public object ErrorMessage { get; set; }

        [JsonPropertyName("elapsed")]
        public int Elapsed { get; set; }

        [JsonPropertyName("credit_count")]
        public int CreditCount { get; set; }

        [JsonPropertyName("notice")]
        public object Notice { get; set; }
    }
    public class Quote
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("volume_24h")]
        public double Volume24h { get; set; }

        [JsonPropertyName("percent_change_1h")]
        public double PercentChange1h { get; set; }

        [JsonPropertyName("percent_change_24h")]
        public double PercentChange24h { get; set; }

        [JsonPropertyName("percent_change_7d")]
        public double PercentChange7d { get; set; }

        [JsonPropertyName("percent_change_30d")]
        public double PercentChange30d { get; set; }

        [JsonPropertyName("market_cap")]
        public double MarketCap { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
    public class Data
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("num_market_pairs")]
        public int NumMarketPairs { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("max_supply")]
        public double? MaxSupply { get; set; }

        [JsonPropertyName("circulating_supply")]
        public double? CirculatingSupply { get; set; }

        [JsonPropertyName("total_supply")]
        public double? TotalSupply { get; set; }

        [JsonPropertyName("is_active")]
        public int IsActive { get; set; }

        [JsonPropertyName("platform")]
        public object Platform { get; set; }

        [JsonPropertyName("cmc_rank")]
        public int CmcRank { get; set; }

        [JsonPropertyName("is_fiat")]
        public int IsFiat { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("quote")]
        public Dictionary<string, Quote> Quote { get; set; }
    }



}
