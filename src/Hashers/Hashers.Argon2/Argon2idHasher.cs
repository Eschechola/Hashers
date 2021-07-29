using Isopoh.Cryptography.Argon2;
using System.Text;

namespace Hashers.Argon
{
    public class Argon2idHasher : IArgon2idHasher
    {
        private readonly Argon2Config _argon2Config;

        public Argon2idHasher(Argon2Config argon2Config)
        {
            _argon2Config = argon2Config;
        }

        public string Hash(string password)
        {
            _argon2Config.Password = Encoding.UTF8.GetBytes(password);
            var argon2A = new Argon2(_argon2Config);

            return _argon2Config.EncodeString(argon2A.Hash().Buffer);
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
            => hashedPassword == Hash(password);
    }
}
