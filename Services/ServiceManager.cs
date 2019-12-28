using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyWalletApp.DomainModel.Repositories;

namespace MyWalletApp.Services
{
    public static class ServiceManager
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            services.AddMediatR(typeof(ServiceManager).Assembly);

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

        }
    }
}