using Application.Contract.RepositoryInterfaces;
using Application.Contract.Responses;
using Application.Contract.ServicesInterfaces;
using Application.CQRS.Queries;
using Application.Maping;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Handlers.Queries
{
    public class GetAllCryptoCurrenciesHandler : IRequestHandler<GetAllCryptoCurrencies, List<CryptoCurrencyResponse>>
    {
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;
        private readonly ICryptoCurrencyExchangeService _cryptoCurrencyExchangeService;
        private readonly IMapper _mapper;

        public GetAllCryptoCurrenciesHandler(ICryptoCurrencyRepository cryptoCurrencyRepository,
            ICryptoCurrencyExchangeService cryptoCurrencyExchangeService,
            IMapper mapper)
        {
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
            _cryptoCurrencyExchangeService = cryptoCurrencyExchangeService;
            _mapper = mapper;
        }

        public async Task<List<CryptoCurrencyResponse>> Handle(GetAllCryptoCurrencies request, CancellationToken cancellationToken)
        {
            var cryptoCurrencies = await _cryptoCurrencyRepository.GetAllCryptoCurrenciesAsync(cancellationToken);

            var result = await _cryptoCurrencyExchangeService.GetCryptoListQuotsAsync(cryptoCurrencies);

            return _mapper.MapCryptoCurrencyToCryptoCurrencyResponse(result);
        }
    }
}
