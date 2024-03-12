using Mock_Project_Group9.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Mock_Project_Group9.Models.Orders
{
    public class Order
    {
        [Key]
        [Required]
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [DisplayFormat(NullDisplayText ="No status")]
        public string? Status {  get; set; }

        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}
