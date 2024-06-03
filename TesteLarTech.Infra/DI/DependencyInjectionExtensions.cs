using TesteLarTech.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using TesteLarTech.Core.Data;
using TesteLarTech.Domain.Interfaces.Repositories;
using TesteLarTech.Domain.Repositories;
using TesteLarTech.Infra.Repositories;
using TesteLarTech.Domain.Service.Interfaces;
using TesteLarTech.Domain.Service;
using TesteLarTech.Core.Interfaces;

namespace TesteLarTech.Infra.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddDbContext<AppDbContext>();

            return services;
        }
    }
}
