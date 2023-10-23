using Domain.Models.Accounts;
using Domain.Models.Categories;
using SharedKernel.Core;

namespace Domain.Repositories.Categories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<Category?> FindByNameAsync(Guid id, string name);
        Task UpdateAsync(Category obj);
        Task RemoveAsync(Category obj);
    }
}
