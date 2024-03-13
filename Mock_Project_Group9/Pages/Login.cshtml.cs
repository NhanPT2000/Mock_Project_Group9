using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;
using Mock_Project_Group9.Models.Users;

namespace Mock_Project_Group9.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public LoginModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.users != null)
            {
                User = await _context.users
                .Include(u => u.Role)
                .Include(u => u.UserDetails).ToListAsync();
            }
        }
    }
}
