using Domain.Models.Transactions;

namespace Domain.Factories.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
        public Transaction CreateTransactionIncome(Guid accountId, Guid categoryId, DateOnly date, decimal amount, string description)
        {
            return new Transaction(accountId, categoryId, date, amount, description, TransactionType.Income);
        }

        public Transaction CreateTransactionOutcome(Guid accountId, Guid categoryId, DateOnly date, decimal amount, string description)
        {
            return new Transaction(accountId, categoryId, date, amount, description, TransactionType.Outcome);
        }
    }
}
