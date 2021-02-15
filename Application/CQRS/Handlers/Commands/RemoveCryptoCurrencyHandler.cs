using Application.Contract.RepositoryInterfaces;
using Application.Contract.Responses;
using Application.CQRS.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Handlers.Commands
{
    public class RemoveCryptoCurrencyHandler : IRequestHandler<RemoveCryptoCurrencyCommand, ValidateableResponse<CryptoCurrencyDeleteResponse>>
    {

        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;

        public RemoveCryptoCurrencyHandler(ICryptoCurrencyRepository cryptoCurrencyRepository)
        {
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
        }

        public async Task<ValidateableResponse<CryptoCurrencyDeleteResponse>> Handle(RemoveCryptoCurrencyCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _cryptoCurrencyRepository.RemoveCryptoCurrencyAsync(request.Id, cancellationToken);
            return new ValidateableResponse<CryptoCurrencyDeleteResponse>(new CryptoCurrencyDeleteResponse(result));
        }
    }
}
