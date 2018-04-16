using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.Domain.EmailModule
{
    public class IdentificadorEmail
    {
        public Guid Processo { get; set; }
        
        public Guid Solicitante { get; set; }

        public Guid Documento { get; set; }
    }
}