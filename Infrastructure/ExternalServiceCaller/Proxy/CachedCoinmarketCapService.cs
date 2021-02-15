using Application.Contract.ServicesInterfaces;
using Domain.Models;
using Infrastructure.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServiceCaller.Proxy
{
    internal class CachedCoinmarketCapService : ICryptoCurrencyExchangeService
    {
        private readonly ICryptoCurrencyExchangeService _cryptoCurrencyExchangeService;
        private readonly ConcurrentDictionary<string, CachedCryptoCurrency> _cache;
        private readonly CoinmarketCapConfig _coinmarketCapConfig;

        public CachedCoinmarketCapService(ICryptoCurrencyExchangeService cryptoCurrencyExchangeService,
             CoinmarketCapConfig coinmarketCapConfig)
        {
            _cryptoCurrencyExchangeService = cryptoCurrencyExchangeService;
            _coinmarketCapConfig = coinmarketCapConfig;
            _cache = new ConcurrentDictionary<string, CachedCryptoCurrency>();
        }

        public async Task<List<CryptoCurrency>> GetCryptoListQuotsAsync(List<CryptoCurrency> cryptoCurrencyCodeList)
        {
            RemoveDeletedDataFromCache(cryptoCurrencyCodeList);

            var currentCacheTime = DateTime.Now;
            var requestList = new List<CryptoCurrency>();

            foreach (var crypto in cryptoCurrencyCodeList)
            {
                if (!CheckCachedData(crypto, currentCacheTime))
                {
                    requestList.Add(crypto);
                }
            }

            var freshData = await _cryptoCurrencyExchangeService.GetCryptoListQuotsAsync(requestList);
            AddFreshDataToCache(freshData, currentCacheTime);

            return _cache.ReturnCryptoData();
        }

        private bool CheckCachedData(CryptoCurrency crypto, DateTime currentCacheTime)
        {
            if (_cache.ContainsKey(crypto.Code))
            {
                return currentCacheTime.Subtract(_cache[crypto.Code].CachedTime).TotalMinutes <= _coinmarketCapConfig.TotalMinuteCachedServiceData;
            }

            return false;
        }

        private void AddFreshDataToCache(List<CryptoCurrency> freshData, DateTime currentCacheTime)
        {
            foreach (var crypto in freshData)
            {
                _cache.AddOrUpdate(crypto.Code, new CachedCryptoCurrency(crypto, currentCacheTime)
                    , (key, oldValue) => new CachedCryptoCurrency(crypto, currentCacheTime));
            }
        }

        private void RemoveDeletedDataFromCache(List<CryptoCurrency> requestedData)
        {
            var removedKeys = _cache.Keys.Except(requestedData.Select(x => x.Code));
            foreach (var key in removedKeys)
            {
                var removedData = _cache[key];
                _cache.TryRemove(key, out removedData);
            }
        }
    }
}
