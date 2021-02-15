using Application.Contract.Responses;
using Application.CQRS.Commands;
using Domain.Models;
using System.Collections.Generic;

namespace Application.Maping
{
    public interface IMapper
    {
        List<CryptoCurrencyResponse> MapCryptoCurrencyToCryptoCurrencyResponse(List<CryptoCurrency> cryptoCurrencies);
        CryptoCurrencyResponse MapCryptoCurrencyToCryptoCurrencyResponse(CryptoCurrency cryptoCurrency);
        CryptoCurrencyRawResponse MapCryptoCurrencyToCryptoCurrencyRawResponse(CryptoCurrency cryptoCurrency);
        CryptoCurrency MapAddCryptoCurrencyCommandToCryptoCurrency(AddCryptoCurrencyCommand command);

    }
}
