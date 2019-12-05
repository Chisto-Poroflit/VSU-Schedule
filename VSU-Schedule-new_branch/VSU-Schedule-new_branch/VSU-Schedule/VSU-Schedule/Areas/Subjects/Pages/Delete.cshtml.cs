using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace VSU_Schedule.Areas.Subjects.Pages
{
    
    public class DeleteModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public DeleteModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Subject Subject { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject = await _context.Subjects.FirstOrDefaultAsync(m => m.Id == id);

            if (Subject == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject = await _context.Subjects.FindAsync(id);

            if (Subject != null)
            {
                _context.Subjects.Remove(Subject);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}