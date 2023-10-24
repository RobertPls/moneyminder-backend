using Infrastructure.EntityFramework.ReadModel.UserProfiles;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EntityFramework.ReadModel.Categories
{
    internal class CategoryReadModel
    {

        [Key]
        public Guid Id { get; set; }
        public UserProfileReadModel UserProfile { get; set; }
        public Guid UserProfileId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
