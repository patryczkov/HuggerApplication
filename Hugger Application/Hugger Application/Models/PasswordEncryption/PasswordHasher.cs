﻿using System;
using System.Security.Cryptography;

namespace Hugger_Application.Models.PasswordEncryption
{
    public class PasswordHasher
    {
        public static string PasswordHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            Console.WriteLine(Convert.ToBase64String(hashBytes));
            return Convert.ToBase64String(hashBytes);
        }

        public static void Decrypter(string password)
        {
            string savedPasswordHash = PasswordHash("password");
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();

            Console.WriteLine("password matches");
        }
    }
}
