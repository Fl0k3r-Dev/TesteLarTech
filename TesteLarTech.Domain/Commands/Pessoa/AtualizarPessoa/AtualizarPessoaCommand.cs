using MediatR;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Domain.Commands.Pessoa.AtualizarPessoa
{
    public class AtualizarPessoaCommand : IRequest<Response>
    {

        public AtualizarPessoaCommand(Guid id, string? nome, string? cpf, DateTime nascimento, string? email, string? password, string? nivel, IEnumerable<TelefoneViewModel> contatos)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Nivel = nivel;
            Email = email;
            Password = password;
            Ativo = true;
            Contatos = contatos;
        }

        public Guid Id { get; }
        public string? Nome { get; }
        public string? CPF { get; }
        public DateTime Nascimento { get; }
        public bool Ativo { get; }
        public string? Email { get; }
        public string? Password { get; }
        public string? Nivel { get; }
        public IEnumerable<TelefoneViewModel> Contatos { get; }
    }
}
