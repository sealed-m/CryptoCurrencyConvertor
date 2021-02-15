using Application.Contract.RepositoryInterfaces;
using Application.CQRS.Commands;
using Application.CQRS.Validations;
using Domain.Models;
using FakeItEasy;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Test.CQRS.Validations
{
    public class RemoveCryptoCurrencyValidationTest
    {

        private readonly RemoveCryptoCurrencyValidation _testee;
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;

        public RemoveCryptoCurrencyValidationTest()
        {
            _cryptoCurrencyRepository = A.Fake<ICryptoCurrencyRepository>();
            _testee = new RemoveCryptoCurrencyValidation(_cryptoCurrencyRepository);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void Code_WhenThereIsNotInDataBase_ShouldHaveValidationError(int id)
        {
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyAsync(id, default)).Returns<CryptoCurrency>(null);

            (await _testee.ShouldHaveValidationErrorForAsync(x => x.Id, new RemoveCryptoCurrencyCommand(id)))
                .WithErrorMessage("The 'Code' does not exist!");
        }

    }
}
