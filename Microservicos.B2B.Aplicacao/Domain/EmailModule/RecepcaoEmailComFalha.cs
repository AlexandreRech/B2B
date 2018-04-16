using FluentValidation.Results;
using Microservicos.B2B.Domain.EmailModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicos.B2B.Domain.EmailModule
{
    public class RecepcaoEmailComFalha
    {
        private Email _email;

        private string _identificadorMensagem;
        private Guid _identificadorSolicitante;
        private Guid _identificadorProcesso;
        private Guid _identificadorDocumento;

        private List<string> _erros;
        

        public RecepcaoEmailComFalha(string idMensagem, Guid idSolicitante, Guid idProcesso, Guid idDocumento, Email email)
        {
            _email = email;

            _identificadorMensagem = idMensagem;
            _identificadorSolicitante = idSolicitante;
            _identificadorProcesso = idProcesso;
            _identificadorDocumento = idDocumento;

            _erros = new List<string>();
        }

        public void RegistrarErro(string erro)
        {
            _erros.Add(erro);
        }

        internal bool TemErroRegistrado()
        {
            return _erros.Count > 0;
        }

        public void RegistrarListaErros(IEnumerable<string> erros)
        {
            _erros.AddRange(erros);
        }
    }

    
}