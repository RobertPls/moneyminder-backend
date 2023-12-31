﻿using Infrastructure.EntityFramework.Config.ReadConfig.Accounts;
using Infrastructure.EntityFramework.Config.ReadConfig.Categories;
using Infrastructure.EntityFramework.Config.ReadConfig.Transactions;
using Infrastructure.EntityFramework.Config.ReadConfig.UserProfiles;
using Infrastructure.EntityFramework.Config.ReadConfig.Users;
using Infrastructure.EntityFramework.ReadModel.Accounts;
using Infrastructure.EntityFramework.ReadModel.Categories;
using Infrastructure.EntityFramework.ReadModel.Transactions;
using Infrastructure.EntityFramework.ReadModel.UserProfiles;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context
{
    internal class ReadDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<UserProfileReadModel> UserProfile { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<AccountReadModel> Account { get; set; }
        public virtual DbSet<CategoryReadModel> Category { get; set; }
        public virtual DbSet<TransactionReadModel> Transaction { get; set; }

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserReadConfig());
            modelBuilder.ApplyConfiguration(new UserProfileReadConfig());
            modelBuilder.ApplyConfiguration(new AccountReadConfig());
            modelBuilder.ApplyConfiguration(new CategoryReadConfig());
            modelBuilder.ApplyConfiguration(new TransactionReadConfig());


            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });
        }
    }
}
