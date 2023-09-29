using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.User
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();
        Task CreateUser(UserModel user);
        Task<UserModel> ValidateUser(string email, string password);
    }
}