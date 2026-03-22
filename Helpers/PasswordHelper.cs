using System.Security.Cryptography;
using System.Text;

namespace GreenCityManagement.Helpers
{
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }

        public static bool Verify(string password, string hash) => Hash(password) == hash;
    }
}
