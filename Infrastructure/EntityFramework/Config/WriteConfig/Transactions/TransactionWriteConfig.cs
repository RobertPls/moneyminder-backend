using Domain.Models.Transactions;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.Config.WriteConfig.Transactions
{

    internal class TransactionWriteConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            var descriptionConverter = new ValueConverter<DescriptionValue, string>(
                descriptionValue => descriptionValue.Description,
                stringValue => new DescriptionValue(stringValue)
            );
            var dateConverter = new ValueConverter<DateValue, DateTime>(
                dateValue => dateValue.Date,
                dateOnlyuValue => new DateValue(dateOnlyuValue)
            );
            var moneyConverter = new ValueConverter<MoneyValue, decimal>(
                moneyValue => moneyValue.Value,
                decimalValue => new MoneyValue(decimalValue)
            );

            var typeConverter = new ValueConverter<TransactionType, string>(
                stateEnumValue => stateEnumValue.ToString(),
                state => (TransactionType)Enum.Parse(typeof(TransactionType), state)
            );

            builder.ToTable("Transaction");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasColumnName("description").HasConversion(descriptionConverter);
            builder.Property(x => x.Date).HasColumnName("date").HasConversion(dateConverter);
            builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(38, 2).HasConversion(moneyConverter);
            builder.Property(x => x.Type).HasColumnName("type").HasConversion(typeConverter); ;

            builder.Property(x => x.RelatedTransactionId).HasColumnName("relatedTransactionId").IsRequired(false);
            builder.Property(x => x.CategoryId).HasColumnName("categoryId").IsRequired(false);
            builder.Property(x => x.AccountId).HasColumnName("accountId");

            builder.Ignore("_domainEvents");
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
