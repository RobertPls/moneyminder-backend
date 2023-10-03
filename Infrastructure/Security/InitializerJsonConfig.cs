namespace Infrastructure.Security
{
    public class InitializerJsonConfig
    {
        public List<InitializerJsonRole> roles { get; set; }
        public List<InitializerJsonUser> users { get; set; }
    }

    public class InitializerJsonRole
    {
        public string role { get; set; }
        public List<InitializerJsonPermission> permissions { get; set; }
    }

    public class InitializerJsonPermission
    {
        public string permission { get; set; }
    }

    public class InitializerJsonUser
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string jobtitle { get; set; }
        public List<InitializerJsonUserRole> userroles { get; set; }
    }

    public class InitializerJsonUserRole
    {
        public string role { get; set; }
    }
}
