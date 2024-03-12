using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Products;

namespace Mock_Project_Group9.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;
        private IHostEnvironment _enviroment;

        public EditModel(Mock_Project_Group9.Database.WebDBContext context, IHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile? FileUpload { get; set; }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product =  await _context.products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (FileUpload != null)
            {
                var file = Path.Combine(_enviroment.ContentRootPath, "Pages\\images", FileUpload.FileName);
                Console.WriteLine(FileUpload.FileName);
                Product.Images = FileUpload.FileName;
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);
                }
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
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

        private bool ProductExists(Guid id)
        {
          return (_context.products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
