using Domain.Models.UserProfiles;

namespace Domain.Factories.UserProfiles
{
    public class UserProfileFactory : IUserProfileFactory
    {
        public UserProfile Create(Guid userId, string fullName)
        {
            return new UserProfile(userId, fullName); ;
        }
    }
}
