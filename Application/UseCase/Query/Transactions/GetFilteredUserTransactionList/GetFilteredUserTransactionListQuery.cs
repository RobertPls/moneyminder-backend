using Application.Dto.Transactions;
using Application.Utils;
using MediatR;

namespace Application.UseCase.Query.Transactions.GetFilteredUserTransactionList
{
    public class GetFilteredUserTransactionListQuery : IRequest<Result<IEnumerable<TransactionDto>>>
    {
        public Guid UserId { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? SinceDate { get; set; }
        public string? UntilDate { get; set; }

        public GetFilteredUserTransactionListQuery(Guid userId, Guid? accountId, Guid? categoryId, string? sinceDate, string? untilDate)
        {
            UserId = userId;
            AccountId = accountId;
            CategoryId = categoryId;
            SinceDate = sinceDate;
            UntilDate = untilDate;
        }
    }
}
