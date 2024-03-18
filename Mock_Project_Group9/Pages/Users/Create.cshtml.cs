using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Pages.Users
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
        ViewData["RoleId"] = new SelectList(_context.roles, "RoleId", "RoleName");
        ViewData["UserId"] = new SelectList(_context.userDetails, "UserId", "UserName");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["RoleId"] = new SelectList(_context.roles, "RoleId", "RoleName");
            ViewData["UserId"] = new SelectList(_context.userDetails, "UserId", "UserName");
            User.UserId = Guid.NewGuid();
          if (!ModelState.IsValid || _context.users == null || User == null)
            {
                return Page();
            }

            _context.users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
