using DataAccessLibrary.Models;
using DataAccessLibrary.Repository;
using System.Threading.Tasks;
using WebApi.Password;

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

        public async Task CreateUser(UserModel user)
        {
            // Hash the user's password before sending it for storage
            user.PasswordHash = _passwordHashingService.HashPassword(user.PasswordHash);

            // Save the user with hashed password
            await _userRepository.CreateUser(user);
        }

        /*  public async Task<bool> ValidateUser(string email, string password)
          {
              string storedHashedPassword = await _userRepository.GetPasswordByEmail(email);

              if (string.IsNullOrEmpty(storedHashedPassword))
                  return false;

              // Verify the provided password against the stored hash
              return _passwordHashingService.VerifyPassword(password, storedHashedPassword);
          }*/
    }
}