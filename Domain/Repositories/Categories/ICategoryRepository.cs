using Domain.Models.Categories;
using SharedKernel.Core;

namespace Domain.Repositories.Categories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task UpdateAsync(Category obj);
        Task RemoveAsync(Category obj);
    }
}
