using Hashers.Interfaces;

namespace Hashers.Services
{
    public class BCryptHasher : IBCryptHasher
    {
        private readonly string Salt;

        public BCryptHasher(string salt)
        {
            Salt = salt;
        }

        public string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, Salt);


        public bool VerifyHashedPassword(string password, string hashedPassword)
            => hashedPassword == Hash(password);
    }
}
