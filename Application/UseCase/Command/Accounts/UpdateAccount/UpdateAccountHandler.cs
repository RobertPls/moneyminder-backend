using Application.Utils;
using Domain.Repositories.Accounts;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Accounts.UpdateAccount
{

    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Result>
    {
        private readonly IUserProfileRepository _userUserProfileRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountHandler(IAccountRepository accountRepository, IUserProfileRepository userUserProfileRepository, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userUserProfileRepository = userUserProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _userUserProfileRepository.FindByUserIdAsync(request.UserId!.Value);
            if (userProfile == null) return new Result(false, "User not found");

            var account = await _accountRepository.FindByIdAsync(request.AccountId);
            if (account == null)return new Result(false, "User Account not found");           
            if (account.UserProfileId != userProfile.Id) return new Result(false, "The user is not the owner of this account");           

            account.UpdateDescription(request.Description);

            account.UpdateName(request.Name);

            await _accountRepository.UpdateAsync(account);

            await _unitOfWork.Commit();

            return new Result<string>(true, "Account has updated");

        }
    }
}
