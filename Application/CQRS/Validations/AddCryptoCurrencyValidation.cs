using Application.Contract.RepositoryInterfaces;
using Application.CQRS.Commands;
using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Validations
{
    public class AddCryptoCurrencyValidation : AbstractValidator<AddCryptoCurrencyCommand>
    {
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;

        public AddCryptoCurrencyValidation(ICryptoCurrencyRepository cryptoCurrencyRepository)
        {
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
        }

        public async override Task<ValidationResult> ValidateAsync(ValidationContext<AddCryptoCurrencyCommand> context, CancellationToken cancellation = default)
        {
            RuleFor(x => x.Code)
              .NotNull()
              .Length(3);

            RuleFor(x => x.Code)
              .Matches(new Regex("^[A-Z]+$"))
              .WithMessage("The 'Code' must be 3 upper case letters");


            RuleFor(x => x.Code)
              .MustAsync(NotContainCryptoCurrencyAlready)
              .WithMessage("The 'Code' already exist!");

            return await base.ValidateAsync(context, cancellation);
        }


        private async Task<bool> NotContainCryptoCurrencyAlready(AddCryptoCurrencyCommand command, string code, CancellationToken cancellation)
        {
            var result = await _cryptoCurrencyRepository.GetCryptoCurrencyByCodeAsync(code, cancellation);
            return result == default;
        }
    }
}
