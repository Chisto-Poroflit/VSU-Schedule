using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace VSU_Schedule.Areas.Specializations.Pages
{
    
    public class EditModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public EditModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
            Specializations = _context.Specializations.ToList();
        }

        [BindProperty]
        public Specialization Specialization { get; set; }
        public IList<Specialization> Specializations { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var parentSpec = await _context.Specializations.FirstOrDefaultAsync(x => x.Id == Specialization.ParentSpec.Id);
            //Specialization.ParentSpec = parentSpec;

            var local = _context.Set<Specialization>()
    .Local
    .FirstOrDefault(entry => entry.Id.Equals(Specialization.Id));

            // check if local is not null 
            if (local!=null) // I'm using a extension method
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(Specialization).State = EntityState.Modified;

            // save 
            _context.SaveChanges();

            //_context.Attach(Specialization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecializationExists(Specialization.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SpecializationExists(int id)
        {
            return _context.Specializations.Any(e => e.Id == id);
        }
    }
}
