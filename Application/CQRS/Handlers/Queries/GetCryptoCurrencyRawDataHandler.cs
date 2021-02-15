using Application.Contract.RepositoryInterfaces;
using Application.Contract.Responses;
using Application.CQRS.Queries;
using Application.Maping;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Handlers.Queries
{
    public class GetCryptoCurrencyRawDataHandler : IRequestHandler<GetCryptoCurrencyRawDataQuery, CryptoCurrencyRawResponse>
    {
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;
        private readonly IMapper _mapper;

        public GetCryptoCurrencyRawDataHandler(ICryptoCurrencyRepository cryptoCurrencyRepository,
            IMapper mapper)
        {
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
            _mapper = mapper;
        }

        public async Task<CryptoCurrencyRawResponse> Handle(GetCryptoCurrencyRawDataQuery request, CancellationToken cancellationToken)
        {
            var result = await _cryptoCurrencyRepository.GetCryptoCurrencyAsync(request.Id, cancellationToken);
            return _mapper.MapCryptoCurrencyToCryptoCurrencyRawResponse(result);
        }
    }
}
