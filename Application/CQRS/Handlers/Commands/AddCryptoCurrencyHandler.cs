using Application.Contract.RepositoryInterfaces;
using Application.Contract.Responses;
using Application.CQRS.Commands;
using Application.Maping;
using Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Handlers.Commands
{
    public class AddCryptoCurrencyHandler : IRequestHandler<AddCryptoCurrencyCommand, ValidateableResponse<CryptoCurrencyRawResponse>>
    {
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;
        private readonly IMapper _mapper;

        public AddCryptoCurrencyHandler(ICryptoCurrencyRepository cryptoCurrencyRepository, IMapper mapper)
        {
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
            _mapper = mapper;
        }


        public async Task<ValidateableResponse<CryptoCurrencyRawResponse>> Handle(AddCryptoCurrencyCommand request, CancellationToken cancellationToken)
        {
            var newCryptoCurrency = _mapper.MapAddCryptoCurrencyCommandToCryptoCurrency(request);
            var result = await _cryptoCurrencyRepository.AddNewCryptoCurrencyAsync(newCryptoCurrency, cancellationToken);
            return new ValidateableResponse<CryptoCurrencyRawResponse>(_mapper.MapCryptoCurrencyToCryptoCurrencyRawResponse(result));
        }
    }
}
