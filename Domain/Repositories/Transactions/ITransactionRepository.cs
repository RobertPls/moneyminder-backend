using Domain.Models.Transactions;
using SharedKernel.Core;

namespace Domain.Repositories.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task UpdateAsync(Transaction obj);
        Task RemoveAsync(Transaction obj);
    }
}
