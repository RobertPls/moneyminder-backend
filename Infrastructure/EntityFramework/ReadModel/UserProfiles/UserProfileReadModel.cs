using Infrastructure.Security;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EntityFramework.ReadModel.UserProfiles
{
    internal class UserProfileReadModel
    {
        [Key]
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public decimal Balance { get; set; }

    }
}
