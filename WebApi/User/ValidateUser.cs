/*namespace WebApi.Service
{
    public class ValidateUser
    {
        public async Task<bool> ValidateUser(string email, string password)
        {
            // First, fetch the user's hashed password from the database using their email
            string sql = @"SELECT PasswordHash FROM dbo.[User] WHERE Email = @Email";
            var storedHashedPasswordList = await _db.LoadData<string, dynamic>(sql, new { Email = email });

            if (!storedHashedPasswordList.Any())
                return false;

            string storedHashedPassword = storedHashedPasswordList.First();

            // Verify the provided password against the stored hash
            return _passwordHashingService.VerifyPassword(password, storedHashedPassword);
        }
    }
}*/
