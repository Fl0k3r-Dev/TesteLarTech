using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLarTech.Domain.Entities.Base;
using TesteLarTech.Domain.Enums;

namespace TesteLarTech.Domain.Entities
{
    public class Telefone: EntityBase
    {
        public Telefone()
        {
            
        }
            public Guid IdPessoa { get; set; }
            public TipoContatoEnum Tipo { get; set; }
            public string Numero { get; set; }
            public required Pessoa Pessoa { get; set; }
    }
}
