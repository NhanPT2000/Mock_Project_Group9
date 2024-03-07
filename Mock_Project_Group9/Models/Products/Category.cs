using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mock_Project_Group9.Models.Products
{
    public class Category
    {
        [Key]
        [Required]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required.")]
        [Column(TypeName ="nvarchar(50)")]
        public string CategoryName { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        [DisplayFormat(NullDisplayText ="No status")]
        public string? Status { get; set; }
        public ICollection<Product>? Products { get; set;}
    }
}
