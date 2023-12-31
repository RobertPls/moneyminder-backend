﻿using Domain.Events.Accounts;
using Domain.Models.Transactions;
using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.Accounts
{
    public class Account: AggregateRoot<Guid>
    {
        public Guid UserProfileId { get; private set; }

        public AccountNameValue Name { get; private set; }

        public DescriptionValue Description { get; private set; }
        
        public decimal Balance { get; private set; }

        public Account(Guid userProfileId, string name, string description) 
        {
            if (userProfileId == Guid.Empty)
            {
                throw new BussinessRuleValidationException("The user cannot be empty");
            }

            Id = Guid.NewGuid();
            UserProfileId = userProfileId;
            Name = name;
            Description = description;
            Balance = 0;
        }

        public Account() { }

        public void UpdateAccount(string name, string description)
        {
            Name = name;
            Description = description;

        }

        public void UpdateBalance(decimal amount, TransactionType type, bool isTransfer)
        {
            MoneyValue amountMoney = new MoneyValue(amount);

            if(!isTransfer)
            {
                AddDomainEvent(new UpdatedAccountBalance(Id, amount, type));
            }

            if (type == TransactionType.Income)
            {
                Balance = Balance + amountMoney.Value;
            }
            else
            {
                Balance = Balance - amountMoney;
            }

        }

    }
}
