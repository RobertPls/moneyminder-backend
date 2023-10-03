using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.EntityFramework
{
    internal class DbInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService(typeof(ReadDbContext)) as ReadDbContext;
            if (dbContext is null)
            {
                return;
            }

            await dbContext.Database.MigrateAsync(cancellationToken);

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
