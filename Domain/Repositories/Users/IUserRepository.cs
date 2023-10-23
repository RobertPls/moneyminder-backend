using Domain.Models.Users;
using SharedKernel.Core;

namespace Domain.Repositories.Users
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task UpdateAsync(User obj);
        Task RemoveAsync(User obj);
    }
}
