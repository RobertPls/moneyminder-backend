using Application.Utils;
using MediatR;

namespace Application.UseCase.Command.Security.Login
{
    public record LoginCommand : IRequest<Result<string>>
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginCommand(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
