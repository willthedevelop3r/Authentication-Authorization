namespace WebApi.dto
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
