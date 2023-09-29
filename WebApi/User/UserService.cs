using DataAccessLibrary.Models;
using DataAccessLibrary.Repository;
using System.ComponentModel;
using System.Threading.Tasks;
using WebApi.Password;
using System.Collections.Generic;

namespace WebApi.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;

        public UserService(IUserRepository userRepository, IPasswordHashingService passwordHashingService)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task CreateUser(UserModel user)
        {
            // Hash the user's password before sending it for storage
            user.PasswordHash = _passwordHashingService.HashPassword(user.PasswordHash);

            // Save the user with hashed password
            await _userRepository.CreateUser(user);
        }

        public async Task<UserModel> ValidateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
                return null;

            // Verify the provided password against the stored hash
            bool isPasswordValid = _passwordHashingService.VerifyPassword(password, user.PasswordHash);

            return isPasswordValid ? user : null;
        }

    }
}

