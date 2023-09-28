using DataAccessLibrary.Models;

namespace WebApi.User
{
    public interface IUserService
    {
        Task CreateUser(UserModel user);
    }
}