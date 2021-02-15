using Application.Contract.Responses;
using Application.CQRS.Commands;
using Application.CQRS.Handlers.Commands;
using Application.CQRS.Handlers.Queries;
using Application.CQRS.Pipelines;
using Application.CQRS.Queries;
using Application.Maping;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Application
{
    public static class ConfigurationStartup
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>();

            services.AddTransient<IRequestHandler<GetAllCryptoCurrencies, List<CryptoCurrencyResponse>>, GetAllCryptoCurrenciesHandler>();
            services.AddTransient<IRequestHandler<GetCryptoCurrencyRawDataQuery, CryptoCurrencyRawResponse>, GetCryptoCurrencyRawDataHandler>();
            services.AddTransient<IRequestHandler<AddCryptoCurrencyCommand, ValidateableResponse<CryptoCurrencyRawResponse>>, AddCryptoCurrencyHandler>();
            services.AddTransient<IRequestHandler<RemoveCryptoCurrencyCommand, ValidateableResponse<CryptoCurrencyDeleteResponse>>, RemoveCryptoCurrencyHandler>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(ConfigurationStartup).Assembly);
        }

    }
}
