using Infrastructure.EntityFramework.ReadModel.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Config.ReadConfig.Categories
{
    internal class CategoryReadConfig : IEntityTypeConfiguration<CategoryReadModel>
    {
        public void Configure(EntityTypeBuilder<CategoryReadModel> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");

            builder.Property(x => x.UserProfileId).HasColumnName("userProfileId");
            builder.HasOne(x => x.UserProfile).WithMany().HasForeignKey(x => x.UserProfileId);
        }
    }
}
