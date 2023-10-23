﻿using Domain.Events.Transactions;
using Domain.Factories.Accounts;
using Domain.Models.Accounts;
using Domain.Models.Transactions;
using Domain.Repositories.Accounts;
using MediatR;
using SharedKernel.Core;
using System.Security.Principal;

namespace Application.UseCase.EventHandlers.AgregateTransactionEvents
{

    public class UpdateBalanceAccountAfterTransactionUpdated : INotificationHandler<UpdatedTransaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountFactory _accountFactory;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBalanceAccountAfterTransactionUpdated(IAccountRepository accountRepository, IAccountFactory accountFactory, IUnitOfWork unitOfWort)
        {
            _accountRepository = accountRepository;
            _accountFactory = accountFactory;
            _unitOfWork = unitOfWort;
        }

        public async Task Handle(UpdatedTransaction notification, CancellationToken cancellationToken)
        {
            var oldAccount = await _accountRepository.FindByIdAsync(notification.OldAccountId);
            var newAccount = await _accountRepository.FindByIdAsync(notification.NewAccountId);

            if (oldAccount == null || newAccount == null)
            {
                throw new NullReferenceException("User account is null, it's not possible to update its balance.");
            }

            if(oldAccount.Id == newAccount.Id && notification.OldType == notification.NewType && notification.OldType == notification.NewType)
            {
                if (notification.NewType == TransactionType.Income)
                {
                    newAccount.DecreaseBalance(notification.OldAmount);
                    newAccount.IncreaseBalance(notification.NewAmount);
                }
                else
                {
                    newAccount.IncreaseBalance(notification.OldAmount);
                    newAccount.DecreaseBalance(notification.NewAmount);
                }
            }
            else
            {
                if (notification.OldType == TransactionType.Income)
                {
                    oldAccount.DecreaseBalance(notification.OldAmount);
                }
                else
                {
                    oldAccount.IncreaseBalance(notification.OldAmount);
                }

                if (notification.NewType == TransactionType.Income)
                {
                    newAccount.IncreaseBalance(notification.NewAmount);
                }
                else
                {
                    newAccount.DecreaseBalance(notification.NewAmount);
                }
            }


            await _accountRepository.UpdateAsync(oldAccount);
            await _accountRepository.UpdateAsync(newAccount);
        }
    }
}