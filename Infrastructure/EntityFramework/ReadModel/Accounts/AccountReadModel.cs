using Infrastructure.EntityFramework.ReadModel.UserProfiles;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EntityFramework.ReadModel.Accounts
{
    internal class AccountReadModel
    {
        [Key]
        public Guid Id { get; set; }
        public UserProfileReadModel UserProfile { get; set; }
        public Guid UserProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }

    }
}
