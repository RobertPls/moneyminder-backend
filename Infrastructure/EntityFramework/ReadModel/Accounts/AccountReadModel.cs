using Infrastructure.EntityFramework.ReadModel.Users;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EntityFramework.ReadModel.Accounts
{
    internal class AccountReadModel
    {
        [Key]
        public Guid Id { get; set; }
        public UserReadModel User { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }

    }
}
