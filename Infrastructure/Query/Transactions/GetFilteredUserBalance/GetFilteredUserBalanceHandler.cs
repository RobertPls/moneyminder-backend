using Application.Dto.Users;
using Application.UseCase.Query.Transactions.GetFilteredUserBalance;
using Application.Utils;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.ReadModel.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query.Transactions.GetFilteredUserBalance
{

    internal class GetFilteredUserBalanceHandler : IRequestHandler<GetFilteredUserBalanceQuery, Result<BalanceDto>>
    {
        private readonly DbSet<TransactionReadModel> transaction;
        public GetFilteredUserBalanceHandler(ReadDbContext dbContext)
        {
            transaction = dbContext.Transaction;
        }
        public async Task<Result<BalanceDto>> Handle(GetFilteredUserBalanceQuery request, CancellationToken cancellationToken)
        {
            var query = transaction.Where(x => x.Account.UserProfile.UserId == request.UserId).AsNoTracking().AsQueryable();

            query = DateFilter(query, request.SinceDate, request.UntilDate);

            string income = Domain.Models.Transactions.TransactionType.Income.ToString();
            string outcome = Domain.Models.Transactions.TransactionType.Outcome.ToString();

            decimal totalIncome = query
                .Where(x => x.Type.Equals(income))
                .Sum(x => x.Amount);

            decimal totalOutcome = query
                .Where(x => x.Type.Equals(outcome))
                .Sum(x => x.Amount);

            decimal balance = totalIncome - totalOutcome;

            System.Diagnostics.Debug.WriteLine(query.Count());

            var balanceDto = new BalanceDto
            {
                Income = totalIncome,
                Outcome = totalOutcome,
                Balance = balance
            };

            return new Result<BalanceDto>(balanceDto, true, "User balance");
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
