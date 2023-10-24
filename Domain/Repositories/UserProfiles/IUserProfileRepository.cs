using Domain.Models.UserProfiles;
using SharedKernel.Core;

namespace Domain.Repositories.UserProfiles
{
    public interface IUserProfileRepository : IRepository<UserProfile, Guid>
    {
        Task<UserProfile?> FindByUserIdAsync(Guid id);
        Task UpdateAsync(UserProfile obj);
        Task RemoveAsync(UserProfile obj);
    }
}
