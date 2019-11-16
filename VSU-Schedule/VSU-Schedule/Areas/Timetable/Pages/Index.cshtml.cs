using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace VSU_Schedule.Areas.Timetable.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }

        public List<Para> Para { get; private set; }
        public List<Group> Groups { get; private set; }
        public List<TeacherSubject> TeacherSubjects { get; private set; }

        public List<Room> Rooms { get; private set; }
        public List<Subject> Subjects { get; private set; }

        public void OnGet()
        {
            Para = _context.Para.ToList();
            Groups = _context.Groups.ToList();
            TeacherSubjects = _context.TeacherSubject
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .ToList();
            Rooms = _context.Rooms.ToList();
            Subjects = _context.Subjects.ToList();
        }

        public JsonResult OnGetTeachersSubject(string subjectName)
        {
            return new JsonResult(_context.TeacherSubject.Where(s=>s.Subject.Name == subjectName).Select(s=>s.Teacher).ToList());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Customers.Add(Customer);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}