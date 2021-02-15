using Application.Contract.Responses;
using Application.CQRS.Commands;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Maping
{
    public class Mapper : IMapper
    {

        public List<CryptoCurrencyResponse> MapCryptoCurrencyToCryptoCurrencyResponse(List<CryptoCurrency> cryptoCurrencies)
        {
            return cryptoCurrencies.Select(x => new CryptoCurrencyResponse()
            {
                Code = x.Code,
                Id = x.Id,
                QuoteCurrenciesResponse = x.CurrencyQuotes.Select(c => new QuoteCurrencyResponse()
                {
                    CurrencyCode = c.Code,
                    Price = c.Value,
                    ErrorMessage = c.Error,
                    HasError = !string.IsNullOrEmpty(c.Error)
                }).ToList()
            }).ToList();
        }

        public CryptoCurrencyResponse MapCryptoCurrencyToCryptoCurrencyResponse(CryptoCurrency cryptoCurrency)
        {
            return new CryptoCurrencyResponse()
            {
                Id = cryptoCurrency.Id,
                Code = cryptoCurrency.Code
            };
        }

        public CryptoCurrencyRawResponse MapCryptoCurrencyToCryptoCurrencyRawResponse(CryptoCurrency cryptoCurrency)
        {
            return new CryptoCurrencyRawResponse()
            {
                Id = cryptoCurrency.Id,
                Code = cryptoCurrency.Code
            };
        }

        public CryptoCurrency MapAddCryptoCurrencyCommandToCryptoCurrency(AddCryptoCurrencyCommand command)
        {
            return new CryptoCurrency() { Code = command.Code };
        }
    }
}
