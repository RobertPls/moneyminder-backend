using Domain.Models.Accounts;
using Domain.Repositories.Accounts;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repository.Accounts
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly WriteDbContext _context;
        public AccountRepository(WriteDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Account obj)
        {
            await _context.AddAsync(obj);
        }

        public async Task<Account?> FindByIdAsync(Guid id)
        {
            return await _context.Account.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account?> FindByNameAsync(Guid id, string name)
        {
            var accounts = await _context.Account
                .Where(x => x.UserProfileId == id)
                .ToListAsync();

            return accounts.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public Task RemoveAsync(Account obj)
        {
            _context.Account.Remove(obj);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Account obj)
        {
            _context.Account.Update(obj);
            return Task.CompletedTask;
        }
    }
}
