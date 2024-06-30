using System.ComponentModel.DataAnnotations;

namespace EShopMVC.CustomValidators
{
    public class CreditCardNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var creditCardNumber = value.ToString().Replace(" ", ""); 
                if (creditCardNumber.Length == 16 && long.TryParse(creditCardNumber, out _))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Credit Card Number must be 16 digits.");
        }
    }
}
