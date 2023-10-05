using BlazorWeb.Models;

namespace BlazorWeb.Services
{
    public interface IAuthenticationService
    {
        Task<(bool, DisplayUserModel)> Login(LoginRequest loginRequest);
        Task Logout();
    }
}