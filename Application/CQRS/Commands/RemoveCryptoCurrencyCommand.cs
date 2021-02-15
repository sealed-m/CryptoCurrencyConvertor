using Application.Contract.Responses;
using MediatR;

namespace Application.CQRS.Commands
{
    public class RemoveCryptoCurrencyCommand : IRequest<ValidateableResponse<CryptoCurrencyDeleteResponse>>
    {

        public RemoveCryptoCurrencyCommand(int id)
        {
            this.Id = id;
        }
        public int Id { get; private set; }
    }
}
