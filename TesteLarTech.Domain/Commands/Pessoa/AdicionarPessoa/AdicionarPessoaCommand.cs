using MediatR;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Domain.Commands.Pessoa.AdicionarPessoa
{
    public class AdicionarPessoaCommand : IRequest<Response>
    {

        public AdicionarPessoaCommand(string nome, string cpf, DateTime nascimento, string email, string password, string nivel, IEnumerable<TelefoneViewModel> contatos)
        {
            Nome = nome;
            CPF = cpf;
            Nivel = nivel;
            Email = email;
            Password = password;
            Ativo = true;
            Contatos = contatos;
        }

        public string Nome { get; }
        public string CPF { get; }
        public DateTime Nascimento { get; }
        public bool Ativo { get; }
        public string Email { get; }
        public string Password { get; }
        public string Nivel { get; }
        public IEnumerable<TelefoneViewModel> Contatos { get; }
    }
}
