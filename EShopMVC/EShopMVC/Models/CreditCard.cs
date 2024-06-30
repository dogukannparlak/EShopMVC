using System.ComponentModel.DataAnnotations;
using EShopMVC.CustomValidators;

namespace EShopMVC.Models
{
    public class CreditCard
    {
        public int Id { get; set; }

        [Required]
        [CreditCardNumber]
        [StringLength(19, MinimumLength = 19, ErrorMessage = "Credit Card Number must be 16 digits.")]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CardHolderName { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Expiration Date must be in MM/YY format")]
        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits")]
        public string CVV { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
