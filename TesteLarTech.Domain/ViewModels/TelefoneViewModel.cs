using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.Enums;

namespace TesteLarTech.Domain.ViewModels
{
    public class TelefoneViewModel
    {
        public Guid IdPessoa { get; set; }
        public TipoContatoEnum Tipo { get; set; }
        public string Numero { get; set; }
    }

}
