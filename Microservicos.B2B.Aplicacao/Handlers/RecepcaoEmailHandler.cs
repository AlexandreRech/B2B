using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microservicos.B2B.Domain.ConfiguracaoModule;
using Microservicos.B2B.Domain.EmailModule;
using Microservicos.B2B.Domain.MensagensModule;
using Microservicos.B2B.Messages;
using Microservicos.B2B.Messages.Events;
using NServiceBus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicos.B2B
{
    public class RecepcaoEmailHandler : IHandleMessages<RecepcaoEmailMessage>
    {
        private IConfiguracaoRepository _configuracaoDao = null;
        private IRecepcaoEmailRepository _recepcaoEmailDao;
        private IMapper _b2bMapper = null;
        private IValidator<RecepcaoEmailMessageValidator> _validador;
        private IMensagemRepository _mensagemDao;

        public RecepcaoEmailHandler(
            IConfiguracaoRepository configuracaoDao,
            IMapper b2bMapper,
            IRecepcaoEmailRepository recepcaoEmailDao,
            IValidator<RecepcaoEmailMessageValidator> validador)
        {
            _configuracaoDao = configuracaoDao;
            _b2bMapper = b2bMapper;
            _recepcaoEmailDao = recepcaoEmailDao;
            _validador = validador;
        }        
        
        public Task Handle(RecepcaoEmailMessage msgEmail, IMessageHandlerContext context)
        {
            Email email = _b2bMapper.Map<Email>(msgEmail);

            IEvent @event = ValidarMensagem(msgEmail, context.MessageId, email);

            if (@event != null)
            {
                context.Publish(@event);

                return Task.CompletedTask;
            }

            _recepcaoEmailDao.InserirEmail(email);

            if (msgEmail.QuantidadeAnexos == 0)
            {
                SolicitacaoEnvioEmailCommand solicitacaoEnvioEmail = new SolicitacaoEnvioEmailCommand();
                context.Send(solicitacaoEnvioEmail);
            }

            context.Publish(new RecepcaoEmailProcessadoEvent { IdentificadorProcesso = msgEmail.Identificadores.Processo });

            return Task.CompletedTask;
        }

        private IEvent ValidarMensagem(RecepcaoEmailMessage msgEmail, string contextMessageId, Email email)
        {
            ValidationResult resultadoValidacao = _validador.Validate(msgEmail);

            var errosSintaticos = resultadoValidacao.Errors.Where(x => x.ErrorCode == Constantes.Falha_Sintatica);

            var errosSemanticos = resultadoValidacao.Errors.Where(x => x.ErrorCode != Constantes.Falha_Sintatica);
            
            var ids = msgEmail.Identificadores;

            var recepcaoEmailComFalha = new RecepcaoEmailComFalha(contextMessageId, ids.Solicitante, ids.Processo, ids.Documento, email);

            IEvent evento = null;

            if (errosSintaticos.Any())
            {                              
                recepcaoEmailComFalha.RegistrarListaErros(errosSintaticos.Select(x => x.ErrorMessage));

                evento = _b2bMapper.Map<RecepcaoEmailComFalhaSintaticaEvent>(recepcaoEmailComFalha);

                _recepcaoEmailDao.RegistrarEmailComFalhaSintatica(recepcaoEmailComFalha);
            }
            else if (errosSemanticos.Any())
            {
                recepcaoEmailComFalha.RegistrarListaErros(errosSemanticos.Select(x => x.ErrorMessage));

                evento = _b2bMapper.Map<RecepcaoEmailComFalhaSemanticaEvent>(recepcaoEmailComFalha);

                _recepcaoEmailDao.RegistrarEmailComFalhaSemantica(recepcaoEmailComFalha);
            }

            return evento;
        }

    }
}