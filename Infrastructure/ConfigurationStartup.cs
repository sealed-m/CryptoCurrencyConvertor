using Application.Contract.RepositoryInterfaces;
using Application.Contract.ServicesInterfaces;
using Infrastructure.Context;
using Infrastructure.ExternalServiceCaller;
using Infrastructure.ExternalServiceCaller.Proxy;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigurationStartup
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CryptoCurrencyContext>(option => option.UseInMemoryDatabase("knab-cryptocurrency-in-memory-db"));

            var coinmarketCapOptions = new CoinmarketCapConfig();
            configuration.Bind(nameof(CoinmarketCapConfig), coinmarketCapOptions);
            services.AddSingleton(coinmarketCapOptions);

            services.AddTransient<ICryptoCurrencyRepository, CryptoCurrencyRepository>();

            services.AddSingleton<ICryptoCurrencyExchangeService, CoinmarketCapService>();
            services.Decorate<ICryptoCurrencyExchangeService, CachedCoinmarketCapService>();
        }

    }
}
