using System;
using System.Security.Cryptography;
using System.Text;

namespace Arival.Domain.Service
{
    public static class PasswordHasher
    {
        private const int KeySize = 32; 
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        public static string Hash(string password)
        {
            var salt = Encoding.ASCII.GetBytes("1C1C1C1C1C1C1C1C");
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
            return Convert.ToBase64String(hash);
        }

    }
}
