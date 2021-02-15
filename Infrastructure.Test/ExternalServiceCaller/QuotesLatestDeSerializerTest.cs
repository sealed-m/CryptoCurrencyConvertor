using FluentAssertions;
using Infrastructure.ExternalServiceCaller;
using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Infrastructure.Test.ExternalServiceCaller
{
    public class QuotesLatestDeSerializerTest
    {

        [Theory]
        [InlineData("Quotes01", "BTC", "USD", 48689.956894335926)]
        [InlineData("Quotes01", "BTC", "EUR", 40172.4766649389)]
        [InlineData("Quotes03", "AAA", "USD", 0.00026173861061)]
        [InlineData("Quotes04", "AAA", "EUR", 0.00021590268338136592)]
        public void GetCryptoQuotes_ReturnCorrectData(string JsonfileName, string cryptoCode, string quote, decimal price)
        {
            //arrange
            var jsonData = "";
            using (var fileStream = new StreamReader("ExternalServiceCaller/Json/" + $"{JsonfileName}" + ".json"))
            {
                jsonData = fileStream.ReadToEnd();
            }

            var apiresult = CoinMarketQuotesLatestResponseMaker.OK(jsonData);

            //act
            var result = QuotesLatestDeSerializer.GetCryptoQuotes(apiresult, cryptoCode, quote);

            //assert
            result.Value.Should().Equals(price);
            result.Code.Should().Equals(cryptoCode);
            result.Error.Should().BeNullOrEmpty();
        }

    }
}
