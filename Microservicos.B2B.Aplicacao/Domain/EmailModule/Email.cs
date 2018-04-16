using FluentResults;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;

namespace Microservicos.B2B.Domain.EmailModule
{
    [Validator(typeof(EmailValidator))]
    public class Email : Entity
    {
        private Email()
        {
        }

        public Guid IdentificadorEmail { get; private set; }

        public AplicacaoSolicitante Solicitante { get; set; }

        public List<EnderecoEmail> Destinatarios { get; private set; }

        public List<EnderecoEmail> DestinatariosEmCopia { get; private set; }

        public List<EnderecoEmail> DestinatariosEmCopiaOculta { get; private set; }

        public string Assunto { get; private set; }

        public string CorpoEmail { get; private set; }

        public EnderecoEmail Remetente { get; private set; }
        
        public static Result<Email> Create(Guid identificadorEmail, )
        {

        }

    }

   
}