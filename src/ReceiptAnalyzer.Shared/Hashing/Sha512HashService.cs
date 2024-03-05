using System.Security.Cryptography;
using System.Text;

namespace BS.ReceiptAnalyzer.Shared.Hashing
{
    internal class Sha512HashService : IHashService
    {
        public string GetHash(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);

            return GetHash(bytes);
        }

        public string GetHash(byte[] data)
        {
            var hasher = SHA512.Create();
            var result = hasher.ComputeHash(data);
            return GetStringFromHash(result);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
