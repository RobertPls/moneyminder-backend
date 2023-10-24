using Domain.Models.Accounts;
using Domain.Models.Categories;
using Domain.Repositories.Categories;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repository.Categories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly WriteDbContext _context;
        public CategoryRepository(WriteDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Category obj)
        {
            await _context.AddAsync(obj);
        }

        public async Task<Category?> FindByIdAsync(Guid id)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Category?> FindByNameAsync(Guid id, string name)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Name.ToString().ToLower() == name.ToLower() && x.UserProfileId == id);
        }
        public Task RemoveAsync(Category obj)
        {
            _context.Category.Remove(obj);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Category obj)
        {
            _context.Category.Update(obj);
            return Task.CompletedTask;
        }
    }
}
