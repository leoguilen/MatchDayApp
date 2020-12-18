using System;
using System.Security.Cryptography;
using System.Text;

namespace MatchDayApp.Domain.Helpers
{
    public static class SenhaHasherHelper
    {
        public static string CriarSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GerarHash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool SaoIguais(string password, string hashedInput, string salt)
        {
            string newHashedPin = GerarHash(password, salt);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
