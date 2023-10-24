using Domain.Events.Transactions;
using Domain.Models.Transactions;
using Domain.Repositories.Transactions;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repository.Transactions
{
    internal class TransactionRepository : ITransactionRepository
    {
        private readonly WriteDbContext _context;
        public TransactionRepository(WriteDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Transaction obj)
        {
            await _context.AddAsync(obj);
        }

        public async Task<Transaction?> FindByIdAsync(Guid id)
        {
            return await _context.Transaction.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task RemoveAsync(Transaction obj)
        {
            _context.Transaction.Remove(obj);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Transaction obj)
        {
            _context.Transaction.Update(obj);
            return Task.CompletedTask;
        }
    }
}
