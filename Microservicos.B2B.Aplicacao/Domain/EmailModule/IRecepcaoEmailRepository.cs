using System;

namespace Microservicos.B2B.Domain.EmailModule
{
    public interface IRecepcaoEmailRepository
    {
        void InserirEmail(Email email);
        void InserirAnexosEmail(AnexoEmail email);
        bool ExisteEmail(Guid identificadorEmail);        
        Email SelecionarEmail(Guid identificadorEmail);
        bool ExisteProcesso(Guid processo);
        void RegistrarEmailComFalhaSintatica(RecepcaoEmailComFalha recepcaoEmailComFalha);
        void RegistrarEmailComFalhaSemantica(RecepcaoEmailComFalha recepcaoEmailComFalha);
    }
}