using Domain.Models.Accounts;

namespace Domain.Factories.Accounts
{
    public interface IAccountFactory
    {
        Account Create(Guid userId, string name, string descripcion);
    }
}
