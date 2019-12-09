using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DbLibrary;
using DbLibrary.Models.Entity;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class CreateModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public CreateModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        public class SubjectInput
        {
            public int SubjectId { get; set; }
            public int QuatityOfHours { get; set; }
        }
        public IActionResult OnGet()
        {
            Subjects = _context.Subjects.ToList();
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "Id", "Name");
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public CurriculumUnit CurriculumUnit { get; set; }
        public List<Subject> Subjects { get; set; }
        [BindProperty]
        public SubjectInput Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CurriculumUnits.Add(CurriculumUnit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}