using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Accounts.CreateAccount
{
    public record CreateAccountCommand : IRequest<Result>
    {
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateAccountCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
