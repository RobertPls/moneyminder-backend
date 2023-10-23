using Application.Dto.Users;
using Application.Utils;
using MediatR;

namespace Application.UseCase.Query.Transactions.GetFilteredUserBalance
{

    public class GetFilteredUserBalanceQuery : IRequest<Result<BalanceDto>>
    {
        public Guid UserId { get; set; }
        public string? SinceDate { get; set; }
        public string? UntilDate { get; set; }

        public GetFilteredUserBalanceQuery(Guid userId, string? sinceDate, string? untilDate)
        {
            UserId = userId;
            SinceDate = sinceDate;
            UntilDate = untilDate;
        }
    }
}
