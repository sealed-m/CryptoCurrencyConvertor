using Application.Contract.ServicesInterfaces;
using Domain.Models;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.ExternalServiceCaller
{
    internal class CoinmarketCapService : ICryptoCurrencyExchangeService
    {
        private readonly CoinmarketCapConfig _coinmarketCapConfig;

        public CoinmarketCapService(CoinmarketCapConfig coinmarketCapConfig)
        {
            _coinmarketCapConfig = coinmarketCapConfig;
        }

        public async Task<List<CryptoCurrency>> GetCryptoListQuotsAsync(List<CryptoCurrency> cryptoCurrencyList)
        {
            foreach (var crypto in cryptoCurrencyList)
            {
                var quotes = GetCryptoQuots(crypto.Code);
                crypto.CurrencyQuotes = new List<CurrencyQuote>();
                crypto.CurrencyQuotes.AddRange(await quotes);
            }

            return cryptoCurrencyList;
        }

        private async Task<List<CurrencyQuote>> GetCryptoQuots(string cryptoCurrencyCode)
        {
            var result = new List<CurrencyQuote>();
            foreach (var quote in _coinmarketCapConfig.Quotes)
            {
                var apiResult = await GetCryptoListQuotApiResult(cryptoCurrencyCode, quote);
                result.Add(QuotesLatestDeSerializer.GetCryptoQuotes(apiResult, cryptoCurrencyCode, quote));
            }

            return result;
        }

        private async Task<CoinMarketQuotesLatestResponseModel> GetCryptoListQuotApiResult(string cryptocode, string quote)
        {
            try
            {
                var URL = new UriBuilder(_coinmarketCapConfig.CoinmarketCapBaseUrl + _coinmarketCapConfig.CryptocurrencyQuotesLatestUrl);

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString.Set("symbol", cryptocode);
                queryString.Set("convert", quote);

                URL.Query = queryString.ToString();

                var client = new WebClient();
                client.Headers.Add("X-CMC_PRO_API_KEY", _coinmarketCapConfig.ApiKey);
                client.Headers.Add("Accepts", "application/json");

                var result = await client.DownloadStringTaskAsync(URL.Uri);
                return CoinMarketQuotesLatestResponseMaker.OK(result);
            }
            catch (Exception ex)
            {
                return CoinMarketQuotesLatestResponseMaker.Fail(ex.Message);
            }

        }

    }
}
