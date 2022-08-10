using System.ComponentModel.DataAnnotations;

namespace PetDream.Domain.Validation
{
    public static class ValidateEmail
    {
        public static bool IsValid(string email)
        {
            var emailValidation = new EmailAddressAttribute().IsValid(email);
            return emailValidation;
        }
    }
}