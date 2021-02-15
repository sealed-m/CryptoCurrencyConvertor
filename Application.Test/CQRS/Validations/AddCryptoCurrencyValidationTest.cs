using Application.Contract.RepositoryInterfaces;
using Application.Contract.Requests;
using Application.CQRS.Commands;
using Application.CQRS.Validations;
using Domain.Models;
using FakeItEasy;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Test.CQRS.Validations
{
    public class AddCryptoCurrencyValidationTest
    {
        private readonly AddCryptoCurrencyValidation _testee;
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;

        public AddCryptoCurrencyValidationTest()
        {
            _cryptoCurrencyRepository = A.Fake<ICryptoCurrencyRepository>();
            _testee = new AddCryptoCurrencyValidation(_cryptoCurrencyRepository);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("bvbdd")]
        [InlineData("ASDF")]
        [InlineData("1231wee")]
        public async void Code_WhenNullOrNotThreeCharachter_ShouldHaveValidationError(string code)
        {
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyByCodeAsync(code, default)).Returns<CryptoCurrency>(null);

            var request = new CryptoCurrencyAddRequest() { Code = code };
            await _testee.ShouldHaveValidationErrorForAsync(x => x.Code, new AddCryptoCurrencyCommand(request));
        }


        [Theory]
        [InlineData("aaa")]
        [InlineData("123")]
        [InlineData("APa")]
        [InlineData("FeD")]
        [InlineData("1AD")]
        [InlineData("kLO")]
        public async void Code_WhenAllThreeCharachtersAreNotUpperCaseLetters_ShouldHaveValidationError(string code)
        {
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyByCodeAsync(code, default)).Returns<CryptoCurrency>(null);

            var request = new CryptoCurrencyAddRequest() { Code = code };
            (await _testee.ShouldHaveValidationErrorForAsync(x => x.Code, new AddCryptoCurrencyCommand(request)))
                .WithErrorMessage("The 'Code' must be 3 upper case letters");
        }


        [Theory]
        [InlineData("BTC")]
        [InlineData("AAA")]
        [InlineData("ADG")]
        public async void Code_WhenAllThreeCharachtersAreUpperCaseLetters_ShouldNotHaveValidationError(string code)
        {
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyByCodeAsync(code, default)).Returns<CryptoCurrency>(null);

            var request = new CryptoCurrencyAddRequest() { Code = code };
            await _testee.ShouldNotHaveValidationErrorForAsync(x => x.Code, new AddCryptoCurrencyCommand(request));
        }

        [Theory]
        [InlineData("BTC")]
        [InlineData("AAA")]
        [InlineData("ADG")]
        public async void Code_WhenThreIsInDataBase_ShouldHaveValidationError(string code)
        {
            var respone = new CryptoCurrency() { Code = code };
            A.CallTo(() => _cryptoCurrencyRepository.GetCryptoCurrencyByCodeAsync(code, default)).Returns(respone);

            var request = new CryptoCurrencyAddRequest() { Code = code };
            (await _testee.ShouldHaveValidationErrorForAsync(x => x.Code, new AddCryptoCurrencyCommand(request)))
                .WithErrorMessage("The 'Code' already exist!");
        }
    }
}
