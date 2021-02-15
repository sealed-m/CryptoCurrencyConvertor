using Domain.Models;
using Infrastructure.ExternalServiceCaller.Proxy;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Utils
{
    internal static class CachedCryptoCurrencyHelper
    {
        internal static List<CryptoCurrency> ReturnCryptoData(this ConcurrentDictionary<string, CachedCryptoCurrency> cache)
        {
            return cache.Values.Select(x => new CryptoCurrency()
            {
                Id = x.Id,
                Code = x.Code,
                CurrencyQuotes = x.CurrencyQuotes

            }).ToList();
        }
    }
}
