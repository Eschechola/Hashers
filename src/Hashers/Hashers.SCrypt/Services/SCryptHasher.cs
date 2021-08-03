using Hashers.Interfaces;
using Scrypt;

namespace Hashers.Services
{
    public class SCryptHasher : ISCryptHasher
    {
        private readonly ScryptEncoder _encoder;

        public SCryptHasher(ScryptEncoder encoder)
        {
            _encoder = encoder;
        }

        public string Hash(string password)
            => _encoder.Encode(password);


        public bool VerifyHashedPassword(string password, string hashedPassword)
            => hashedPassword == Hash(password);
    }
}
