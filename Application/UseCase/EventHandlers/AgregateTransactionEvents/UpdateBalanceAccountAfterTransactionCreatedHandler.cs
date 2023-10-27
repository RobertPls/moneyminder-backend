using Domain.Events.Transactions;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using Domain.Repositories.Transactions;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.EventHandlers.AgregateTransactionEvents
{
    public class UpdateBalanceAccountAfterTransactionCreatedHandler : INotificationHandler<CreatedTransaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBalanceAccountAfterTransactionCreatedHandler(IAccountRepository accountRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(CreatedTransaction notification, CancellationToken cancellationToken)
        {

            var account = await _accountRepository.FindByIdAsync(notification.AccountId);

            if(account == null) 
            {
                throw new NullReferenceException("User account is null, it's not possible to update its balance.");
            }

            account.UpdateBalance(notification.Amount, notification.Type, notification.IsTransfer);

            await _accountRepository.UpdateAsync(account);

            if (!notification.IsTransfer)
            {
                var userProfile = await _userProfileRepository.FindByIdAsync(account.UserProfileId);

                if (userProfile == null)
                {
                    throw new NullReferenceException("User profile is null, it's not possible to update its balance.");
                }
                userProfile.UpdateBalance(notification.Amount, notification.Type);
            }
        }
    }
}
