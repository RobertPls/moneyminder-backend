using Domain.Events.Accounts;
using Domain.Repositories.Accounts;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.EventHandlers.AgregateAccountEvents
{

    public class UpdateBalanceUserProfileAfterIncreaseAccountBalance : INotificationHandler<IncreasedAccountBalance>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBalanceUserProfileAfterIncreaseAccountBalance(IAccountRepository accountRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(IncreasedAccountBalance notification, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindByIdAsync(notification.AccountId);

            if (account == null)
            {
                throw new NullReferenceException("User account is null, it's not possible to update its balance.");
            }


            var userProfile = await _userProfileRepository.FindByIdAsync(account.UserProfileId);

            if (userProfile == null)
            {
                throw new NullReferenceException("User profile is null, it's not possible to update its balance.");
            }

            userProfile.IncreaseBalance(notification.Ammount);

            await _accountRepository.UpdateAsync(account);

            await _unitOfWork.Commit();
        }
    }
}
