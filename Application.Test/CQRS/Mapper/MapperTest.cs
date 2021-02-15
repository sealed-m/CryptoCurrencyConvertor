using Application.Contract.Requests;
using Application.CQRS.Commands;
using Application.Maping;
using Domain.Models;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Application.Test.CQRS.Mapper
{
    public class MapperTest
    {
        private readonly IMapper _mapper;
        public MapperTest()
        {
            _mapper = new Application.Maping.Mapper();
        }


        [Theory]
        [InlineData("BTC")]
        [InlineData("AAA")]
        public void MapAddCryptoCurrencyCommandToCryptoCurrency_ShouldreturnCorrectData(string code)
        {
            //arrange
            var request = new CryptoCurrencyAddRequest() { Code = code };
            var command = new AddCryptoCurrencyCommand(request);

            //act
            var result = _mapper.MapAddCryptoCurrencyCommandToCryptoCurrency(command);

            //assert
            result.Code.Should().Be(code);
            result.Id.Should().Be(default);
            result.CurrencyQuotes.Should().BeNull();
        }


        [Theory]
        [InlineData(1, "BTC")]
        [InlineData(2, "AAA")]
        public void MapCryptoCurrencyToCryptoCurrencyRawResponse_ShouldreturnCorrectData(int id, string code)
        {
            //arrange
            var request = new CryptoCurrency()
            {
                Id = id,
                Code = code
            };

            //act
            var result = _mapper.MapCryptoCurrencyToCryptoCurrencyRawResponse(request);

            //assert
            result.Code.Should().Be(code);
            result.Id.Should().Be(id);
        }


        [Theory]
        [InlineData(1, "BTC")]
        [InlineData(2, "AAA")]
        public void MapCryptoCurrencyToCryptoCurrencyResponse_ShouldreturnCorrectData(int id, string code)
        {
            //arrange
            var request = new CryptoCurrency()
            {
                Id = id,
                Code = code
            };

            //act
            var result = _mapper.MapCryptoCurrencyToCryptoCurrencyResponse(request);

            //assert
            result.Code.Should().Be(code);
            result.Id.Should().Be(id);
            result.QuoteCurrenciesResponse.Should().BeNull();
        }


        [Theory]
        [ClassData(typeof(CryptoCurrencyTestData))]
        public void MapCryptoCurrencyToCryptoCurrencyResponseList_ShouldreturnCorrectData(CryptoCurrency first, CryptoCurrency second)
        {
            //arrange
            var cryptoCurrencies = new List<CryptoCurrency>();
            cryptoCurrencies.Add(first);
            cryptoCurrencies.Add(second);

            //act
            var result = _mapper.MapCryptoCurrencyToCryptoCurrencyResponse(cryptoCurrencies);

            //assert
            result.Count.Should().Equals(cryptoCurrencies.Count);
            result.Select(x => x.QuoteCurrenciesResponse).ToList().Count.Should().Equals(cryptoCurrencies.Select(x => x.CurrencyQuotes).ToList().Count);


            result.FirstOrDefault().Id.Should().Equals(cryptoCurrencies.FirstOrDefault().Id);
            result.FirstOrDefault().Code.Should().Equals(cryptoCurrencies.FirstOrDefault().Code);
            result.LastOrDefault().Id.Should().Equals(cryptoCurrencies.LastOrDefault().Id);
            result.LastOrDefault().Code.Should().Equals(cryptoCurrencies.LastOrDefault().Code);
        }





        public class CryptoCurrencyTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return GetCryptoCurrencyList01();
                yield return GetCryptoCurrencyList02();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            private object[] GetCryptoCurrencyList01()
            {
                var list = new List<CryptoCurrency>();

                var qoutes01 = new List<CurrencyQuote>();
                var qoute01 = new CurrencyQuote() { Code = "USD", Error = string.Empty, Value = 2800 };
                var qoute02 = new CurrencyQuote() { Code = "EUR", Error = string.Empty, Value = 28850 };
                qoutes01.Add(qoute01);
                qoutes01.Add(qoute02);

                list.Add(new CryptoCurrency() { Code = "BTC", Id = 1, CurrencyQuotes = qoutes01 });


                var qoutes02 = new List<CurrencyQuote>();
                var qoute03 = new CurrencyQuote() { Code = "", Error = "Error", Value = 0 };
                var qoute04 = new CurrencyQuote() { Code = "", Error = "Error", Value = 0 };
                qoutes02.Add(qoute03);
                qoutes02.Add(qoute04);

                list.Add(new CryptoCurrency() { Code = "AAA", Id = 1, CurrencyQuotes = qoutes02 });


                return list.ToArray();
            }

            private object[] GetCryptoCurrencyList02()
            {
                var list = new List<CryptoCurrency>();

                var qoutes01 = new List<CurrencyQuote>();
                var qoute01 = new CurrencyQuote() { Code = "USD", Error = string.Empty, Value = 3569 };
                var qoute02 = new CurrencyQuote() { Code = "EUR", Error = string.Empty, Value = 28942 };
                qoutes01.Add(qoute01);
                qoutes01.Add(qoute02);

                list.Add(new CryptoCurrency() { Code = "POI", Id = 1, CurrencyQuotes = qoutes01 });


                var qoutes02 = new List<CurrencyQuote>();
                var qoute03 = new CurrencyQuote() { Code = "Test", Error = "Error", Value = 2135465 };
                var qoute04 = new CurrencyQuote() { Code = "TEst021", Error = "Error", Value = 55555 };
                qoutes02.Add(qoute03);
                qoutes02.Add(qoute04);

                list.Add(new CryptoCurrency() { Code = "DGF", Id = 1, CurrencyQuotes = qoutes02 });

                return list.ToArray();
            }


        }
    }
}
