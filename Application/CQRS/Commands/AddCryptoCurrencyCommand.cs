using Application.Contract.Requests;
using Application.Contract.Responses;
using MediatR;

namespace Application.CQRS.Commands
{
    public class AddCryptoCurrencyCommand : IRequest<ValidateableResponse<CryptoCurrencyRawResponse>>
    {
        public AddCryptoCurrencyCommand(CryptoCurrencyAddRequest request)
        {
            this.Code = request.Code;
        }
        public string Code { get; private set; }

    }
}
