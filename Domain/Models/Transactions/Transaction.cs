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
        public void RemoveTransacionRelation()
        {
            RelatedTransactionId = null;
        }
        public void RemoveCategoryRelation()
        {
            CategoryId = null;
        }
        public void AddTransacionRelation(Guid transactionId)
        {
            RelatedTransactionId = transactionId;
        }

        public void AddCategoryRelation(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public void UpdateDate(DateTime newDate)
        {
            Date = new DateValue(newDate);
        }
        public void UpdateAmout(decimal newAmount)
        {
            Amount = new MoneyValue(newAmount);
        }
        public void UpdateDescription(string newDescription)
        {
            Description = new DescriptionValue(newDescription); ;
        }
        public void UpdateType(TransactionType newType)
        {
            Type = newType;
        }

        public void UpdateAccount(Guid newAccountId)
        {
            if (newAccountId == Guid.Empty)
            {
                throw new BussinessRuleValidationException("The account cannot be empty");
            }
            AccountId = newAccountId;
        }
        public void UpdateCategory(Guid newCategoryId)
        {
            CategoryId = newCategoryId;
        }

        public void Updated(Guid oldAccountId,Guid newAccountId,decimal oldAmount,decimal newAmount,TransactionType oldType,TransactionType newType)
        {
            AddDomainEvent(new UpdatedTransaction(oldAccountId, newAccountId, oldAmount, newAmount, oldType, newType));
        }
        public void Deleted()
        {
            AddDomainEvent(new DeletedTransaction(AccountId, Amount, Type));
        }
    }
}
  