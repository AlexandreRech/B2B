using AutoMapper;
using Microservicos.B2B.Domain.EmailModule;
using Microservicos.B2B.Domain.MensagensModule;
using Microservicos.B2B.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecepcaoEmailMessage, Email>();
            CreateMap<MensagemComFalhaSintatica, RecepcaoEmailComFalhaSintaticaEvent>();                
        }
    }
}
