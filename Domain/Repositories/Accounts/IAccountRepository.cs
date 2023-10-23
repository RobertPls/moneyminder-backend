using Domain.Models.Accounts;
using SharedKernel.Core;
using System.Security.Cryptography;

namespace Domain.Repositories.Accounts
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<Account?> FindByNameAsync(Guid id, string name);
        Task UpdateAsync(Account obj);
        Task RemoveAsync(Account obj);
    }
}
