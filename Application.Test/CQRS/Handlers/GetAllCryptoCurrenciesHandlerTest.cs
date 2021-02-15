using Application.Contract.RepositoryInterfaces;
using Application.Contract.ServicesInterfaces;
using Application.CQRS.Handlers.Queries;
using Application.Maping;
using Domain.Models;
using FakeItEasy;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Application.Test.CQRS.Handlers
{
    public class GetAllCryptoCurrenciesHandlerTest
    {
        private readonly GetAllCryptoCurrenciesHandler _testee;
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;
        private readonly ICryptoCurrencyExchangeService _cryptoCurrencyExchangeService;
        private readonly IMapper _mapper;

        public GetAllCryptoCurrenciesHandlerTest()
        {
            _cryptoCurrencyRepository = A.Fake<ICryptoCurrencyRepository>();
            _cryptoCurrencyExchangeService = A.Fake<ICryptoCurrencyExchangeService>();
            _mapper = A.Fake<IMapper>();

            _testee = new GetAllCryptoCurrenciesHandler(_cryptoCurrencyRepository, _cryptoCurrencyExchangeService, _mapper);
        }


        [Fact]
        public async void Handle_ShouldCallGetAllCryptoCurrenciesAsync()
        {
            //act
            await _testee.Handle(new Application.CQRS.Queries.GetAllCryptoCurrencies(), default);

            //assert
            A.CallTo(() => _cryptoCurrencyRepository.GetAllCryptoCurrenciesAsync(default)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async void Handle_ShouldCallGetCryptoListQuots()
        {
            //arrange
            var cryptoCurrencyBTC = new CryptoCurrency() { Code = "BTC", Id = 1, CurrencyQuotes = new List<CurrencyQuote>() };
            var result = new List<CryptoCurrency>() { cryptoCurrencyBTC };
            A.CallTo(() => _cryptoCurrencyRepository.GetAllCryptoCurrenciesAsync(default)).Returns(result);

            //act
            await _testee.Handle(new Application.CQRS.Queries.GetAllCryptoCurrencies(), default);

            //assert
            A.CallTo(() => _cryptoCurrencyExchangeService.GetCryptoListQuotsAsync(result)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldCallMapCryptoCurrencyToCryptoCurrencyResponse()
        {
            //arrange
            var cryptoCurrencyBTC = new CryptoCurrency() { Code = "BTC", Id = 1, CurrencyQuotes = new List<CurrencyQuote>() };
            var result = new List<CryptoCurrency>() { cryptoCurrencyBTC };
            A.CallTo(() => _cryptoCurrencyRepository.GetAllCryptoCurrenciesAsync(default)).Returns(result);
            A.CallTo(() => _cryptoCurrencyExchangeService.GetCryptoListQuotsAsync(result)).Returns(result);

            //act
            await _testee.Handle(new Application.CQRS.Queries.GetAllCryptoCurrencies(), default);

            //assert
            A.CallTo(() => _mapper.MapCryptoCurrencyToCryptoCurrencyResponse(result)).MustHaveHappenedOnceExactly();
        }


        [Theory]
        [InlineData("AAA", 1, "AAA", 1)]
        [InlineData("BTC", 2, "BTC", 2)]
        public async void Handle_ShouldReturnCryptoListQuots(string codeIn, int idIn, string codeOut, int idOut)
        {
            //arrange
            var cryptoCurrencyBTC = new CryptoCurrency() { Code = codeIn, Id = idIn, CurrencyQuotes = new List<CurrencyQuote>() };
            var result = new List<CryptoCurrency>() { cryptoCurrencyBTC };
            A.CallTo(() => _cryptoCurrencyRepository.GetAllCryptoCurrenciesAsync(default)).Returns(result);

            A.CallTo(() => _cryptoCurrencyExchangeService.GetCryptoListQuotsAsync(result)).Returns(result);
            
            var respone = new Contract.Responses.CryptoCurrencyResponse() { Code = codeIn, Id = idIn, QuoteCurrenciesResponse = new List<Contract.Responses.QuoteCurrencyResponse>() };
            var mapperResult = new List<Contract.Responses.CryptoCurrencyResponse>() { respone };
            A.CallTo(() => _mapper.MapCryptoCurrencyToCryptoCurrencyResponse(result)).Returns(mapperResult);

            //act
            var handleResult = await _testee.Handle(new Application.CQRS.Queries.GetAllCryptoCurrencies(), default);

            //assert
            handleResult.Should().BeOfType<List<Contract.Responses.CryptoCurrencyResponse>>();
            handleResult.Should().HaveCount(1);
            handleResult.Should().ContainSingle(x => x.Code == codeOut && x.Id == idOut);
        }
    }
}
