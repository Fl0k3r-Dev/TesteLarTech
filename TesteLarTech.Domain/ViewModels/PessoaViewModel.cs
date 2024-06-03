using TesteLarTech.Domain.Entities;

namespace TesteLarTech.Domain.ViewModels
{
    public class PessoaViewModel
    {  
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Nivel { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<TelefoneViewModel> Contatos { get; set; }

    }

}
