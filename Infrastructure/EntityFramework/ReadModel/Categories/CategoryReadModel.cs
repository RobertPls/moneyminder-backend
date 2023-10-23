using Infrastructure.EntityFramework.ReadModel.Users;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EntityFramework.ReadModel.Categories
{
    internal class CategoryReadModel
    {

        [Key]
        public Guid Id { get; set; }
        public UserReadModel User { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
