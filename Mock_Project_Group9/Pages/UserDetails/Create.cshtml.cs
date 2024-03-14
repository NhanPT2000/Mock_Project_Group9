using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Text;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Pages.UserDetails
{
    public class CreateModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public CreateModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public Models.Users.UserDetails UserDetails { get; set; } = default!;

        public Guid _id { get; set; }
        public string _userName {  get; set; }
        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            _id = user.UserId;
            _userName = user.UserName;
            return Page();
        }
 

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            UserDetails.UserDetailsId = Guid.NewGuid();
          if (!ModelState.IsValid || _context.userDetails == null || UserDetails == null)
            {
                return Page();
            }

            _context.userDetails.Add(UserDetails);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailsExists(UserDetails.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect("./Details?id=" + UserDetails.UserDetailsId);
        }

        private bool UserDetailsExists(Guid id)
        {
            return (_context.userDetails?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
