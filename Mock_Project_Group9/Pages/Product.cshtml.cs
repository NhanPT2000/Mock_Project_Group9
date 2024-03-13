using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Products;

namespace Mock_Project_Group9.Pages
{
    public class ProductModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public ProductModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.products != null)
            {
                Product = await _context.products
                .Include(p => p.Category).ToListAsync();
            }
        }
    }
}
