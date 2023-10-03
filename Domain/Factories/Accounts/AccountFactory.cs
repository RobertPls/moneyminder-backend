using Domain.Models.Accounts;

namespace Domain.Factories.Accounts
{
    public class AccountFactory : IAccountFactory
    {
        public Account Create(Guid userId, string name, string descripcion)
        {
            return new Account(userId, name, descripcion);
        }
    }
}
