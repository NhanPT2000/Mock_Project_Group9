using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Orders;

namespace Mock_Project_Group9.Pages.OrderDetails
{
    public class CreateModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public CreateModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrderId"] = new SelectList(_context.orders, "OrderId", "OrderId");
        ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "ProductName");
            return Page();
        }

        [BindProperty]
        public Models.Orders.OrderDetails OrderDetails { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            OrderDetails.OrderDetailId = Guid.NewGuid();
          if (!ModelState.IsValid || _context.orderDetails == null || OrderDetails == null)
            {
                return OnGet();
            }

            _context.orderDetails.Add(OrderDetails);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
