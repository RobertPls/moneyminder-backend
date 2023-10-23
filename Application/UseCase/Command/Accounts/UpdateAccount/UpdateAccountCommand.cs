using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Accounts.UpdateAccount
{
    public record UpdateAccountCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateAccountCommand(Guid accountId, string name, string description)
        {
            AccountId = accountId;
            Name = name;
            Description = description;
        }
    }
}
