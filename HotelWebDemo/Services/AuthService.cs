using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HotelWebDemo.Services;

public class AuthService : IAuthService
{
    public bool CompareHashes(string passwordInput, string passwordHash, byte[] salt)
    {
        return Hash(passwordInput, salt) == passwordHash;
    }

    public byte[] GenerateSalt()
    {
        byte[] salt = new byte[128 / 8];
        using RandomNumberGenerator rand = RandomNumberGenerator.Create();
        rand.GetBytes(salt);

        return salt;
    }

    public string Hash(string password, byte[]? salt = null)
    {
        salt ??= GenerateSalt();

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}
