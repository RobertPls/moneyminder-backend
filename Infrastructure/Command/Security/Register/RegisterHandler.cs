using Application.UseCase.Command.Security.Register;
using Application.Utils;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Command.Security.Register
{
    internal class RegisterHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterHandler> _logger;

        public RegisterHandler(UserManager<ApplicationUser> userManager, ILogger<RegisterHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            bool emailConfirmationRequired = false;
            _logger.LogInformation($"{request.Email} is trying to register");
            var newUser = new ApplicationUser(request.Username, request.FirstName, request.LastName, request.Email, true, false);

            IdentityResult userCreated = await _userManager.CreateAsync(newUser, request.Password);

            if (userCreated.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                if (emailConfirmationRequired)
                {
                    //TODO: Send email confirmation
                }
                else
                {
                    IdentityResult result = await _userManager.ConfirmEmailAsync(newUser, token);
                    if (result.Succeeded)
                    {

                        await _userManager.AddToRolesAsync(newUser, request.Roles.AsEnumerable());

                        return new Result(true, "User created");
                    }
                    else
                    {
                        return new Result(false, "User created but email confirmation failed");
                    }
                }
            }

            userCreated.Errors.ToList().ForEach(error => _logger.LogError("Error { ErrorCode }: { Description }", error.Code, error.Description));
            return new Result(false, "User not created");
        }
    }
}
