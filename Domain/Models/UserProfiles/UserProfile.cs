using Domain.Events.UserProfiles;
using Domain.Models.Transactions;
using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.UserProfiles

{
    public class UserProfile : AggregateRoot<Guid>
    {
        public Guid UserId { get; private set; }
        public string FullName { get; private set; }
        public decimal Balance { get; private set; }


        public UserProfile(Guid userId, string fullName)
        {
            if (userId == Guid.Empty)
            {
                throw new BussinessRuleValidationException("The user cannot be empty");
            }
            Id = Guid.NewGuid();
            UserId = userId;
            FullName = fullName;
            Balance = 0;
        }

        public UserProfile() { }

        public void UpdateBalance(decimal amount, TransactionType type)
        {
            MoneyValue amountMoney = new MoneyValue(amount);


            if (type == TransactionType.Income)
            {
                Balance = Balance + amountMoney.Value;
            }
            else
            {
                Balance = Balance - amountMoney;
            }

        }

        public void Created()
        {
            AddDomainEvent(new CreatedUserProfile(Id));
        }
    }
}
