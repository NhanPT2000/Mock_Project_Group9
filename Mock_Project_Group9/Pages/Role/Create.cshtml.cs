using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Pages.Role
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
            return Page();
        }

        [BindProperty]
        public Models.Users.Role Role { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Role.RoleId = Guid.NewGuid();
          if (!ModelState.IsValid || _context.roles == null || Role == null)
            {
                return OnGet();
            }

            _context.roles.Add(Role);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
