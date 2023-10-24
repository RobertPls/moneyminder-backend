using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.UserProfiles

{
    public class UserProfile : AggregateRoot<Guid>
    {
        public Guid UserId { get; private set; }
        public string FullName { get; private set; }
        public decimal Balance { get; private set; }


        internal UserProfile(Guid userId, string fullName)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Balance = 0;
        }

        public UserProfile() { }

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
    }
}
