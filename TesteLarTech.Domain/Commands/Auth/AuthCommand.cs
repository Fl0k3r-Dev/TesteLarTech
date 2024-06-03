using MediatR;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Domain.Commands
{
    public class AuthCommand : IRequest<PessoaViewModel>
    {
        public AuthCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
        public string Email { get; }
        public string Senha { get; }
    }
}
