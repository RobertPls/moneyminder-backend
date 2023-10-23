using Domain.ValueObjects;
using SharedKernel.Core;

namespace Domain.Models.Users
{
    public class User : AggregateRoot<Guid>
    {
        public UserNameValue UserName { get; set; }
        public FirstNameValue FirstName { get; set; }
        public LastNameValue LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public EmailValue Email { get; set; }
        public decimal Balance { get; private set; }
        public bool Active { get; set; }
        public bool Staff { get; set; }

        internal User(string userName, string lastName, string firstName, string email, bool active, bool staff)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Active = active;
            Staff = staff;
            Balance = 0;
        }

        public User() { }

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
