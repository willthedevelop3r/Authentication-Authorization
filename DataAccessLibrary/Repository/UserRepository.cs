using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccessLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlDataAccess _db;
        
        public UserRepository(ISqlDataAccess db)
        {
            _db = db;
      
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            string sql = @"SELECT UserId, Firstname, LastName, Email FROM dbo.[User]";

            return await _db.LoadData<UserModel, dynamic>(sql, new { });
        }

        public async Task CreateUser(UserModel user)
        {

            string sql = @"INSERT INTO dbo.[User] (FirstName, LastName, Email, PasswordHash)
                           VALUES (@FirstName, @LastName, @Email, @PasswordHash)";

            await _db.SaveData(sql, user);
        }

       
    }
}
