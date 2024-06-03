using Microsoft.Extensions.DependencyInjection;
using TesteLarTech.Core.Notifications;

namespace TesteLarTech.Core.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
    