using Domain.Models.Accounts;
using Domain.Models.Categories;
using Domain.Models.Transactions;
using Domain.Models.UserProfiles;
using Infrastructure.EntityFramework.Config.WriteConfig.Accounts;
using Infrastructure.EntityFramework.Config.WriteConfig.Categories;
using Infrastructure.EntityFramework.Config.WriteConfig.Transactions;
using Infrastructure.EntityFramework.Config.WriteConfig.UserProfiles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context
{
    internal class WriteDbContext : DbContext
    {
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Account> Account{ get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserProfileWriteConfig());
            modelBuilder.ApplyConfiguration(new AccountWriteConfig());
            modelBuilder.ApplyConfiguration(new CategoryWriteConfig());
            modelBuilder.ApplyConfiguration(new TransactionWriteConfig());

        }
    }
}
