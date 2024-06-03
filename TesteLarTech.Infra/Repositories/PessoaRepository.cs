using AutoMapper;
using TesteLarTech.Core.Data;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.Interfaces.Repositories;
using TesteLarTech.Infra.Repositories.Base;

namespace TesteLarTech.Infra.Repositories
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        private readonly AppDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public PessoaRepository(AppDbContext context, INotificationService notificationService, IMapper mapper) : base(context, notificationService, mapper)
        {
            _context = context;
            _mapper = mapper;
            _notificationService = notificationService;
        }


    }
}
