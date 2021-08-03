namespace Hashers.Interfaces
{
    public interface IArgon2idHasher
    {
        string Hash(string password);
        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}
