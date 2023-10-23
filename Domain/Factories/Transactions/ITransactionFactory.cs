using Domain.Models.Transactions;

namespace Domain.Factories.Transactions
{
    public interface ITransactionFactory
    {
        Transaction CreateIncomeTransaction(Guid accountId, Guid? categoryId, DateTime date, decimal amount, string description);
        Transaction CreateOutcomeTransaction(Guid accountId, Guid? categoryId, DateTime date, decimal amount, string description);

    }
}
