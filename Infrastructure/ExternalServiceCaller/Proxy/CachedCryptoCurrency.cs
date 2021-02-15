using Domain.Models;
using System;

namespace Infrastructure.ExternalServiceCaller.Proxy
{
    public class CachedCryptoCurrency : CryptoCurrency
    {
        public CachedCryptoCurrency(CryptoCurrency cryptoCurrency, DateTime cachedTime)
        {
            this.Id = cryptoCurrency.Id;
            this.Code = cryptoCurrency.Code;
            this.CurrencyQuotes = cryptoCurrency.CurrencyQuotes;
            this.CachedTime = cachedTime;
        }
        public DateTime CachedTime { get; private set; }
    }
}
