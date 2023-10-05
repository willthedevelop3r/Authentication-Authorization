namespace BlazorWeb.Models
{
    public class LoginResult
    {
        public string Message { get; set; }
        public UserDetails User { get; set; }

        public class UserDetails
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
    }
}
