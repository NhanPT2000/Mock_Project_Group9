using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Orders;

namespace Mock_Project_Group9.Pages.OrderDetails
{
    public class IndexModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public IndexModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        public IList<Models.Orders.OrderDetails> OrderDetails { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.orderDetails != null)
            {
                OrderDetails = await _context.orderDetails
                .Include(o => o.Order)
                .Include(o => o.Product).ToListAsync();
            }
        }
    }
}
