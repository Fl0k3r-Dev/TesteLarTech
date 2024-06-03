using Microsoft.AspNetCore.Http;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Domain.Service.Interfaces
{
    //TODO: Falta registrar no injetor de dependência
    public interface ILoginService
    {
        public Task Login(PessoaViewModel usuario);
        public Task Logoff();
    }
}
