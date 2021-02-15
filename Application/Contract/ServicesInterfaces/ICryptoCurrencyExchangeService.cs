using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contract.ServicesInterfaces
{
    public interface ICryptoCurrencyExchangeService
    {
        Task<List<CryptoCurrency>> GetCryptoListQuotsAsync(List<CryptoCurrency> cryptoCurrencyCodeList);
    }
}
