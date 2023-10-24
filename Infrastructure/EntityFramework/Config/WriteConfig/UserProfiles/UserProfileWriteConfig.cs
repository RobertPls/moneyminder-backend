using Domain.Models.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Config.WriteConfig.UserProfiles
{
    internal class UserProfileWriteConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {


            builder.ToTable("UserProfile");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).HasColumnName("fullName");
            builder.Property(x => x.Balance).HasColumnName("balance").HasPrecision(38, 2);

            builder.Property(x => x.UserId).HasColumnName("userId");

            builder.Ignore("_domainEvents");
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
