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

namespace VSU_Schedule.Areas.Rooms.Pages
{
    
    public class DeleteModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public DeleteModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.FirstOrDefaultAsync(m => m.Number == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.FindAsync(id);

            if (Room != null)
            {
                _context.Rooms.Remove(Room);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
