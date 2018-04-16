using AutoMapper;
using Microservicos.B2B.AutoMapperProfiles;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.Aplicacao
{
    public class Startup : INeedInitialization
    {
        public void Customize(EndpointConfiguration configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            configuration.RegisterComponents(
                 registration: configureComponents =>
                 {
                     configureComponents.ConfigureComponent(
                         componentFactory: () =>
                         {
                             var rep = new RecepcaoEmailRepository();

                             return rep;
                         },
                         dependencyLifecycle: DependencyLifecycle.InstancePerCall);
                 }
                );
        }
    }
}
