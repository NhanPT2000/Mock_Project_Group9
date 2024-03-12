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
    public class DeleteModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public DeleteModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Orders.OrderDetails OrderDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.orderDetails == null)
            {
                return NotFound();
            }

            var orderdetails = await _context.orderDetails.FirstOrDefaultAsync(m => m.OrderDetailId == id);

            if (orderdetails == null)
            {
                return NotFound();
            }
            else 
            {
                OrderDetails = orderdetails;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.orderDetails == null)
            {
                return NotFound();
            }
            var orderdetails = await _context.orderDetails.FindAsync(id);

            if (orderdetails != null)
            {
                OrderDetails = orderdetails;
                _context.orderDetails.Remove(OrderDetails);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
