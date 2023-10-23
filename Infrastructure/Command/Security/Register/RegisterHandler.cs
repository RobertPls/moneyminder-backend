using Application.UseCase.Command.Security.Register;
using Application.Utils;
using Domain.Events.Users;
using Domain.Factories.Users;
using Infrastructure.EntityFramework.ReadModel.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SharedKernel.Core;

namespace Infrastructure.Command.Security.Register
{
    internal class RegisterHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly UserManager<UserReadModel> _userManager;
        private readonly ILogger<RegisterHandler> _logger;

        //--------Event
        private readonly IMediator _mediator;
        //-------------

        private readonly IUserFactory _userFactory;
        private readonly IUnitOfWork _unitOfWork;


        public RegisterHandler(UserManager<UserReadModel> userManager, IUserFactory userFactory, ILogger<RegisterHandler> logger, IUnitOfWork unitOfWort, IMediator mediator)
        {
            _userManager = userManager;
            _userFactory = userFactory;
            _mediator = mediator;
            _logger = logger;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var emailConfirmationRequired = false;
            List<string> rol = new List<string> { "ClientUser" };

            _logger.LogInformation($"{request.Email} is trying to register");
            var newUser = new UserReadModel(request.UserName, request.FirstName, request.LastName, request.Email, true, false);

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

                        var domainEvent = new CreatedUser(newUser.Id);
                        domainEvent.MarkAsConsumed();
                        await _mediator.Publish(domainEvent);
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
