using Domain.Models.Accounts;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.Config.WriteConfig.Accounts
{
    internal class AccountWriteConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {

            var descriptionConverter = new ValueConverter<DescriptionValue, string>(
                descriptionValue => descriptionValue.Description,
                stringValue => new DescriptionValue(stringValue)
            );

            var accountNameConverter = new ValueConverter<AccountNameValue, string>(
                accountNameValue => accountNameValue.AccountName,
                stringValue => new AccountNameValue(stringValue)
            );

            builder.ToTable("Account");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("name").HasConversion(accountNameConverter);
            builder.Property(x => x.Description).HasColumnName("description").HasConversion(descriptionConverter);
            builder.Property(x => x.Balance).HasColumnName("balance").HasPrecision(38, 2);

            builder.Property(x => x.UserProfileId).HasColumnName("userProfileId");

            builder.Ignore("_domainEvents");
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
