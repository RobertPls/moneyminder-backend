using Domain.Models.Transactions;

namespace Domain.Factories.Transactions
{
    public interface ITransactionFactory
    {
        Transaction CreateTransactionIncome(Guid accountId, Guid categoryId, DateOnly date, decimal amount, string description);
        Transaction CreateTransactionOutcome(Guid accountId, Guid categoryId, DateOnly date, decimal amount, string description);

    }
}
