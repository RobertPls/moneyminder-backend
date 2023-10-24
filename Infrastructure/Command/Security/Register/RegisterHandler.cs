using Application.UseCase.Command.Security.Register;
using Application.Utils;
using Domain.Factories.UserProfiles;
using Domain.Repositories.UserProfiles;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SharedKernel.Core;

namespace Infrastructure.Command.Security.Register
{
    internal class RegisterHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterHandler> _logger;

        private readonly IUserProfileFactory _userProfileFactory;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;


        public RegisterHandler(UserManager<ApplicationUser> userManager,IUserProfileRepository userProfileRepository, IUserProfileFactory userProfileFactory, ILogger<RegisterHandler> logger, IUnitOfWork unitOfWort)
        {
            _userManager = userManager;
            _userProfileFactory = userProfileFactory;
            _userProfileRepository = userProfileRepository;
            _logger = logger;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var emailConfirmationRequired = false;
            List<string> rol = new List<string> { "ClientUser" };

            _logger.LogInformation($"{request.Email} is trying to register");

            var newUser = new ApplicationUser(request.UserName, request.FirstName, request.LastName, request.Email, true, false);

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
                        await _userManager.AddToRolesAsync(newUser, rol );

                        var userUserProfile = _userProfileFactory.Create(newUser.Id, newUser.FullName);

                        await _userProfileRepository.CreateAsync(userUserProfile);
                        
                        await _unitOfWork.Commit();
                        
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
