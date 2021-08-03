namespace Hashers.Interfaces
{
    public interface ISCryptHasher
    {
        string Hash(string password);
        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}
