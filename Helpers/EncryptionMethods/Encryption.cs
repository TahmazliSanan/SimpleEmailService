using System.Security.Cryptography;
using System.Text;

namespace Helpers.EncryptionMethods
{
    public static class Encryption
    {
        public static string GenerateSalt()
        {
            var provider = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[255];
            provider.GetNonZeroBytes(bytes);
            var salt = Convert.ToBase64String(bytes);
            return salt;
        }

        public static string GenerateHash(string password, string salt)
        {
            var algorithm = new HMACSHA256(Encoding.UTF8.GetBytes(salt));
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            return Encoding.UTF8.GetString(hash);
        }
    }
}