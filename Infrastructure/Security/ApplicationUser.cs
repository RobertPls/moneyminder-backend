using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public bool Staff { get; set; }

        public ApplicationUser(string username, string firstName, string lastName, string email, bool active, bool staff) : base(username)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Active = active;
            Staff = staff;
        }

        private ApplicationUser()
        {
            LastName = "";
            FirstName = "";
        }
    }

    public class ApplicationRole : IdentityRole<Guid> { }
}
