using System;
using System.Text.RegularExpressions;

namespace WorkflowPro.Common
{
    /// <summary>
    /// Reusable domain validation helper methods for business logic operations.
    /// </summary>
    public static class ValidationUtils
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex PhoneRegex = new Regex(
            @"^\+?[0-9\s\-\(\)]{7,20}$",
            RegexOptions.Compiled);

        /// <summary>
        /// Validates email address format against standard email pattern.
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return EmailRegex.IsMatch(email.Trim());
        }

        /// <summary>
        /// Validates phone number format.
        /// </summary>
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return true; // Optional property
            return PhoneRegex.IsMatch(phone.Trim());
        }

        /// <summary>
        /// Validates that a date range has end date greater than or equal to start date.
        /// </summary>
        public static void ValidateDateRange(DateTime startDate, DateTime? endDate, string fieldName = "Date range")
        {
            if (endDate.HasValue && endDate.Value < startDate)
            {
                throw new ValidationException(string.Format("{0} is invalid: End date ({1:yyyy-MM-dd}) cannot be earlier than start date ({2:yyyy-MM-dd}).", fieldName, endDate.Value, startDate));
            }
        }

        /// <summary>
        /// Validates numeric value against acceptable minimum and maximum boundaries.
        /// </summary>
        public static void ValidateRange(decimal value, decimal min, decimal max, string paramName)
        {
            if (value < min || value > max)
            {
                throw new ValidationException(string.Format("{0} value ({1}) must be between {2} and {3}.", paramName, value, min, max));
            }
        }
    }
}

