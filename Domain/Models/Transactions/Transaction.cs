using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.Transactions
{
    public class Transaction : AggregateRoot<Guid>
    {
        public GuidValue TransactionId{ get; private set; }
        public GuidValue AccountId { get; private set; }
        public GuidValue CategoryId { get; private set; }
        public DateValue Date { get; private set; }
        public MoneyValue Amount { get; private set; }
        public DescriptionValue Description { get; private set; }
        public TransactionType Type { get; private set; }

        public Transaction(Guid accountId, Guid categoryId, DateOnly date, decimal amount, string description, TransactionType type)
        {
            AccountId = accountId;
            CategoryId = categoryId;
            Date = date;
            Amount = amount;
            Description = description;
            Type = type;
        }
    }
}
  