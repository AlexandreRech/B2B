using FluentValidation.Attributes;
using Microservicos.B2B.Messages.DTOs;
using NServiceBus;
using System;
using System.Collections.Generic;

namespace Microservicos.B2B
{    
    [Validator(typeof(RecepcaoEmailMessageValidator))]
    public class RecepcaoEmailMessage : IMessage
    {
        public IdentificadoresDTO Identificadores { get; set; }

        public int QuantidadeAnexos { get; set; }
        
        public List<string> Destinatarios { get; set; }

        public List<string> DestinatariosEmCopia { get; set; }

        public List<string> DestinatariosEmCopiaOculta { get; set; }

        public string Assunto { get; set; }

        public string CorpoEmail { get; set; }

        public string Remetente { get; set; }

    }
}