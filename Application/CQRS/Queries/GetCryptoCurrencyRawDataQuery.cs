using Application.Contract.Responses;
using MediatR;

namespace Application.CQRS.Queries
{
    public class GetCryptoCurrencyRawDataQuery : IRequest<CryptoCurrencyRawResponse>
    {
        public GetCryptoCurrencyRawDataQuery(int id)
        {
            this.Id = id;
        }
        public int Id { get; private set; }
    }
}
