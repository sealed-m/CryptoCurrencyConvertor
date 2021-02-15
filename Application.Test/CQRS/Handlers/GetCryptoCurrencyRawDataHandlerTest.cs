using Application.Contract.RepositoryInterfaces;
using Application.CQRS.Handlers.Queries;
using Application.Maping;
using Domain.Models;
using FakeItEasy;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Application.Test.CQRS.Handlers
{
    public class GetCryptoCurrencyRawDataHandlerTest
    {
        private readonly GetCryptoCurrencyRawDataHandler _testee;
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;
        private readonly IMapper _mapper;

        public GetCryptoCurrencyRawDataHandlerTest()
        {
            _cryptoCurrencyRepository = A.Fake<ICryptoCurrencyRepository>();
            _mapper = A.Fake<IMapper>();

            _testee = new GetCryptoCurrencyRawDataHandler(_cryptoCurrencyRepository, _mapper);
        }

        [Fact]
        public async void Handle_ShouldCallGetCryptoCurrencyAsync()
        {
            //act
            await _testee.Handle(new Application.CQRS.Queries.GetCryptoCurrencyRawDataQuery(1), default);

            //assert
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyAsync(1, default)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldCallMapCryptoCurrencyToCryptoCurrencyRawResponse()
        {
            //arrange
            var cryptoCurrencyBTC = new CryptoCurrency() { Code = "BTC", Id = 1, CurrencyQuotes = new List<CurrencyQuote>() };
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyAsync(1, default)).Returns(cryptoCurrencyBTC);

            //act
            await _testee.Handle(new Application.CQRS.Queries.GetCryptoCurrencyRawDataQuery(1), default);

            //assert
            A.CallTo(() => _mapper.MapCryptoCurrencyToCryptoCurrencyRawResponse(cryptoCurrencyBTC)).MustHaveHappenedOnceExactly();
        }


        [Theory]
        [InlineData("AAA", 1, "AAA", 1)]
        [InlineData("BTC", 2, "BTC", 2)]
        public async void Handle_ShouldReturnCryptoCurrencyRaw(string codeIn, int idIn, string codeOut, int idOut)
        {
            //arrange
            var cryptoCurrencyBTC = new CryptoCurrency() { Code = codeIn, Id = idIn, CurrencyQuotes = new List<CurrencyQuote>() };
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyAsync(1, default)).Returns(cryptoCurrencyBTC);

            var respone = new Contract.Responses.CryptoCurrencyRawResponse() { Code = codeIn, Id = idIn };
            A.CallTo(() => _mapper.MapCryptoCurrencyToCryptoCurrencyRawResponse(cryptoCurrencyBTC)).Returns(respone);

            //act
            var handleResult = await _testee.Handle(new Application.CQRS.Queries.GetCryptoCurrencyRawDataQuery(1), default);

            //assert
            handleResult.Should().BeOfType<Contract.Responses.CryptoCurrencyRawResponse>();
            handleResult.Id.Should().Be(idOut);
            handleResult.Code.Should().Be(codeOut);
        }
    }
}
