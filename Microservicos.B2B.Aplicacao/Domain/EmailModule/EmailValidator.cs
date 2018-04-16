using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.Domain.EmailModule
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Destinatarios).NotNull();

            RuleForEach(x => x.Destinatarios).NotNull()
                .SetValidator(new EnderecoEmailValidator());

            RuleFor(x => x.Remetente).SetValidator(new EnderecoEmailValidator());

            //esta regra poderá ser reprocessado         
        }

      
    }
}
