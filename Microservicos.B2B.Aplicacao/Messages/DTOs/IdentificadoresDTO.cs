using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservicos.B2B.Messages.DTOs
{
    public class IdentificadoresDTO
    {
        /// <summary>
        /// Campo obrigatório.
        /// Para cada solicitação, deverá ser um novo Identificador.
        /// Será utilizado para a aplicação solicitante obter informações da ação solicitada
        /// </summary>
        public Guid Processo { get; set; }

        /// <summary>
        /// Campo obrigatório.        
        /// Identificador do solicitante 
        /// </summary>
        public Guid Solicitante { get; set; }

        /// <summary>
        /// Campo obrigatório.
        /// Para cada email, deverá ser um novo Identificador.
        /// Caso haja necessidade de reprocessamento, este identificador deve ser mantido
        /// Identificador do Email
        /// </summary>
        public Guid Documento { get; set; }
    }
}
