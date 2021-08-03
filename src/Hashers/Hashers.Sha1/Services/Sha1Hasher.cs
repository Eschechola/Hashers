using Hashers.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Hashers.Services
{
    public class Sha1Hasher : ISha1Hasher
    {
        private readonly SHA1CryptoServiceProvider sha1CryptoTransform;

        public Sha1Hasher()
        {
            sha1CryptoTransform = new SHA1CryptoServiceProvider();
        }

        public string Hash(string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            string hashedPassword = BitConverter.ToString(sha1CryptoTransform.ComputeHash(buffer))
                .Replace("-", "")
                .ToLower();

            return hashedPassword;
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
            => hashedPassword == Hash(password);
    }
}
