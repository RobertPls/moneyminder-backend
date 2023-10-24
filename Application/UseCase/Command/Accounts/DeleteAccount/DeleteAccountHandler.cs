using Application.Utils;
using Domain.Repositories.Accounts;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.Command.Accounts.DeleteAccount
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Result>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountHandler(IAccountRepository accountRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.FindByUserIdAsync(request.UserId!.Value);
            if (userProfile == null) return new Result(false, "User not found");

            var account = await _accountRepository.FindByIdAsync(request.AccountId);
            if (account == null) return new Result(false, "User Account not found");           
            if (account.UserProfileId != userProfile.Id)return new Result(false, "The user is not the owner of this account");          

            await _accountRepository.RemoveAsync(account);

            await _unitOfWork.Commit();

            return new Result(true, "Account has deleted");

        }
    }
}
