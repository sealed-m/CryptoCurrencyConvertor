using Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contract.RepositoryInterfaces
{
    public interface ICryptoCurrencyRepository
    {
        Task<List<CryptoCurrency>> GetAllCryptoCurrenciesAsync(CancellationToken cancellationToken);
        Task<CryptoCurrency> AddNewCryptoCurrencyAsync(CryptoCurrency cryptoCurrency, CancellationToken cancellationToken);
        Task<CryptoCurrency> GetCryptoCurrencyAsync(int Id, CancellationToken cancellationToken);
        Task<CryptoCurrency> GetCryptoCurrencyByCodeAsync(string code, CancellationToken cancellationToken);
        Task<int> RemoveCryptoCurrencyAsync(int Id, CancellationToken cancellationToken);

    }
}
