using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Accounts.DeleteAccount
{
    public record DeleteAccountCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public Guid AccountId { get; set; }

        public DeleteAccountCommand(Guid accountId)
        {

            AccountId = accountId;
        }
    }
}
