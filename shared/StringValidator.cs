using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.shared
{
    public class StringValidator
    {
        public static bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}