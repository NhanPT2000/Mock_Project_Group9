﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly Mock_Project_Group9.Database.WebDBContext _context;

        public DeleteModel(Mock_Project_Group9.Database.WebDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Users.UserDetails UserDetails { get; set; } = default!;

        public Guid _id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.userDetails == null)
            {
                return NotFound();
            }

            var userdetails = await _context.userDetails.FirstOrDefaultAsync(m => m.UserId == id);

            if (userdetails == null)
            {
                return NotFound();
            }
            else 
            {
                UserDetails = userdetails;
            }
            _id = id ?? Guid.Empty;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.userDetails == null)
            {
                return NotFound();
            }
            var userdetails = await _context.userDetails.FindAsync(id);

            if (userdetails != null)
            {
                UserDetails = userdetails;
                _context.userDetails.Remove(UserDetails);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Users/Index");
        }
    }
}
