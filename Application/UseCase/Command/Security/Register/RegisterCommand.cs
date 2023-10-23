using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Security.Register
{
    public record RegisterCommand : IRequest<Result>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public RegisterCommand()
        {
            UserName = "";
            FirstName = "";
            LastName = "";
            Password = "";
            Email = "";
        }

    }
}
