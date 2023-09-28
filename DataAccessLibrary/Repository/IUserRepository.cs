using DataAccessLibrary.Models;


namespace DataAccessLibrary.Repository
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task CreateUser(UserModel user);

    }
}