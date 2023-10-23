using Domain.Events.Transactions;
using Domain.Factories.Accounts;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using MediatR;
using SharedKernel.Core;

namespace Application.UseCase.EventHandlers.AgregateTransactionEvents
{
    public class UpdateBalanceAccountAfterTransactionDeletedHandler : INotificationHandler<DeletedTransaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountFactory _accountFactory;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBalanceAccountAfterTransactionDeletedHandler(IAccountRepository accountRepository, IAccountFactory accountFactory, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _accountFactory = accountFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(DeletedTransaction notification, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindByIdAsync(notification.AccountId);

            if (account == null)
            {
                throw new NullReferenceException("User account is null, it's not possible to update its balance.");
            }

            if (notification.Type == TransactionType.Income)
            {
                account.DecreaseBalance(notification.Amount);
            }
            else
            {
                account.IncreaseBalance(notification.Amount);
            }

            await _accountRepository.UpdateAsync(account);
        }
    }
}
