using DataAccessLibrary.Models;

namespace WebApi.User
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();
        Task CreateUser(UserModel user);
        
    }
}