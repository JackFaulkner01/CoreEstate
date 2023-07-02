using System.ComponentModel.DataAnnotations;

namespace CoreEstate.Models
{
    public class IsOver18 : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var customer = (WebUser) validationContext.ObjectInstance;

            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required to book a viewing.");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Customer needs to be 18+ years old to book a viewing.");
        }
    }
}
