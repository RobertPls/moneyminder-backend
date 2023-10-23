using Application.Utils;
using Domain.Factories.Accounts;
using Domain.Models.Accounts;
using Domain.Models.Users;
using Domain.Repositories.Accounts;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Accounts.CreateAccount
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountFactory _accountFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountHandler(IAccountRepository accountRepository, IUserRepository userRepository, IAccountFactory accountFactory, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _accountFactory = accountFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null) return new Result(false, "User not found");

            var existingAccount = await _accountRepository.FindByNameAsync(user.Id, request.Name);
            if (existingAccount != null)return new Result(false, "An account with the same name already exists.");

            var account = _accountFactory.Create(userId: request.UserId!.Value,name: request.Name,descripcion: request.Description);

            await _accountRepository.CreateAsync(account);

            await _unitOfWork.Commit();

            return new Result(true, "Account created successfully.");

        }
    }
}
