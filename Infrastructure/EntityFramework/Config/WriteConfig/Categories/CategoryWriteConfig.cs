using Domain.Models.Categories;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.Config.WriteConfig.Categories
{
    internal class CategoryWriteConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            var categoryNameConverter = new ValueConverter<CategoryNameValue, string>(
                categoryNameValue => categoryNameValue.CategoryName,
                stringValue => new CategoryNameValue(stringValue)
            );

            builder.ToTable("Category");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("name").HasConversion(categoryNameConverter);
            builder.Property(x => x.IsDefault).HasColumnName("isDefault");

            builder.Property(x => x.UserProfileId).HasColumnName("userProfileId");

            builder.Ignore("_domainEvents");
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
