using Domain.Events.Transactions;
using Domain.Models.Transactions;

namespace Domain.Factories.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
        public Transaction CreateIncomeTransaction(Guid accountId, Guid? categoryId, DateTime date, decimal amount, string description, bool isTransference)
        {
            var obj = new Transaction(accountId,categoryId, date, amount, description, TransactionType.Income);
            var domainEvent = new CreatedTransaction(obj.AccountId, obj.Amount, obj.Type, isTransference);
            obj.AddDomainEvent(domainEvent);
            return obj;
        }

        public Transaction CreateOutcomeTransaction(Guid accountId, Guid? categoryId, DateTime date, decimal amount, string description, bool isTransference)
        {
            var obj = new Transaction(accountId, categoryId, date, amount, description, TransactionType.Outcome);
            var domainEvent = new CreatedTransaction(obj.AccountId, obj.Amount, obj.Type, isTransference);
            obj.AddDomainEvent(domainEvent);
            return obj;
        }
    }
}
