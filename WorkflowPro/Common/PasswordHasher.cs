using System;
using System.Security.Cryptography;

namespace WorkflowPro.Common
{
    /// <summary>
    /// Utility for secure password hashing and verification using PBKDF2.
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32;  // 256 bit
        private const int Iterations = 10000;

        /// <summary>
        /// Hashes a plain-text password using PBKDF2 with a cryptographically secure random salt.
        /// </summary>
        /// <param name="password">Plain-text password string.</param>
        /// <returns>Base64 encoded string containing iterations, salt, and hash key.</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", "password");
            }

            using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
            {
                byte[] key = algorithm.GetBytes(KeySize);
                byte[] salt = algorithm.Salt;

                byte[] hashBytes = new byte[1 + 4 + SaltSize + KeySize];
                hashBytes[0] = 0x01; // Format marker format version 1

                Buffer.BlockCopy(BitConverter.GetBytes(Iterations), 0, hashBytes, 1, 4);
                Buffer.BlockCopy(salt, 0, hashBytes, 5, SaltSize);
                Buffer.BlockCopy(key, 0, hashBytes, 5 + SaltSize, KeySize);

                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Verifies a plain-text password against a hashed password representation.
        /// </summary>
        /// <param name="password">Plain-text candidate password.</param>
        /// <param name="hashedPassword">Stored hashed password string.</param>
        /// <returns>True if password matches hash, false otherwise.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);

                if (hashBytes.Length != 1 + 4 + SaltSize + KeySize || hashBytes[0] != 0x01)
                {
                    return false;
                }

                int iterations = BitConverter.ToInt32(hashBytes, 1);

                byte[] salt = new byte[SaltSize];
                Buffer.BlockCopy(hashBytes, 5, salt, 0, SaltSize);

                byte[] key = new byte[KeySize];
                Buffer.BlockCopy(hashBytes, 5 + SaltSize, key, 0, KeySize);

                using (var algorithm = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    byte[] keyToCheck = algorithm.GetBytes(KeySize);
                    return ByteArraysEqual(key, keyToCheck);
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            int areSame = 0;
            for (int i = 0; i < a.Length; i++)
            {
                areSame |= a[i] ^ b[i];
            }
            return areSame == 0;
        }
    }
}

