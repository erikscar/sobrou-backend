using System.Security.Cryptography;
using sobrou.Infrastructure.Repositories.Interfaces;

namespace sobrou.Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;
    private const int HashSize = 32;
    private const int SaltSize = 16;
    private const int Iterations = 100000;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, AlgorithmName, HashSize);

        return string.Join(":", "PBKDF2", AlgorithmName.Name, Iterations, Convert.ToHexString(hash), Convert.ToHexString(salt));
    }

    public bool Verify(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split(":");
        byte[] hash = Convert.FromHexString(parts[3]);
        byte[] salt = Convert.FromHexString(parts[4]);

        byte[] inputhash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, AlgorithmName, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputhash);
    }
}