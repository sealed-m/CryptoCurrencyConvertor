using Application.Contract.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    internal class CryptoCurrencyRepository : ICryptoCurrencyRepository
    {
        private readonly CryptoCurrencyContext _context;

        public CryptoCurrencyRepository(CryptoCurrencyContext context)
        {
            _context = context;
        }

        public async Task<CryptoCurrency> AddNewCryptoCurrencyAsync(CryptoCurrency cryptoCurrency, CancellationToken cancellationToken)
        {
            await _context.CryptoCurrencies.AddAsync(cryptoCurrency);
            await _context.SaveChangesAsync(cancellationToken);
            return cryptoCurrency;
        }

        public async Task<List<CryptoCurrency>> GetAllCryptoCurrenciesAsync(CancellationToken cancellationToken)
        {
            return await _context.CryptoCurrencies.ToListAsync(cancellationToken);
        }

        public async Task<CryptoCurrency> GetCryptoCurrencyAsync(int Id, CancellationToken cancellationToken)
        {
            return await _context.CryptoCurrencies.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<CryptoCurrency> GetCryptoCurrencyByCodeAsync(string code, CancellationToken cancellationToken)
        {
            return await _context.CryptoCurrencies.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }

        public async Task<int> RemoveCryptoCurrencyAsync(int Id, CancellationToken cancellationToken)
        {
            var removeItem = await _context.CryptoCurrencies.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            _context.CryptoCurrencies.Remove(removeItem);
            await _context.SaveChangesAsync(cancellationToken);
            return removeItem.Id;
        }
    }
}
