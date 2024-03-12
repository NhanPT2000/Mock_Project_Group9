using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Orders;

namespace Mock_Project_Group9.Pages.OrderDetails
{
    public class EditModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public EditModel(Mock_Project_Group9.Database.WebDBContext context)
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

            var orderdetails =  await _context.orderDetails.FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderdetails == null)
            {
                return NotFound();
            }
            OrderDetails = orderdetails;
           ViewData["OrderId"] = new SelectList(_context.orders, "OrderId", "OrderId");
           ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "ProductName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(OrderDetails.OrderDetailId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderDetailsExists(Guid id)
        {
          return (_context.orderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
