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

namespace VSU_Schedule.Areas.Teachers.Pages
{
    
    public class EditModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public EditModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
            Subjects = _context.Subjects.ToList();
        }

        [BindProperty]
        public Teacher Teacher { get; set; }
        [BindProperty]
        public IList<Subject> Subjects { get; set; }
        [BindProperty]
        public IList<TeacherSubject> TeacherSubjects { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            TeacherSubjects = await _context.TeacherSubject
                            .Where(ts => ts.TeacherId == id).ToListAsync();
            for(int i = 0; i < Subjects.Count; i++)
            {
                if(TeacherSubjects.Any(m => m.SubjectId == Subjects[i].Id))
                {
                    Subjects[i].ForTeacher = true;
                }
            }

            if (Teacher == null)
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
            TeacherSubjects = await _context.TeacherSubject
                            .Where(ts => ts.TeacherId == Teacher.Id).ToListAsync();

            _context.Attach(Teacher).State = EntityState.Modified;

            _context.TeacherSubject.AddRange(Subjects.Where(s => s.ForTeacher && !TeacherSubjects.Any(t => t.SubjectId == s.Id))
                .Select(q => new TeacherSubject { SubjectId = q.Id, TeacherId = Teacher.Id }));
            List<TeacherSubject> lst = new List<TeacherSubject>();
            for (int i = 0; i < Subjects.Count; i++)
            {
                if (!Subjects[i].ForTeacher && TeacherSubjects.Any(t => t.SubjectId == Subjects[i].Id))
                    lst.Add(TeacherSubjects.FirstOrDefault(t => t.TeacherId == Teacher.Id && t.SubjectId == Subjects[i].Id));
            }
            _context.TeacherSubject.RemoveRange(lst);
            //_context.TeacherSubject.RemoveRange(Subjects.Where(s => !s.ForTeacher && TeacherSubjects.Any(t => t.SubjectId == s.Id))
            //    .Select(s => TeacherSubjects.Where(t => t.SubjectId == s.Id && t.TeacherId == Teacher.Id)));
            //var lst = (IEnumerable<TeacherSubject>)TeacherSubjects
            //    .Select(t => Subjects.Where(s => s.Id != t.SubjectId && s.ForTeacher));
            //_context.TeacherSubject.RemoveRange(lst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(Teacher.Id))
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

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
