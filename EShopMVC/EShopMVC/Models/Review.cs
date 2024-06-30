using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopMVC.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Author => "Any User";

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

