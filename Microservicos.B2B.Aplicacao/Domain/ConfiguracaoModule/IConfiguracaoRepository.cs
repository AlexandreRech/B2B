﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.Domain.ConfiguracaoModule
{
    public interface IConfiguracaoRepository
    {
        bool ExisteSolicitante(Guid identificadorAplicacaoOrigem);
    }
}
