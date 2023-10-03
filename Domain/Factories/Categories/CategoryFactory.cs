using Domain.Models.Categories;

namespace Domain.Factories.Categories
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category Create(Guid userId, string name)
        {
            return new Category(userId, name);
        }
    }
}
