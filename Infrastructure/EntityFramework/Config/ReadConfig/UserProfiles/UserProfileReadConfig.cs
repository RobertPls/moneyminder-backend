using Infrastructure.EntityFramework.ReadModel.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Config.ReadConfig.UserProfiles
{
    internal class UserProfileReadConfig : IEntityTypeConfiguration<UserProfileReadModel>
    {
        public void Configure(EntityTypeBuilder<UserProfileReadModel> builder)
        {
            builder.ToTable("UserProfile");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.FullName).HasColumnName("fullName");
            builder.Property(x => x.Balance).HasColumnName("balance").HasPrecision(38, 2);

            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
