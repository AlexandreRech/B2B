using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.Domain.EmailModule
{
    [Validator(typeof(EnderecoEmailValidator))]
    public class EnderecoEmail
    {
        public string Endereco { get; set; }
    }
}
