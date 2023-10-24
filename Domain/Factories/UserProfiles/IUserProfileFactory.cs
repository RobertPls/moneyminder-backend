using Domain.Models.UserProfiles;

namespace Domain.Factories.UserProfiles
{
    public interface IUserProfileFactory
    {
        UserProfile Create(Guid userId, string fullName);
    }
}
