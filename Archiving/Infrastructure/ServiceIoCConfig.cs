using Microsoft.Extensions.DependencyInjection;
using Archiving.BLL.Interfaces;
using Archiving.BLL.Services;

namespace Archiving.Infrastructure
{
    public static class ServiceIoCConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IArchivingService, ArchivingService>();
        }
    }
}
