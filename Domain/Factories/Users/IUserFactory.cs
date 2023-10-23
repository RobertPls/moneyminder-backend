using Domain.Models.Users;

namespace Domain.Factories.Users
{
    public interface IUserFactory
    {
        User Create(string userName, string lastName, string firstName, string email, bool active, bool staff);
    }
}
