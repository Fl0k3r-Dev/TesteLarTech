using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T>
        where T : class
    {
        Task<PaginacaoViewModel<T>> GetAllWithPagination(PaginacaoViewModel<T> request);
        Task<T> GetById(Guid id);
        Task<bool> EmailExists(string email);
        Task<bool> Add(T entity);
        Task<bool> Add(List<T> entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id); // Delete lógico para manter a rastreabilidade
    }
}
