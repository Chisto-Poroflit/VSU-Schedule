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
    public class DeleteModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public DeleteModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurriculumUnit = await _context.CurriculumUnits.FindAsync(id);

            if (CurriculumUnit != null)
            {
                _context.CurriculumUnits.Remove(CurriculumUnit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
