using Application.Contract.RepositoryInterfaces;
using Application.CQRS.Commands;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Validations
{
    public class RemoveCryptoCurrencyValidation : AbstractValidator<RemoveCryptoCurrencyCommand>
    {
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;

        public RemoveCryptoCurrencyValidation(ICryptoCurrencyRepository cryptoCurrencyRepository)
        {
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
        }

        public override Task<FluentValidation.Results.ValidationResult> ValidateAsync(ValidationContext<RemoveCryptoCurrencyCommand> context, CancellationToken cancellation = default)
        {
            RuleFor(x => x.Id)
              .MustAsync(ContainCryptoCurrencyAlready)
              .WithMessage("The 'Code' does not exist!");

            return base.ValidateAsync(context, cancellation);
        }

        private async Task<bool> ContainCryptoCurrencyAlready(RemoveCryptoCurrencyCommand command, int id, CancellationToken cancellation)
        {
            var result = await _cryptoCurrencyRepository.GetCryptoCurrencyAsync(id, cancellation);
            return result != default;
        }
    }
}
