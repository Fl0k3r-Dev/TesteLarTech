using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TesteLarTech.Core.Data;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Entities.Base;
using TesteLarTech.Domain.Interfaces.Repositories.Base;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Infra.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : EntityBase
    {

        private readonly AppDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public RepositoryBase(AppDbContext context, INotificationService notificationService, IMapper mapper)
        {
            _context = context;
            _context.Set<T>();
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Add(List<T> entities)
        {
            try
            {
                await _context.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.FindAsync<T>(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Pessoas.AnyAsync(p => p.Email == email);
        }

        public async Task<PaginacaoViewModel<T>> GetAllWithPagination(PaginacaoViewModel<T> request)
        {
            var query = _context.Set<T>().AsQueryable();

            var totalLinhas = await query.CountAsync();

            var results = await query
             .Skip((request.Pagina - 1) * request.TamanhoPaginas)
             .Take(request.TamanhoPaginas)
             .ToListAsync();

            var paginacaoViewModel = new PaginacaoViewModel<T>
            {
                Results = results,
                Pagina = request.Pagina,
                TamanhoPaginas = request.TamanhoPaginas,
                TermoBusca = request.TermoBusca,
                TotalLinhas = totalLinhas
            };

            return paginacaoViewModel;
        }

        public async Task<T> GetById(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
