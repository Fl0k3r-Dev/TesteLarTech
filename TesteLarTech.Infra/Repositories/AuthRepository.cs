using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TesteLarTech.Core.Data;
using TesteLarTech.Core.Hash;
using TesteLarTech.Core.Notifications;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.Repositories;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Infra.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        //private readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;
        private readonly INotificationService _notificationService;
        public AuthRepository(AppDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Pessoa> Login(string username, string password)
        {
            var dataSet = await _context.Pessoas
                            .Where(u => u.Email == username)
                            .FirstOrDefaultAsync();

            if (dataSet == null)
            {
                _notificationService.Notify("Erro Login", "Usuário inexistente");
                return dataSet;
            }

            if (dataSet.Nivel == null)
            {
                _notificationService.Notify("Nível usuário", "Nível de acesso inválido!");
                return dataSet;
            }

            if (!dataSet.Ativo)
            {
                _notificationService.Notify("Erro Login", "Usuário inativo ou inexistente, não pode realizar o login!");
                return dataSet;
            }

            var checkPass = new Encrypt(SHA512.Create()).ValidateHash(password, dataSet.Password);

            if (!checkPass)
            {
                _notificationService.Notify("Erro de Login", "Usuário ou senha inválidos!");
            }
            dataSet.Password = string.Empty;
            return dataSet;
        }
    }
}
