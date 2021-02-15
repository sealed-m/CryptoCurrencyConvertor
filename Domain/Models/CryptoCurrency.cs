using System.Collections.Generic;

namespace Domain.Models
{
    public class CryptoCurrency
    {
        public int Id { get; set; }
        public string Code { get; set; }


        public List<CurrencyQuote> CurrencyQuotes { get; set; }
    }
}
