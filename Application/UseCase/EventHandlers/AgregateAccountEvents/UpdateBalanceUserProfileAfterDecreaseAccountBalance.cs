using Domain.Events.Accounts;
using Domain.Repositories.Accounts;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.EventHandlers.AgregateAccountEvents
{
    public class UpdateBalanceUserProfileAfterDecreaseAccountBalance : INotificationHandler<DecreasedAccountBalance>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBalanceUserProfileAfterDecreaseAccountBalance(IAccountRepository accountRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(DecreasedAccountBalance notification, CancellationToken cancellationToken)
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

            userProfile.DecreaseBalance(notification.Ammount);

            await _accountRepository.UpdateAsync(account);

            await _unitOfWork.Commit();
        }
    }
}
