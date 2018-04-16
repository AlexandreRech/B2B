using NServiceBus;
using System;

namespace Microservicos.B2B
{
    public class RecepcaoEmailProcessadoEvent : IEvent
    {
        public Guid IdentificadorProcesso { get; set; }
    }
}