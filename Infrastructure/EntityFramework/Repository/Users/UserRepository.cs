using Domain.Factories.Users;
using Domain.Models.Users;
using Domain.Repositories.Users;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repository.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly WriteDbContext _context;
        private readonly IUserFactory _userFactory;
        public UserRepository(WriteDbContext context, IUserFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }
        public async Task CreateAsync(User obj)
        {
            await _context.AddAsync(obj);
        }

        public async Task<User?> FindByIdAsync(Guid id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task RemoveAsync(User obj)
        {
            //var userWriteModel = new UserWriteModel(obj.Id, obj.UserName, obj.LastName, obj.FirstName, obj.Email, obj.Active, obj.Staff, obj.Balance);
            //_context.User.Remove(userWriteModel);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(User obj)
        {
            //var userWriteModel = new UserWriteModel(obj.Id, obj.UserName, obj.LastName, obj.FirstName, obj.Email, obj.Active, obj.Staff, obj.Balance);
            //_context.User.Update(userWriteModel);
            return Task.CompletedTask;
        }
    }
}
