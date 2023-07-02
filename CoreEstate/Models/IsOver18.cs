using System.ComponentModel.DataAnnotations;

namespace CoreEstate.Models
{
    public class IsOver18 : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = (WebUser) validationContext.ObjectInstance;

            if (user.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required to book a viewing.");
            }

            var age = DateTime.Today.Year - user.Birthdate.Value.Year;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("User needs to be 18+ years old to book a viewing.");
        }
    }
}
