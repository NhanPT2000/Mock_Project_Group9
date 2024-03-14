using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Pages.UserDetails
{
    public class EditModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public EditModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Users.UserDetails UserDetails { get; set; } = default!;

        public Guid _id {  get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.userDetails == null)
            {
                return NotFound();
            }

            var userdetails =  await _context.userDetails.FirstOrDefaultAsync(m => m.UserId == id);
            if (userdetails == null)
            {
                return NotFound();
            }
            UserDetails = userdetails;
            _id = id ?? Guid.Empty;
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

            _context.Attach(UserDetails).State = EntityState.Modified;

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
