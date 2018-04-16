using Microservicos.B2B.Domain.EmailModule;
using System;

namespace Microservicos.B2B
{
    public class RecepcaoEmailRepository : IRecepcaoEmailRepository
    {
        public bool ExisteEmail(Guid identificadorEmail)
        {
            return false;
        }

        public bool ExisteProcesso(Guid processo)
        {
            return false;
        }

        public void InserirAnexosEmail(AnexoEmail email)
        {
            
        }

        public void InserirEmail(Email email)
        {

        }       
        public Email SelecionarEmail(Guid identificadorEmail)
        {
            return new Email();
        }
    }
}