using Domain.Models.Categories;

namespace Domain.Factories.Categories
{
    public interface ICategoryFactory
    {
        Category Create(Guid userId, string name);
    }
}
