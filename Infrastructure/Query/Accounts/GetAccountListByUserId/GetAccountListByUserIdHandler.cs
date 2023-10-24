using Application.Dto.Accounts;
using Application.UseCase.Query.Accounts.GetAccountListByUserId;
using Application.Utils;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.ReadModel.Accounts;
using Infrastructure.EntityFramework.ReadModel.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query.Accounts.GetAccountListByUserId
{
    internal class GetAccountListByUserIdHandler : IRequestHandler<GetAccountListByUserIdQuery, Result<IEnumerable<AccountDto>>>
    {
        private readonly DbSet<UserProfileReadModel> userProfile;

        private readonly DbSet<AccountReadModel> account;
        public GetAccountListByUserIdHandler(ReadDbContext dbContext)
        {
            account = dbContext.Account;
        }
        public async Task<Result<IEnumerable<AccountDto>>> Handle(GetAccountListByUserIdQuery request, CancellationToken cancellationToken)
        {
            var query = account.Where(x => x.UserProfile.UserId == request.UserId).AsNoTracking().AsQueryable();

            var list = await query.Select(x => new AccountDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Balance = x.Balance,
            }).ToListAsync();

            return new Result<IEnumerable<AccountDto>> (list, true, "User account list");
        }
    }
}
