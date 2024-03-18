using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Models.Products;

namespace Mock_Project_Group9.Pages.Client.Products
{
    public class IndexModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public IndexModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.products != null)
            {
                Products = await _context.products
                .Include(p => p.Category).ToListAsync();
            }
        }
    }
}
