using Domain.Factories.Accounts;
using Domain.Factories.Categories;
using Domain.Factories.Transactions;
using Domain.Factories.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Extensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IAccountFactory, AccountFactory>();
            services.AddScoped<ITransactionFactory, TransactionFactory>();
            services.AddScoped<ICategoryFactory, CategoryFactory>();

            return services;
        }
    }
}
