using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Pages.UserDetails
{
    public class IndexModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public IndexModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        public IList<Models.Users.UserDetails> UserDetails { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.userDetails != null)
            {
                UserDetails = await _context.userDetails.ToListAsync();
            }
        }
    }
}
