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
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public DetailsModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

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
    }
}
