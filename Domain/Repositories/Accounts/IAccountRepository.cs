using Domain.Models.Accounts;
using SharedKernel.Core;

namespace Domain.Repositories.Accounts
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task UpdateAsync(Account obj);
        Task RemoveAsync(Account obj);
    }
}
