using Application.Dto.Accounts;
using Application.Utils;
using MediatR;

namespace Application.UseCase.Query.Accounts.GetAccountListByUserId
{
    public class GetAccountListByUserIdQuery : IRequest<Result<IEnumerable<AccountDto>>>
    {
        public Guid UserId { get; set; }

        public GetAccountListByUserIdQuery(Guid userId) 
        {
            UserId = userId;
        }
    }
}
