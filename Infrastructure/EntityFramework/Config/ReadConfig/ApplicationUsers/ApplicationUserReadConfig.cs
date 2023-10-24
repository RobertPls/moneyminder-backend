using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Config.ReadConfig.Users
{
    internal class ApplicationUserReadConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.FirstName).HasColumnName("FirstName");
            builder.Property(x => x.LastName).HasColumnName("LastName");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Active).HasColumnName("Active");
            builder.Property(x => x.Staff).HasColumnName("Staff");
            builder.Property(x => x.Balance).HasColumnName("Balance").HasPrecision(38, 2);

            //----------------------------------------------------------------------
            builder.Property(x => x.EmailConfirmed).HasColumnName("EmailConfirmed");
            builder.Property(x => x.NormalizedEmail).HasColumnName("NormalizedEmail");
            builder.Property(x => x.UserName).HasColumnName("UserName");
            builder.Property(x => x.NormalizedUserName).HasColumnName("NormalizedUserName");
            builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(x => x.SecurityStamp).HasColumnName("SecurityStamp");
            builder.Property(x => x.ConcurrencyStamp).HasColumnName("ConcurrencyStamp");
            builder.Property(x => x.PhoneNumber).HasColumnName("PhoneNumber");
            builder.Property(x => x.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            builder.Property(x => x.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
            builder.Property(x => x.LockoutEnabled).HasColumnName("LockoutEnabled");
            builder.Property(x => x.LockoutEnd).HasColumnName("LockoutEnd");
            builder.Property(x => x.AccessFailedCount).HasColumnName("AccessFailedCount");

        }
    }
}
