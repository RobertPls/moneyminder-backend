using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Security.Login
{
    public record LoginCommand : IRequest<Result<string>>
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginCommand(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
