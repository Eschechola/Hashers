namespace Hashers.Interfaces
{
    public interface IBCryptHasher
    {
        string Hash(string password);
        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}
