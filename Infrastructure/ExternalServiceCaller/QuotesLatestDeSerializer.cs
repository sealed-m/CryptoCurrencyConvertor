using Domain.Models;
using Infrastructure.Models;
using System.Linq;
using System.Text.Json;

namespace Infrastructure.ExternalServiceCaller
{
    public class QuotesLatestDeSerializer
    {

        public static CurrencyQuote GetCryptoQuotes(CoinMarketQuotesLatestResponseModel apiResult, string cryptoCurrencyCode, string quote)
        {
            if (apiResult.HasError)
            {
                return InvalidResponse(quote);
            }

            return ValidResponse(apiResult, cryptoCurrencyCode);
        }

        private static CurrencyQuote InvalidResponse(string quote)
        {
            return new CurrencyQuote()
            {
                Code = quote,
                Value = 0,
                Error = "Error!"
            };
        }

        private static CurrencyQuote ValidResponse(CoinMarketQuotesLatestResponseModel apiResult, string cryptoCurrencyCode)
        {
            var currencyQuote = JsonSerializer.Deserialize<QuotesLatest>(apiResult.Result);
            return new CurrencyQuote()
            {
                Code = currencyQuote.Data[cryptoCurrencyCode].Quote.First().Key,
                Value = currencyQuote.Data[cryptoCurrencyCode].Quote.First().Value.Price,
                Error = string.Empty
            };
        }

    }
}
