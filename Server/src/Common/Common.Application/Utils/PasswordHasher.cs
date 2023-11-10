using System.Security.Cryptography;
using System.Text;

namespace Common.Application.Utils;

public static class PasswordHasher
{
    const int keySize = 64;

    const int randonStringLength = 33;

    const int iterations = 241293;

    private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        var saltString = Convert.ToBase64String(salt);
        var hashString = Convert.ToHexString(hash);
        var hashToDb = saltString + hashString;
        return hashToDb;
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var splitHash = hash.Split("==");
        var saltString = splitHash[0] + "==";
        var hashString = splitHash[1];

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, System.Convert.FromBase64String(saltString), iterations,
            hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashString));
    }

    private const string Chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";

    private static string GetRandomString(int length)
    {
        var stringBuilder = new StringBuilder();

        var random = new Random();

        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(random.Next(Chars.Length - 1));
        }

        return stringBuilder.ToString();
    }
}