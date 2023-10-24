﻿using Domain.Events.Accounts;
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

        public void IncreaseBalance(decimal amount, bool isTransference)
        {
            MoneyValue amountMoney = new MoneyValue(amount);

            Balance = Balance + amountMoney;

            if(isTransference == false)
            {
                AddDomainEvent(new IncreasedAccountBalance(Id, amount));
            }
        }

        public void DecreaseBalance(decimal amount, bool isTransference)
        {
            MoneyValue amountMoney = new MoneyValue(amount);

            Balance = Balance - amountMoney;

            if (isTransference == false)
            {
                AddDomainEvent(new DecreasedAccountBalance(Id, amount));
            }
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
    }
}
