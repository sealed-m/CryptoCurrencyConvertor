using System.Collections.Generic;

namespace Infrastructure.ExternalServiceCaller
{
    internal class CoinmarketCapConfig
    {
        public string ApiKey { get; set; }
        public List<string> Quotes { get; set; }
        public string CoinmarketCapBaseUrl { get; set; }
        public string CryptocurrencyQuotesLatestUrl { get; set; }
        public int TotalMinuteCachedServiceData { get; set; }

    }
}
