using Application.Contract.RepositoryInterfaces;
using Application.CQRS.Handlers.Commands;
using Application.Maping;
using Domain.Models;
using FakeItEasy;
using Xunit;

namespace Application.Test.CQRS.Handlers
{
    public class AddCryptoCurrencyHandlerTest
    {
        private readonly AddCryptoCurrencyHandler _testee;
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;
        private readonly IMapper _mapper;

        public AddCryptoCurrencyHandlerTest()
        {
            _cryptoCurrencyRepository = A.Fake<ICryptoCurrencyRepository>();
            _mapper = A.Fake<IMapper>();

            _testee = new AddCryptoCurrencyHandler(_cryptoCurrencyRepository, _mapper);
        }


        [Fact]
        public async void Handle_ShouldCallMapAddCryptoCurrencyCommandToCryptoCurrency()
        {
            //arrange
            var request = new Contract.Requests.CryptoCurrencyAddRequest() { Code = "BTC" };
            var command = new Application.CQRS.Commands.AddCryptoCurrencyCommand(request);
         
            //act
            await _testee.Handle(command, default);

            //assert
            A.CallTo(() => _mapper.MapAddCryptoCurrencyCommandToCryptoCurrency(command)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async void Handle_ShouldCallAddNewCryptoCurrencyAsync()
        {
            //arrange
            var request = new Contract.Requests.CryptoCurrencyAddRequest() { Code = "BTC" };
            var command = new Application.CQRS.Commands.AddCryptoCurrencyCommand(request);
            var newCryptoCurrency = new CryptoCurrency() { Code = command.Code };
            A.CallTo(() => _mapper.MapAddCryptoCurrencyCommandToCryptoCurrency(command)).Returns(newCryptoCurrency);
            //act
            await _testee.Handle(command, default);

            //assert
            A.CallTo(() => _cryptoCurrencyRepository.AddNewCryptoCurrencyAsync(newCryptoCurrency, default)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async void Handle_ShouldCallMapCryptoCurrencyToCryptoCurrencyRawResponse()
        {
            //arrange
            var request = new Contract.Requests.CryptoCurrencyAddRequest() { Code = "BTC" };
            var command = new Application.CQRS.Commands.AddCryptoCurrencyCommand(request);
            var newCryptoCurrency = new CryptoCurrency() { Code = command.Code };
            var response = new CryptoCurrency() { Code = command.Code };
            A.CallTo(() => _mapper.MapAddCryptoCurrencyCommandToCryptoCurrency(command)).Returns(newCryptoCurrency);
            A.CallTo(() => _cryptoCurrencyRepository.AddNewCryptoCurrencyAsync(newCryptoCurrency, default)).Returns(response);
            //act
            await _testee.Handle(command, default);

            //assert
            A.CallTo(() => _mapper.MapCryptoCurrencyToCryptoCurrencyRawResponse(response)).MustHaveHappenedOnceExactly();
        }

    }
}
