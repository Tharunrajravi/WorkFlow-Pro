namespace WorkflowPro.Infrastructure
{
    // Thin wrapper around BCrypt.Net-Next so the rest of the app never
    // references the third-party namespace directly.
    public static class PasswordHasher
    {
        public static string Hash(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword, workFactor: 11);
        }

        public static bool Verify(string plainTextPassword, string passwordHash)
        {
            if (string.IsNullOrEmpty(plainTextPassword) || string.IsNullOrEmpty(passwordHash))
            {
                return false;
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(plainTextPassword, passwordHash);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Stored hash isn't a valid BCrypt hash (e.g. legacy/plaintext row).
                return false;
            }
        }
    }
}
