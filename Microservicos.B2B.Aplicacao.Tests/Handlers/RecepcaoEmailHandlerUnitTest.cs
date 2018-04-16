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

namespace Microservicos.B2B.Aplicacao.Tests.Handlers
{
    [TestClass]
    public class RecepcaoEmailHandlerUnitTest
    {
        private RecepcaoEmailHandler handler;
        private TestableMessageHandlerContext context;
        private RecepcaoEmailMessage message;

        private Mock<IConfiguracaoRepository> mockConfiguracaoRepository;
        private Mock<IMensagemRepository> mockMensagemRepository;
        private Mock<IMapper> mockMapper;
        private Mock<IRecepcaoEmailRepository> mockRecepcaoEmailRepository;

        public RecepcaoEmailHandlerUnitTest()
        {            
            mockConfiguracaoRepository = new Mock<IConfiguracaoRepository>();
            mockMensagemRepository = new Mock<IMensagemRepository>();
            mockMapper = new Mock<IMapper>();
            mockRecepcaoEmailRepository = new Mock<IRecepcaoEmailRepository>();

            handler = new RecepcaoEmailHandler(
                mockConfiguracaoRepository.Object, 
                mockMensagemRepository.Object,
                mockMapper.Object,  
                mockRecepcaoEmailRepository.Object);

            context = new TestableMessageHandlerContext();

            message = new RecepcaoEmailMessage
            {
                QuantidadeAnexos = 1,
                Identificadores = new IdentificadoresDTO
                {
                    Solicitante = Guid.NewGuid(),
                    Processo = Guid.NewGuid()
                }                
            };

        }

        [TestMethod]
        public async Task Handler_Deve_Processar_Recepcao_Email_Com_Anexos()
        {
            message.QuantidadeAnexos = 1;

            await handler.Handle(message, context).ConfigureAwait(false);

            //mockService.Verify(x => x.ProcessarRecepcaoEmailComAnexos(message), Times.Once());
            //mockService.Verify(x => x.ProcessarRecepcaoEmailSemAnexos(message), Times.Never());

            Assert.AreEqual(1, context.PublishedMessages.Length);
            Assert.AreEqual(0, context.SentMessages.Length);

            Assert.IsInstanceOfType(context.PublishedMessages[0].Message, typeof(RecepcaoEmailProcessadoEvent));
        }

        [TestMethod]
        public async Task Handler_Deve_Processar_Recepcao_Email_Sem_Anexos()
        {
            message.QuantidadeAnexos = 0;

            await handler.Handle(message, context).ConfigureAwait(false);

            //mockService.Verify(x => x.ProcessarRecepcaoEmailComAnexos(message), Times.Never());
            //mockService.Verify(x => x.ProcessarRecepcaoEmailSemAnexos(message), Times.Once());

            Assert.AreEqual(1, context.PublishedMessages.Length);
            Assert.AreEqual(1, context.SentMessages.Length);

            Assert.IsInstanceOfType(context.SentMessages[0].Message, typeof(SolicitacaoEnvioEmailCommand));
            Assert.IsInstanceOfType(context.PublishedMessages[0].Message, typeof(RecepcaoEmailProcessadoEvent));
        }

        [TestMethod]
        public void Handler_Nao_Deve_Processar_Recepcao_Email_Com_mensagem_invalida()
        {
            message.Identificadores.Solicitante = Guid.Empty;

            Func<Task> excecaoEsperada = async () => await handler.Handle(message, context).ConfigureAwait(false);

            //excecaoEsperada.Should().ThrowExactly<MessageInvalidException>();           
        }
    }
}
