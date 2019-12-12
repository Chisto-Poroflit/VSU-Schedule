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

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class EditModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public EditModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        public class SubjectInput
        {
            public int SubjectId { get; set; }
            public int QuatityOfHours { get; set; }
        }

        public List<Subject> Subjects { get; set; }
        [BindProperty]
        public CurriculumUnit CurriculumUnit { get; set; }
        [BindProperty]
        public int InputId { get; set; }
        [BindProperty]
        public List<SubjectInput> Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subjects = _context.Subjects.ToList();
            CurriculumUnit = await _context.CurriculumUnits
                .Include(c => c.Specialization)
                .Include(c => c.CurriculumSubjects)
                .ThenInclude(cs => cs.Subject).FirstOrDefaultAsync(m => m.Id == id);
            Input = new List<SubjectInput>();
            foreach(var elem in CurriculumUnit.CurriculumSubjects)
            {
                Input.Add(new SubjectInput { QuatityOfHours = elem.QuantityAll, SubjectId = elem.SubjectId });
            }

            if (CurriculumUnit == null)
            {
                return NotFound();
            }
           ViewData["SpecializationId"] = new SelectList(_context.Specializations, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteInput()
        {
            Input.Remove(Input[InputId]);
            Subjects = _context.Subjects.ToList();
            return Partial("EditModalPartial", this);
        }

        public IActionResult OnPostAddInput()
        {
            Input.Add(new SubjectInput());
            Subjects = _context.Subjects.ToList();
            return Partial("EditModalPartial", this);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //_context.Attach(CurriculumUnit).State = EntityState.Modified;
            var curUnit = _context.CurriculumUnits.FirstOrDefault(m => m.Id == CurriculumUnit.Id);
            curUnit.SemesterNumber = CurriculumUnit.SemesterNumber;
            curUnit.SpecializationId = CurriculumUnit.SpecializationId;

            _context.CurriculumUnits.Update(curUnit);

            await _context.SaveChangesAsync();

            CurriculumUnit = await _context.CurriculumUnits
                .Include(c => c.CurriculumSubjects)
                .FirstOrDefaultAsync(m => m.Id == CurriculumUnit.Id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            for(int i = 0; i < CurriculumUnit.CurriculumSubjects.Count; i++)
            {
                if(!Input.Any(m => m.SubjectId == CurriculumUnit.CurriculumSubjects[i].SubjectId))
                {
                    CurriculumUnit.CurriculumSubjects.Remove(CurriculumUnit.CurriculumSubjects[i]);
                }
            }
            foreach(var elem in Input)
            {
                if(CurriculumUnit.CurriculumSubjects.Any(m => m.SubjectId == elem.SubjectId && m.QuantityAll != elem.QuatityOfHours))
                {
                    CurriculumUnit.CurriculumSubjects.FirstOrDefault(m => m.CurriculumId == elem.SubjectId).QuantityAll = elem.QuatityOfHours;
                }
                else if(!CurriculumUnit.CurriculumSubjects.Any(m => m.SubjectId == elem.SubjectId))
                {
                    CurriculumUnit.CurriculumSubjects.Add(new CurriculumSubject { SubjectId = elem.SubjectId, QuantityAll = elem.QuatityOfHours });
                }
            }
            //_context.Attach(CurriculumUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurriculumUnitExists(CurriculumUnit.Id))
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

        private bool CurriculumUnitExists(int id)
        {
            return _context.CurriculumUnits.Any(e => e.Id == id);
        }
    }
}
