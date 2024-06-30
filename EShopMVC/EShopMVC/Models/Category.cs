using System.ComponentModel.DataAnnotations;


namespace EShopMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
