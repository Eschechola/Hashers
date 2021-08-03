namespace Hashers.Interfaces
{
    public interface ISha1Hasher
    {
        string Hash(string password);
        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}
