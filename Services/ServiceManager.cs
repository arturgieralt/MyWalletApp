using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.RealTime;
using MyWalletApp.Services.Providers;

namespace MyWalletApp.Services
{
    public static class ServiceManager
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            services.AddMediatR(typeof(ServiceManager).Assembly);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IEventEmitter, EventEmitter>();
            
            services.AddScoped<IUserContextProvider, UserContextProvider>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IAccountUserInviteRepository, AccountUserInviteRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        }
    }
}