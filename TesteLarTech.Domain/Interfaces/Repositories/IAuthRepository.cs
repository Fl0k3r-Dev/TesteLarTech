using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<Pessoa> Login(string username, string password);
    }

}
