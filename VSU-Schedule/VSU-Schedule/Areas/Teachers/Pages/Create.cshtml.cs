using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace VSU_Schedule.Areas.Teachers.Pages
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public CreateModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
            Subjects = _context.Subjects.ToList();
            //TeacherSubjects = _context.TeacherSubject.ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Teacher Teacher { get; set; }
        [BindProperty]
        public IList<Subject> Subjects { get; set; }
        //public IList<TeacherSubject> TeacherSubjects { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Teachers.Add(Teacher);
            await _context.SaveChangesAsync();
            //Teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.FullName == Teacher.FullName );
            //foreach (var elem in Subjects)
            //{
            //    if(elem.ForTeacher)
            //        TeacherSubjects.Add(new TeacherSubject() { SubjectId = elem.Id, TeacherId = Teacher.Id });
            //}
           // TeacherSubjects.AddRange(Subjects.Where(s => s.ForTeacher).Select(s => new TeacherSubject() { SubjectId = s.Id, TeacherId = Teacher.Id }));
            _context.TeacherSubject.AddRange(Subjects.Where(s => s.ForTeacher).Select(s => new TeacherSubject() { SubjectId = s.Id, TeacherId = Teacher.Id }));
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}