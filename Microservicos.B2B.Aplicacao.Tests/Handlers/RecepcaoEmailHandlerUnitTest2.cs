using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using NServiceBus.Testing;
using Moq;
using FluentAssertions;
using Microservicos.B2B.Domain;
using Microservicos.B2B.Messages.DTOs;
using Microservicos.B2B.Domain.ConfiguracaoModule;
using Microservicos.B2B.Domain.MensagensModule;
using AutoMapper;
using Microservicos.B2B.Domain.EmailModule;
using Microservicos.B2B.Messages;
using Microservicos.B2B.Messages.Events;
using FluentValidation;

namespace Microservicos.B2B.Aplicacao.Tests.Handlers
{
    [TestClass]
    public class RecepcaoEmailHandlerUnitTest2
    {
        private RecepcaoEmailHandler handler;
        private TestableMessageHandlerContext context;
        private RecepcaoEmailMessage message;

        private Mock<IConfiguracaoRepository> mockConfiguracaoRepository;
        private Mock<IMensagemRepository> mockMensagemRepository;
        private Mock<IMapper> mockMapper;
        private Mock<IRecepcaoEmailRepository> mockRecepcaoEmailRepository;
        private Mock<IValidator<RecepcaoEmailMessageValidator>> mockRecepcaoEmailValidator;

        public RecepcaoEmailHandlerUnitTest2()
        {
            mockConfiguracaoRepository = new Mock<IConfiguracaoRepository>();
            mockMensagemRepository = new Mock<IMensagemRepository>();
            mockMapper = new Mock<IMapper>();
            mockRecepcaoEmailRepository = new Mock<IRecepcaoEmailRepository>();
            mockRecepcaoEmailValidator = new Mock<IValidator<RecepcaoEmailMessageValidator>>();

            handler = new RecepcaoEmailHandler(
                mockConfiguracaoRepository.Object,
                mockMapper.Object,
                mockRecepcaoEmailRepository.Object,
                mockRecepcaoEmailValidator.Object);

            context = new TestableMessageHandlerContext();

            message = new RecepcaoEmailMessage
            {
                QuantidadeAnexos = 1,
                Identificadores = new IdentificadoresDTO
                {
                    Solicitante = Guid.NewGuid(),
                    Processo = Guid.NewGuid(),
                    Documento = Guid.NewGuid()
                }
            };
        }

        [TestMethod]
        public void Deve_Validar_Mensagem_Com_Falha_Sintatica()
        {
            //arrange
            message.Identificadores.Solicitante = Guid.Empty;
            mockMapper.Setup(x => x.Map<RecepcaoEmailComFalhaSintaticaEvent>(It.IsAny<object>()))
                .Returns(new RecepcaoEmailComFalhaSintaticaEvent());

            //action
            handler.Handle(message, context);

            //assert
            Assert.AreEqual(1, context.PublishedMessages.Length);
            Assert.AreEqual(0, context.SentMessages.Length);

            mockMensagemRepository.Verify(x => x.RegistrarMensagemComFalhaSintatica(It.IsAny<RecepcaoEmailComFalha>()));

            Assert.IsInstanceOfType(context.PublishedMessages[0].Message, typeof(RecepcaoEmailComFalhaSintaticaEvent));
        }

        [TestMethod]
        public void Deve_Validar_Mensagem_Com_Falha_Semantica()
        {
            //arrange
            mockConfiguracaoRepository.Setup(x => x.ExisteSolicitante(It.IsAny<Guid>())).Returns(false);

            mockMapper.Setup(x => x.Map<RecepcaoEmailComFalhaSemanticaEvent>(It.IsAny<object>()))
                .Returns(new RecepcaoEmailComFalhaSemanticaEvent());

            //action
            handler.Handle(message, context);

            //assert
            Assert.AreEqual(1, context.PublishedMessages.Length);
            Assert.AreEqual(0, context.SentMessages.Length);

            mockMensagemRepository.Verify(x => x.RegistrarEmailComFalhaSemantica(It.IsAny<RecepcaoEmailComFalha>()));

            Assert.IsInstanceOfType(context.PublishedMessages[0].Message, typeof(RecepcaoEmailComFalhaSemanticaEvent));
        }

        [TestMethod]
        public void Deve_Validar_Mensagem_Com_Falha_De_Negocio()
        {
            //arrange
            mockConfiguracaoRepository.Setup(x => x.ExisteSolicitante(It.IsAny<Guid>())).Returns(true);

            mockMapper.Setup(x => x.Map<RecepcaoEmailComFalhaSemanticaEvent>(It.IsAny<object>()))
                .Returns(new RecepcaoEmailComFalhaSemanticaEvent());

            //action
            handler.Handle(message, context);

            //assert
            Assert.AreEqual(1, context.PublishedMessages.Length);
            Assert.AreEqual(0, context.SentMessages.Length);

            mockMensagemRepository.Verify(x => x.RegistrarEmailComFalhaSemantica(It.IsAny<RecepcaoEmailComFalha>()));

            Assert.IsInstanceOfType(context.PublishedMessages[0].Message, typeof(RecepcaoEmailComFalhaSemanticaEvent));
        }
    }
}
