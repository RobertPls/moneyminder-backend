using Domain.Events.UserProfiles;
using Domain.Models.UserProfiles;

namespace Domain.Factories.UserProfiles
{
    public class UserProfileFactory : IUserProfileFactory
    {
        public UserProfile Create(Guid userId, string fullName)
        {
            var obj = new UserProfile(userId, fullName); ;
            return obj;
        }
    }
}
