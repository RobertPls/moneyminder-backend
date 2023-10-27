using Domain.Events.Transactions;
using Domain.Models.Accounts;
using Domain.ValueObjects;
using SharedKernel.Core;
using System.Transactions;

namespace Domain.Models.Transactions
{
    public class Transaction : AggregateRoot<Guid>
    {
        public Guid? RelatedTransactionId { get; private set; }
        public Guid? CategoryId { get; private set; }
        public Guid AccountId { get; private set; }
        public DateValue Date { get; private set; }
        public MoneyValue Amount { get; private set; }
        public DescriptionValue Description { get; private set; }
        public TransactionType Type { get; private set; }

        public Transaction(Guid accountId, Guid? categoryId, DateTime date, decimal amount, string description, TransactionType type)
        {

            if (accountId == Guid.Empty)
            { 
                throw new BussinessRuleValidationException("The account cannot be empty");
            }

            Id = Guid.NewGuid();
            AccountId = accountId;
            CategoryId = categoryId;
            Date = date;
            Amount = amount;
            Description = description;
            Type = type;
        }

        public Transaction() { }

        public void Update(string description, decimal amount, DateTime date, Guid accountId, Guid? categoryId, Guid? relatedTransactionId, TransactionType type, bool isTransference)
        {
            if (accountId == Guid.Empty)
            {
                throw new BussinessRuleValidationException("The account cannot be empty");
            }

            var oldType = Type;
            var oldAccount = AccountId;
            var oldAmount = Amount;

            CategoryId = categoryId;
            RelatedTransactionId = relatedTransactionId;
            AccountId = accountId;
            Description = new DescriptionValue(description);
            Date = new DateValue(date);
            Amount = new MoneyValue(amount);
            Type = type;

            AddDomainEvent(new UpdatedTransaction(oldAccount, accountId, oldAmount, amount, oldType, type ,isTransference));

        }

        public void DeleteTransaction(bool isTransfer)
        {
            RemoveTransferenceRelation(); 

            if(Type == TransactionType.Income)
            {
                Type = TransactionType.Outcome;
            }
            else
            {
                Type = TransactionType.Income;
            }
            AddDomainEvent(new DeletedTransaction(AccountId, Amount, Type, isTransfer));
        }

        public void RemoveTransferenceRelation()
        {
            RelatedTransactionId = null;
        }

        public void AddTransferenceRelation(Guid id)
        {
            RelatedTransactionId = id;
        }
    }
}
  