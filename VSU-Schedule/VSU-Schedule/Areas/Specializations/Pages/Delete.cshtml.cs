﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace VSU_Schedule.Areas.Specializations.Pages
{
    //[Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public DeleteModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Specialization Specialization { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specialization = await _context.Specializations.FirstOrDefaultAsync(m => m.Id == id);

            if (Specialization == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specialization = await _context.Specializations.FindAsync(id);

            if (Specialization != null)
            {
                _context.Specializations.Remove(Specialization);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
