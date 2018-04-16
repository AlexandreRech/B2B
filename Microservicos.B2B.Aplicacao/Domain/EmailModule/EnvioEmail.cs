using FluentValidation.Attributes;
using System;

namespace Microservicos.B2B.Domain.EmailModule
{
    [Validator(typeof(EnvioEmailValidator))]
    public class EnvioEmail
    {
        public Guid IdentificadorProcesso { get; set; }

        public Guid IdentificadorAplicacaoOrigem { get; set; }

        public ContaEmail Conta { get; set; }

        public Email Email { get; set; }

        public DateTime DataEnvio { get; set; }
    }
}