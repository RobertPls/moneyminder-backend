using Application.Utils;
using Domain.Models.Accounts;
using Domain.Models.Users;
using Domain.Repositories.Accounts;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Accounts.UpdateAccount
{

    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountHandler(IAccountRepository accountRepository, IUserRepository userRepository, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.UserId!.Value);
            if (user == null) return new Result(false, "User not found");

            var account = await _accountRepository.FindByIdAsync(request.AccountId);
            if (account == null)return new Result(false, "User Account not found");           
            if (account.UserId != request.UserId) return new Result(false, "The user is not the owner of this account");           

            account.UpdateDescription(request.Description);

            account.UpdateName(request.Name);

            await _accountRepository.UpdateAsync(account);

            await _unitOfWork.Commit();

            return new Result<string>(true, "Account has updated");

        }
    }
}
