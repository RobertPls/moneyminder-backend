using Domain.Events.Transactions;
using Domain.Factories.Accounts;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using Domain.Repositories.Users;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.EventHandlers.AgregateTransactionEvents
{
    public class UpdateBalanceAccountAfterTransactionCreatedHandler : INotificationHandler<CreatedTransaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBalanceAccountAfterTransactionCreatedHandler(IAccountRepository accountRepository, IUserRepository userRepository, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(CreatedTransaction notification, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindByIdAsync(notification.AccountId);

            if(account == null) 
            {
                throw new NullReferenceException("User account is null, it's not possible to update its balance.");
            }

            if(notification.Type == TransactionType.Income) 
            {
                account.IncreaseBalance(notification.Amount);
            }
            else
            {
                account.DecreaseBalance(notification.Amount);
            }

            await _accountRepository.UpdateAsync(account);
        }
    }
}
