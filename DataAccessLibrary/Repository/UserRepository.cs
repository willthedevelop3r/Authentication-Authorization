using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Service;
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
        private readonly IPasswordHashingService _passwordHashingService;

        public UserRepository(ISqlDataAccess db, IPasswordHashingService passwordHashingService)
        {
            _db = db;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            string sql = @"SELECT * FROM dbo.[User]";

            return await _db.LoadData<UserModel, dynamic>(sql, new { });
        }

        public async Task CreateUser(UserModel user)
        {
            // Hash the user's password before saving it to the database
            user.PasswordHash = _passwordHashingService.HashPassword(user.PasswordHash);

            string sql = @"INSERT INTO dbo.[User] (FirstName, LastName, Email, PasswordHash)
                           VALUES (@FirstName, @LastName, @Email, @PasswordHash)";

            await _db.SaveData(sql, user);
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            // First, fetch the user's hashed password from the database using their username
            string sql = @"SELECT PasswordHash FROM dbo.[User] WHERE Email = @Email";
            var storedHashedPasswordList = await _db.LoadData<string, dynamic>(sql, new { Email = email });

            if (!storedHashedPasswordList.Any())
                return false;

            string storedHashedPassword = storedHashedPasswordList.First();

            // Verify the provided password against the stored hash
            return _passwordHashingService.VerifyPassword(password, storedHashedPassword);
        }
    }
}
