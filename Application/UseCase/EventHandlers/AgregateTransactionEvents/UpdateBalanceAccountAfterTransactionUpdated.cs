using Domain.Events.Transactions;
using Domain.Factories.Accounts;
using Domain.Models.Accounts;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using Domain.Repositories.UserProfiles;
using MediatR;
using SharedKernel.Core;
using System.Security.Principal;

namespace Application.UseCase.EventHandlers.AgregateTransactionEvents
{

    public class UpdateBalanceAccountAfterTransactionUpdated : INotificationHandler<UpdatedTransaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountFactory _accountFactory;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBalanceAccountAfterTransactionUpdated(IAccountRepository accountRepository, IUserProfileRepository userProfileRepository, IAccountFactory accountFactory, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _accountFactory = accountFactory;
            _unitOfWork = unitOfWort;
            _userProfileRepository = userProfileRepository;
        }

        public async Task Handle(UpdatedTransaction notification, CancellationToken cancellationToken)
        {
            var oldAccount = await _accountRepository.FindByIdAsync(notification.OldAccountId);
            var newAccount = await _accountRepository.FindByIdAsync(notification.NewAccountId);

            if (oldAccount == null || newAccount == null)
            {
                throw new NullReferenceException("User account is null, it's not possible to update its balance.");
            }

            var oldType = notification.OldType == TransactionType.Outcome ? TransactionType.Income : TransactionType.Outcome;
            
            oldAccount.UpdateBalance(notification.OldAmount, oldType, notification.IsTransference);

            newAccount.UpdateBalance(notification.NewAmount, notification.NewType, notification.IsTransference);


            await _accountRepository.UpdateAsync(oldAccount);

            await _accountRepository.UpdateAsync(newAccount);


            var userProfile = await _userProfileRepository.FindByIdAsync(newAccount.UserProfileId);


            if (userProfile == null)
            {
                throw new NullReferenceException("User profile is null, it's not possible to update its balance.");
            }
            
            if(!notification.WasTransference && notification.IsTransference)
            {
                userProfile.UpdateBalance(notification.OldAmount, oldType);
            }
            else if ((!notification.WasTransference && !notification.IsTransference) || notification.WasTransference && !notification.IsTransference)
            {
                userProfile.UpdateBalance(notification.OldAmount, oldType);
                userProfile.UpdateBalance(notification.NewAmount, notification.NewType);
            }


            await _userProfileRepository.UpdateAsync(userProfile);
            

        }
    }
}
