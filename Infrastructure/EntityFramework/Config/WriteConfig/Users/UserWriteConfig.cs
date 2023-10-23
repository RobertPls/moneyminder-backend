using Domain.Models.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.Config.WriteConfig.Users
{
    internal class UserWriteConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           
            var firstNameConverter = new ValueConverter<FirstNameValue, string>(
                firstNameValue => firstNameValue.FirstName,
                stringValue => new FirstNameValue(stringValue)
            );
            var lastNameConverter = new ValueConverter<LastNameValue, string>(
                lastNameValue => lastNameValue.LastName,
                stringValue => new LastNameValue(stringValue)
            );
            var userNameConverter = new ValueConverter<UserNameValue, string>(
                userNameValue => userNameValue.UserName,
                stringValue => new UserNameValue(stringValue)
            );
            var emailConverter = new ValueConverter<EmailValue, string>(
                emailValue => emailValue.EmailAddress,
                stringValue => new EmailValue(stringValue)
            );

            builder.ToTable("AspNetUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.FirstName).HasColumnName("FirstName").HasConversion(firstNameConverter);
            builder.Property(x => x.LastName).HasColumnName("LastName").HasConversion(lastNameConverter);
            builder.Property(x => x.Email).HasColumnName("Email").HasConversion(emailConverter);
            builder.Property(x => x.UserName).HasColumnName("UserName").HasConversion(userNameConverter);
            builder.Property(x => x.Active).HasColumnName("Active");
            builder.Property(x => x.Staff).HasColumnName("Staff");
            builder.Property(x => x.Balance).HasColumnName("Balance").HasPrecision(38, 2);

            builder.Ignore("_domainEvents");
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
