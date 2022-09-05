using System;
using System.Text.RegularExpressions;

namespace Sunstone.Domain.Helpers
{
    public static class ValidationHelper
    {
        private static string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public static bool IsValidEmail(string email)
        {
            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidAge(DateTime dateOfBirth)
        {
            if (DateTime.Now.Year - dateOfBirth.Year >= 18)
            {
                return true;
            }
            return false;
        }
    }
}
