using NServiceBus;
using Microservicos.B2B.Messages.DTOs;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Microservicos.B2B.Messages
{
    public class RecepcaoEmailComFalhaSintaticaEvent : IEvent
    {
        public IdentificadoresDTO Identificadores { get; set; }
        public string MessageId { get; set; }
        public List<ItemErroDTO> Erros { get; set; }
    }
}
