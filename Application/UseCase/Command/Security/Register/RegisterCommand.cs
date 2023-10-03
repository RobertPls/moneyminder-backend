using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Security.Register
{
    public record RegisterCommand : IRequest<Result>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public RegisterCommand()
        {
            Username = "";
            FirstName = "";
            LastName = "";
            Password = "";
            Email = "";
            Roles = new List<string>();

        }

    }
}
