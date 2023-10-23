using Domain.Events.Users;
using Domain.Models.Users;

namespace Domain.Factories.Users
{
    public class UserFactory : IUserFactory
    {
        public User Create(string userName, string lastName, string firstName, string email, bool active, bool staff)
        {
            var obj = new User(userName, lastName, firstName, email, active, staff);
            var domainEvent = new CreatedUser(obj.Id);
            obj.AddDomainEvent(domainEvent);
            return obj;
        }
    }
}
