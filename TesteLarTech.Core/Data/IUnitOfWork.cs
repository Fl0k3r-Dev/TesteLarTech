using System.Data;

namespace TesteLarTech.Core.Data
{
    /// <summary>
    /// Interface base para implementação do pattern UnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Referência do objeto de conexão.
        /// </summary>
        public IDbConnection Connection { get; }

        /// <summary>
        /// Referência do objeto de transação.
        /// </summary>
        public IDbTransaction Transaction { get; }

        /// <summary>
        /// Inicia a transação.
        /// </summary>
        /// <returns>Hashcode do objeto de transação.</returns>
        int BeginTransaction();

        /// <summary>
        /// Cancela a transação.
        /// </summary>
        /// <param name="hashcode">Código hash da transação.</param>
        Task RollbackAsync(int hashcode);

        /// <summary>
        /// Confirma a transação.
        /// </summary>
        /// <param name="hashcode">Código hash da transação.</param>        
        Task CommitAsync(int hashcode);
    }
}
