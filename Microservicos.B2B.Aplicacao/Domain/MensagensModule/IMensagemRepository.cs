using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microservicos.B2B.Domain.EmailModule;

namespace Microservicos.B2B.Domain.MensagensModule
{
    public interface IMensagemRepository
    {     
        void RegistrarMensagemComFalhaSintatica(RecepcaoEmailComFalha recepcaoEmailComFalha);
        void RegistrarEmailComFalhaSemantica(RecepcaoEmailComFalha recepcaoEmailComFalha);
    }
}
