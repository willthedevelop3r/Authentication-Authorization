using BlazorWeb.Models;
using System.Net.Http.Json;

namespace BlazorWeb.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool, DisplayUserModel)> Login(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                var user = new DisplayUserModel
                {
                    FirstName = result.User.FirstName,
                    LastName = result.User.LastName,
                    EmailAddress = result.User.Email
                };
                return (true, user);
            }
            else
            {
                return (false, null);
            }
        }

        public async Task Logout()
        {
            await _httpClient.PostAsync("api/auth/logout", null);
        }

        // You can expand this service to handle other tasks as needed, like fetching user info.
    }
}
