using DataAccessLibrary.Models;


namespace DataAccessLibrary.Repository
{
    public interface IUserRepository
    {
        Task CreateUser(UserModel user);
        Task<List<UserModel>> GetUsers();

    }
}