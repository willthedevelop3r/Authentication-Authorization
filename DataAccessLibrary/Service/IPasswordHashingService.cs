﻿namespace DataAccessLibrary.Service
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}