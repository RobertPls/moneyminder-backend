using Infrastructure.EntityFramework.ReadModel.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Infrastructure.EntityFramework.Config.ReadConfig.Transactions
{
    internal class TransactionReadConfig : IEntityTypeConfiguration<TransactionReadModel>
    {
        public void Configure(EntityTypeBuilder<TransactionReadModel> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Date).HasColumnName("date");
            builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(38, 2);
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.Type).HasColumnName("type");

            builder.Property(x => x.AccountId).HasColumnName("accountId");
            builder.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.CategoryId).HasColumnName("categoryId");
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);

            builder.Property(x => x.RelatedTransactionId).HasColumnName("relatedTransactionId").IsRequired(false);
            builder.HasOne(x => x.RelatedTransaction).WithMany().HasForeignKey(x => x.RelatedTransactionId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);

        }
    }
}
