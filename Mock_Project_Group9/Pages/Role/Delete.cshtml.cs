using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Pages.Role
{
    public class DeleteModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public DeleteModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Users.Role Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.roles == null)
            {
                return NotFound();
            }

            var role = await _context.roles.FirstOrDefaultAsync(m => m.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }
            else 
            {
                Role = role;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.roles == null)
            {
                return NotFound();
            }
            var role = await _context.roles.FindAsync(id);

            if (role != null)
            {
                Role = role;
                _context.roles.Remove(Role);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
