using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public DetailsModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        public CurriculumUnit CurriculumUnit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurriculumUnit = await _context.CurriculumUnits
                .Include(c => c.Specialization).FirstOrDefaultAsync(m => m.Id == id);

            if (CurriculumUnit == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
