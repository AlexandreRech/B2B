using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservicos.B2B.Messages.DTOs;

namespace Microservicos.B2B.Messages.Events
{
    public class RecepcaoEmailComFalhaSemanticaEvent : IEvent
    {
        public IdentificadoresDTO Identificadores { get; internal set; }
    }
}
