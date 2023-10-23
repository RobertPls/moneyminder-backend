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
            return await _context.Account.FirstOrDefaultAsync(x => x.Name.ToString().ToLower() == name.ToLower() && x.UserId==id);
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
