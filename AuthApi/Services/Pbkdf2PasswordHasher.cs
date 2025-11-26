using System.Security.Cryptography;

namespace AuthApi.Services
{
    public class Pbkdf2PasswordHasher : IPasswordHasher
    {
        public void CreateHash(string password, out string hash, out string salt)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            salt = Convert.ToBase64String(saltBytes);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            hash = Convert.ToBase64String(hashBytes);
        }

        public bool Verify(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
            byte[] computedHashBytes = pbkdf2.GetBytes(32);
            string computedHash = Convert.ToBase64String(computedHashBytes);

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(storedHash),
                Convert.FromBase64String(computedHash));
        }
    }
}
