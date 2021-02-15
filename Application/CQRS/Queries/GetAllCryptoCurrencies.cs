using Application.Contract.Responses;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.Queries
{
    public class GetAllCryptoCurrencies : IRequest<List<CryptoCurrencyResponse>>
    {

    }
}
