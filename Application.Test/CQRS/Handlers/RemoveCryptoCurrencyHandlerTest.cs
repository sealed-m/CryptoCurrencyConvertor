using Application.Contract.RepositoryInterfaces;
using Application.CQRS.Handlers.Commands;
using FakeItEasy;
using Xunit;

namespace Application.Test.CQRS.Handlers
{
    public class RemoveCryptoCurrencyHandlerTest
    {
        private readonly RemoveCryptoCurrencyHandler _testee;
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;

        public RemoveCryptoCurrencyHandlerTest()
        {
            _cryptoCurrencyRepository = A.Fake<ICryptoCurrencyRepository>();

            _testee = new RemoveCryptoCurrencyHandler(_cryptoCurrencyRepository);
        }


        [Fact]
        public async void Handle_ShouldCallAddNewCryptoCurrencyAsync()
        {
            //arrange
            var command = new Application.CQRS.Commands.RemoveCryptoCurrencyCommand(1);

            //act
            await _testee.Handle(command, default);

            //assert
            A.CallTo(() => _cryptoCurrencyRepository.RemoveCryptoCurrencyAsync(command.Id, default)).MustHaveHappenedOnceExactly();
        }
    }
}
