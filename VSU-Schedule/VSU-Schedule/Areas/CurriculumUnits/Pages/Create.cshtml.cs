using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class CreateModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public CreateModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        public class CopySpecInput
        {
            public int SpecId { get; set; }
            public int SemesterNumber { get; set; }
        }

        public class SubjectInput
        {
            public int SubjectId { get; set; }
            public int QuatityOfHours { get; set; }
        }
        public IActionResult OnGet()
        {
            Subjects = _context.Subjects.ToList();
            Input = new List<SubjectInput>() { new SubjectInput(), new SubjectInput() };
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "Id", "Name");
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostCopyCurriculum()
        {
            var curriculumUnit = _context.CurriculumUnits
                .Include(c => c.Specialization)
                .Include(c => c.CurriculumSubjects)
                .ThenInclude(cs => cs.Subject)
                .FirstOrDefault(m => m.SemesterNumber == SpecInput.SemesterNumber && m.SpecializationId == SpecInput.SpecId);

            Input = new List<SubjectInput>();
            foreach (var elem in curriculumUnit.CurriculumSubjects)
            {
                Input.Add(new SubjectInput { QuatityOfHours = elem.QuantityAll, SubjectId = elem.SubjectId });
            }

            return Page();
        }

        [BindProperty]
        public CurriculumUnit CurriculumUnit { get; set; }
        public List<Subject> Subjects { get; set; }
        [BindProperty] public List<SubjectInput> Input { get; set; }
        [BindProperty] public int InputId { get; set; }
        [BindProperty] public CopySpecInput SpecInput { get; set; }

        public async Task<IActionResult> OnPostDeleteInput()
        {
            Input.Remove(Input[InputId]);
            Subjects = _context.Subjects.ToList();
            return Partial("_CreateModalPartial", this);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CurriculumUnits.Add(CurriculumUnit);
            await _context.SaveChangesAsync();
            List<CurriculumSubject> curSubjts = new List<CurriculumSubject>();
            foreach (var elem in Input)
            {
                if (elem.SubjectId != 0 && elem.QuatityOfHours != 0)
                {
                    curSubjts.Add(new CurriculumSubject {CurriculumId = CurriculumUnit.Id, SubjectId = elem.SubjectId, QuantityAll = elem.QuatityOfHours });
                }
            }
            _context.CurriculumSubjects.AddRange(curSubjts);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostAddInput()
        {
            Input.Add(new SubjectInput());
            Subjects = _context.Subjects.ToList();
            return Partial("_CreateModalPartial", this);
        }
    }
}