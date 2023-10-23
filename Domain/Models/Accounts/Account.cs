using Domain.ValueObjects;
using SharedKernel.Core;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Models.Accounts
{
    public class Account: AggregateRoot<Guid>
    {
        public Guid UserId { get; private set; }

        public AccountNameValue Name { get; private set; }

        public DescriptionValue Description { get; private set; }
        
        public decimal Balance { get; private set; }

        public Account(Guid userId, string name, string description) 
        {
            if (userId == Guid.Empty)
            {
                throw new BussinessRuleValidationException("The user cannot be empty");
            }

            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Description = description;
            Balance = 0;
        }

        public Account() { }

        public void IncreaseBalance(decimal amount)
        {
            MoneyValue amountMoney = new MoneyValue(amount);

            Balance = Balance + amountMoney;
        }

        public void DecreaseBalance(decimal amount)
        {
            MoneyValue amountMoney = new MoneyValue(amount);

            Balance = Balance - amountMoney;
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
