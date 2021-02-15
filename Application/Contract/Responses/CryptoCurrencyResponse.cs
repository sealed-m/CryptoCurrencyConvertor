using System.Collections.Generic;

namespace Application.Contract.Responses
{
    public class CryptoCurrencyResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public List<QuoteCurrencyResponse> QuoteCurrenciesResponse { get; set; }

    }
}
