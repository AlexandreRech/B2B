using System;
using FluentValidation;
using Microservicos.B2B.Domain.ConfiguracaoModule;
using Microservicos.B2B.Domain.EmailModule;

namespace Microservicos.B2B
{
    public static class Constantes
    {
        public const string Falha_Sintatica = "FALHA_SINTATICA";
    }

    public class RecepcaoEmailMessageValidator : AbstractValidator<RecepcaoEmailMessage>
    {
        private IConfiguracaoRepository _configuracaoDao = null;
        private IRecepcaoEmailRepository _recepcaoEmailDao = null;
       
        public RecepcaoEmailMessageValidator()
        {
            RuleFor(x => x.Identificadores.Solicitante).NotNull().NotEmpty().WithErrorCode(Constantes.Falha_Sintatica);
            RuleFor(x => x.Identificadores.Processo).NotNull().NotEmpty().WithErrorCode(Constantes.Falha_Sintatica);
            RuleFor(x => x.Identificadores.Documento).NotNull().NotEmpty().WithErrorCode(Constantes.Falha_Sintatica);
            
            RuleFor(x => x.Identificadores.Processo).Must(IdentificadorProcessoNaoUtilizado)
                .WithMessage("O identificador do processo já foi utilizado");

            RuleFor(x => x.Identificadores.Solicitante).Must(SolicitanteDeveEstarCadastrado)
                .WithMessage("Solicitante não encontrado");
        }

        private bool SolicitanteDeveEstarCadastrado(Guid arg)
        {
            return _configuracaoDao.ExisteSolicitante(arg);
        }

        private bool IdentificadorProcessoNaoUtilizado(Guid numeroProcesso)
        {
            return _recepcaoEmailDao.ExisteProcesso(numeroProcesso);
        }
    }   
}