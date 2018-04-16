using FluentValidation;

namespace Microservicos.B2B.Domain.EmailModule
{
    public class EnderecoEmailValidator : AbstractValidator<EnderecoEmail>
    {
        public EnderecoEmailValidator()
        {
            RuleFor(x => x.Endereco).NotEmpty().EmailAddress();
        }
    }
}
