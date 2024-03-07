using Mock_Project_Group9.Models.Products;

namespace Mock_Project_Group9.Models.Users
{
    public class BuyUser
    {
        public Guid BuyUserId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public User? User {  get; set; }
        public Product? Product { get; set; }
    }
}
