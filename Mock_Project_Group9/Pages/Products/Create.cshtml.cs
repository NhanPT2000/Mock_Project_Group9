using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Products;

namespace Mock_Project_Group9.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;
        private IHostEnvironment _enviroment;


        public CreateModel(Mock_Project_Group9.Database.WebDBContext context, IHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile? FileUpload { get; set; }
        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Product.ProductId = Guid.NewGuid();
            Task image = GetImage();
          if (!ModelState.IsValid || _context.products == null || Product == null)
            {
                return Page();
            }
            _context.products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task GetImage()
        {
            {
                if (FileUpload != null)
                {
                        var file = Path.Combine(_enviroment.ContentRootPath, "images", FileUpload.FileName);
                        using (var fileStream = new FileStream(file, FileMode.Create))
                        {
                            await FileUpload.CopyToAsync(fileStream);
                        }
                        Product.Images = file.ToString();
                    }
                else
                {
                    Product.Images = "images/No_Image_Available.jpg";
                }
            }
            }

        }
    }
