using Microsoft.AspNetCore.Identity;


namespace SmartHealthCareSystem.Infrastructure.Utilities;
    public static class PasswordHasherHelper
    {
        private static readonly PasswordHasher<object> Hasher = new PasswordHasher<object>();

        public static string HashPassword(string password)
        {
            return Hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = Hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
