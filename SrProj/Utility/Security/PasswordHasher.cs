
using Microsoft.AspNet.Identity;

namespace SrProj.Utility.Security
{
    public static class PasswordHasher
    {
        private static readonly Microsoft.AspNet.Identity.PasswordHasher hasher = new Microsoft.AspNet.Identity.PasswordHasher();

        public static string EncryptPassword(string password)
        {
            return hasher.HashPassword(password);
        }

        public static PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
        {
            return hasher.VerifyHashedPassword(hashedPassword, providedPassword);
        }
    }
}