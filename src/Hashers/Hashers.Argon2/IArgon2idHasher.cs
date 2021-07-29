using System.Threading.Tasks;

namespace Hashers.Argon
{
    public interface IArgon2idHasher
    {
        string Hash(string password);
        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}
