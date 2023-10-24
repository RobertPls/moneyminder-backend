using Application.Dto.Transactions;
using Application.UseCase.Query.Transactions.GetFilteredUserTransactionList;
using Application.Utils;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.ReadModel.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query.Transactions.GetFilteredUserTransactionList
{
    internal class GetFilteredUserTransactionListHandler : IRequestHandler<GetFilteredUserTransactionListQuery, Result<IEnumerable<TransactionDto>>>
    {
        private readonly DbSet<TransactionReadModel> transaction;
        public GetFilteredUserTransactionListHandler(ReadDbContext dbContext)
        {
            transaction = dbContext.Transaction;
        }
        public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetFilteredUserTransactionListQuery request, CancellationToken cancellationToken)
        {
            var query = transaction.Where(x => x.Account.UserProfile.UserId == request.UserId).AsNoTracking().AsQueryable();

            query = DateFilter(query, request.SinceDate, request.UntilDate);

            query = AccountFilter(query, request.AccountId);

            query = CategoryFilter(query, request.CategoryId);

            var list = await query.Select(x => new TransactionDto
            {
                Id = x.Id,
                RelatedTransactionId = x.RelatedTransactionId,
                AccountId = x.Account.Id,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Amount = x.Amount,
                Date = x.Date,
                Type = x.Type
            }).ToListAsync();

            return new Result<IEnumerable<TransactionDto>>(list, true, "User transaction list");
        }

        private IQueryable<TransactionReadModel> AccountFilter(IQueryable<TransactionReadModel> query, Guid? accountId)
        {
            if (accountId != Guid.Empty && !string.IsNullOrEmpty(accountId.ToString()))
            {
                query = query.Where(x => x.AccountId == accountId);
            }

            return query;
        }

        private IQueryable<TransactionReadModel> CategoryFilter(IQueryable<TransactionReadModel> query, Guid? categoryId)
        {
            if (categoryId != Guid.Empty && !string.IsNullOrEmpty(categoryId.ToString()))
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            return query;
        }

        private IQueryable<TransactionReadModel> DateFilter(IQueryable<TransactionReadModel> query, string? sinceDate, string? untilDate)
        {
            if (DateTime.TryParse(sinceDate, out var sinceDateValue))
            {
                query = query.Where(x => x.Date.Date >= sinceDateValue.Date);
            }

            if (DateTime.TryParse(untilDate, out var untilDateValue))
            {
                query = query.Where(x => x.Date.Date <= untilDateValue.Date);
            }

            return query;
        }
    }
}
