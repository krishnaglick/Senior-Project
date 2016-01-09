
using System;
using System.Security.Cryptography;
using System.Text;

namespace SrProj.API
{
    public class AuthorizationUser
    {
        //string user
        //time stamp diff
    }

    public class Authorization
    {
        //TODO: Something with these...
        private const string _alg = "HMACSHA256";
        private const string _salt = "IPritBFDbXRAoo3u651v";
        private const int _expirationMinutes = 10;

        public static string GenerateToken(string username)
        {
            var hashData = string.Join(":", username, DateTime.UtcNow.Ticks);

            string hashHmac;

            using (HMAC hmac = HMAC.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hashData));

                hashHmac = Convert.ToBase64String(hmac.Hash);
            }

            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashHmac, hashData)));
            return token;
        }

        //TODO: Improve this.
        public static bool ValidateToken(string token, string username)
        {
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            string[] parts = key.Split(':');

            if (parts.Length == 3)
            {
                string hashedUsername = parts[1];
                DateTime timeStamp = new DateTime(long.Parse(parts[2]));

                return Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes
                    && username == hashedUsername;
            }
            return false;
        }
    }
}