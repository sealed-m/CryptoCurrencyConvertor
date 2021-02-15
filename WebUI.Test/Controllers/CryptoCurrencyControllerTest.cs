using Application.Contract.Responses;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebUI.Controllers;
using Xunit;

namespace WebUI.Test.Controllers
{
    public class CryptoCurrencyControllerTest
    {
        private readonly IMediator _mediator;
        private readonly CryptoCurrencyController _testee;

        public CryptoCurrencyControllerTest()
        {
            _mediator = A.Fake<IMediator>();
            _testee = new CryptoCurrencyController(_mediator);

            var currencyQoute = new QuoteCurrencyResponse()
            {
                CurrencyCode = "USD",
                HasError = false,
                ErrorMessage = string.Empty,
                Price = 18000
            };

            var cryptoCurrencyBTC = new CryptoCurrencyResponse()
            {
                Id = 1,
                Code = "BTC",
                QuoteCurrenciesResponse = new List<QuoteCurrencyResponse>() { currencyQoute }
            };
            var cryptoCurrencyAAA = new CryptoCurrencyResponse()
            {
                Id = 2,
                Code = "AAA",
                QuoteCurrenciesResponse = new List<QuoteCurrencyResponse>() { currencyQoute }
            };

            var cryptoCurrencies = new List<CryptoCurrencyResponse>();
            cryptoCurrencies.Add(cryptoCurrencyBTC);
            cryptoCurrencies.Add(cryptoCurrencyAAA);


            A.CallTo(() => _mediator.Send(A<GetAllCryptoCurrencies>._, default)).Returns(cryptoCurrencies);
        }



        [Fact]
        public async void Index_ShouldReturnValidView()
        {
            //act
            var result = await _testee.Index();

            //assert
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult)?.ViewData.Model.Should().BeOfType<List<CryptoCurrencyResponse>>();
            ((result as ViewResult).ViewData.Model as List<CryptoCurrencyResponse>).Count.Should().Be(2);
        }


        [Fact]
        public async void Create_ShouldHaveValidationError()
        {
            //arrange
            var errors = new List<string>() { "Invalid cerate request" };
            var AddCryptoCurrencyValidateableResponse = new ValidateableResponse<CryptoCurrencyRawResponse>(default, errors);
            A.CallTo(() => _mediator.Send(A<AddCryptoCurrencyCommand>._, default)).Returns(AddCryptoCurrencyValidateableResponse);

            //act
            var result = await _testee.Create(new Application.Contract.Requests.CryptoCurrencyAddRequest() { Code = "Btc" });

            //assert
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).ViewData.Model.Should().BeNull();
            (result as ViewResult).ViewData.Values.Should().HaveCount(1);
            (result as ViewResult).ViewData.Values.Should().ContainSingle("Invalid cerate request");
        }


        [Fact]
        public async void Delete_ShouldHaveValidationError()
        {
            //arrange
            var errors = new List<string>() { "Invalid delete request" };
            var removeCryptoCurrencyValidateableResponse = new ValidateableResponse<CryptoCurrencyDeleteResponse>(default, errors);
            A.CallTo(() => _mediator.Send(A<RemoveCryptoCurrencyCommand>._, default)).Returns(removeCryptoCurrencyValidateableResponse);

            //act
            var result = await _testee.Delete(1, null);

            //assert
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).ViewData.Model.Should().BeNull();
            (result as ViewResult).ViewData.Values.Should().HaveCount(1);
            (result as ViewResult).ViewData.Values.Should().ContainSingle("Invalid delete request");
        }

    }
}
