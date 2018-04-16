using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using Microservicos.B2B.Domain.EmailModule;
using FluentValidation;

namespace Microservicos.B2B.Tests
{
    /**
    [TestClass]
    public class RecepcaoEmailServiceUnitTest
    {
        private Mock<IRecepcaoEmailRepository> repositorioMock;
        private Mock<IValidator<Email>> validatorMock;
        private Mock<IMapper> mapperMock;
        private RecepcaoEmailService service;
        private RecepcaoEmailMessage msgEmailComAnexos;

        public RecepcaoEmailServiceUnitTest()
        {
            repositorioMock = new Mock<IRecepcaoEmailRepository>();
            mapperMock = new Mock<IMapper>();

            service = new RecepcaoEmailService(repositorioMock.Object, mapperMock.Object, validatorMock.Object);

            msgEmailComAnexos = new RecepcaoEmailMessage();
        }

        [TestMethod]
        public void Deve_receber_email_com_anexos_referenciados()
        {
            mapperMock.Setup(x => x.Map<Email>(msgEmailComAnexos)).Returns(new Email());
            
            
            msgEmailComAnexos.QuantidadeAnexos = 1;

            service.ProcessarRecepcaoEmailComAnexos(msgEmailComAnexos);

            repositorioMock.Verify(x => x.InserirCabecalhoEmail(It.IsAny<Email>()), Times.Once());
        }

        [TestMethod]
        public void Nao_deve_processar_receção_email_ja_recebido()
        {
            //arrange
            mapperMock.Setup(x => x.Map<Email>(msgEmailComAnexos)).Returns(new Email());

            msgEmailComAnexos.QuantidadeAnexos = 1;
            msgEmailComAnexos.IdentificadorEmail = Guid.NewGuid();

            repositorioMock.Setup(x => x.ExisteEmail(msgEmailComAnexos.IdentificadorEmail))
                .Returns(true);

            //action
            service.ProcessarRecepcaoEmailComAnexos(msgEmailComAnexos);

            //assert
            repositorioMock.Verify(x => x.RegistrarEmailComFalha(It.IsAny<EmailNaoConforme>()), Times.Once());
            repositorioMock.Verify(x => x.InserirCabecalhoEmail(It.IsAny<Email>()), Times.Never());

        }
      
    }

   */
}
