﻿using Infrastructure.EntityFramework.ReadModel.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Config.ReadConfig.Accounts
{
    internal class AccountReadConfig : IEntityTypeConfiguration<AccountReadModel>
    {
        public void Configure(EntityTypeBuilder<AccountReadModel> builder)
        {
            builder.ToTable("Account");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.Balance).HasColumnName("balance").HasPrecision(38, 2);

            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
