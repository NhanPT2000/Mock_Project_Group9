using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mock_Project_Group9.Models.Orders;
using Mock_Project_Group9.Models.Products;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Database
{
    public class WebDBContext : DbContext
    {
        public WebDBContext() { }
        public WebDBContext(DbContextOptions<WebDBContext> options) : base(options) 
        { }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<BuyUser> buyUsers { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserDetails> userDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:WebDb");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<UserDetails>().HasOne(u => u.User)
                .WithOne(u => u.UserDetails)
                .HasForeignKey<User>(ud => ud.UserId);
            modelBuilder.Entity<Order>().HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
            modelBuilder.Entity<Product>().HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<BuyUser>()
                .HasKey(e => new {e.BuyUserId, e.UserId, e.ProductId });
            modelBuilder.Entity<BuyUser>()
                .HasOne(bu => bu.User)
                .WithMany(u => u.BuyUsers)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BuyUser>()
                .HasOne(bu => bu.Product)
                .WithMany(p => p.BuyUsers)
                .HasForeignKey(bu => bu.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderDetails>()
                .HasKey(e => new { e.OrderDetailId, e.OrderId, e.ProductId });
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
