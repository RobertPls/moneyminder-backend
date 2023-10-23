using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EntityFramework.ReadModel.Users
{
    public class UserReadModel : IdentityUser<Guid>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public bool Staff { get; set; }
        public decimal Balance { get; set; }

        public UserReadModel(string username, string firstName, string lastName, string email, bool active, bool staff) : base(username)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Active = active;
            Staff = staff;
        }

        public UserReadModel(Guid id, string username, string firstName, string lastName, string email, bool active, bool staff, decimal balance) : base(username)
        {
            Id = id;
            UserName = username;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Active = active;
            Staff = staff;
            Balance = balance;
        }

        private UserReadModel()
        {
            LastName = "";
            FirstName = "";
        }
    }

   
}
