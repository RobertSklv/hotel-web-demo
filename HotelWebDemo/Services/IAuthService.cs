namespace HotelWebDemo.Services;

public interface IAuthService
{
    bool CompareHashes(string passwordInput, string passwordHash, byte[] salt);

    byte[] GenerateSalt();

    string Hash(string password, byte[]? salt = null);
}
