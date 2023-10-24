using Domain.Models.UserProfiles;
using Domain.Repositories.UserProfiles;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repository.UserProfiles
{
    internal class UserProfileRepository : IUserProfileRepository
    {
        private readonly WriteDbContext _context;
        public UserProfileRepository(WriteDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(UserProfile obj)
        {
            await _context.AddAsync(obj);
        }

        public async Task<UserProfile?> FindByIdAsync(Guid id)
        {
            return await _context.UserProfile.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserProfile?> FindByUserIdAsync(Guid id)
        {
            return await _context.UserProfile.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public Task RemoveAsync(UserProfile obj)
        {
            _context.UserProfile.Remove(obj);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(UserProfile obj)
        {
            _context.UserProfile.Update(obj);
            return Task.CompletedTask;
        }
    }
}
