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

        public List<Couple> Couples { get; set; }
        public List<CoupleGroup> CoupleGroups { get; set; }
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
            Couples = _context.Couples
                .Include(g => g.Teacher)
                .Include(g => g.Subject)
                .Include(g => g.CoupleGroups)
                .ThenInclude(g => g.Group)
                .ToList();
            CoupleGroups = _context.CoupleGroups.ToList();
        }

        public JsonResult OnGetTeachersSubject(string subjectName)
        {
            return new JsonResult(_context.TeacherSubject.Where(s=>s.Subject.Name == subjectName).Select(s=>s.Teacher).ToList());
        }

        public class InputModel
        {
            public int GroupId { get; set; }
            public int SubjectId { get; set; }
            public string RoomId { get; set; }
            public int TeacherId { get; set; }
            public int Day { get; set; }
            public int ParaId { get; set; }
            public int NumOrDenum { get; set; }
            public bool NumAndDenum { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var input = Input;
            bool num = false, denum = false;
            if (input.NumAndDenum)
            {
                num = true;
                denum = true;
            }
            else if(!input.NumAndDenum && input.NumOrDenum == 0)
            {
                num = true;
            }
            else
            {
                denum = true;
            }
            //if(Couples.Any(g => g.Day == input.Day && g.RoomId == input.RoomId && g.ParaId == input.ParaId && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
            //{

            //}

            if(Couples.Any(g => g.Day == input.Day && g.Numerator == num && g.Denomirator == denum && g.ParaId == input.ParaId && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
            {
                var couple = _context.Couples.Where(g => g.Day == input.Day && g.Numerator == num && g.Denomirator == denum && g.ParaId == input.ParaId 
                && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)).FirstOrDefault();
                var couplegroups = _context.CoupleGroups.Where(g => g.CoupleId == couple.Id).ToList();

                if(couplegroups.Count() > 1)
                {
                    var group = couplegroups.Where(g => g.GroupId == input.GroupId);
                    _context.Remove(group);
                    _context.Couples.Add(
                        new Couple { Day = input.Day, Denomirator = denum, Numerator = num, ParaId = input.ParaId, RoomId = input.RoomId, SubjectId = input.SubjectId, TeacherId = input.TeacherId }
                        );
                    _context.SaveChanges();
                    _context.CoupleGroups.Add(
                        new CoupleGroup
                        {
                            GroupId = input.GroupId,
                            Couple =
                        _context.Couples.Where(g => g.Day == input.Day && g.Denomirator == denum && g.Numerator == num && g.ParaId == input.ParaId && g.RoomId == input.RoomId).FirstOrDefault()
                        });
                    _context.SaveChanges();
                }
                else
                {
                    couple.Numerator = num;
                    couple.Denomirator = denum;
                    couple.TeacherId = input.TeacherId;
                    couple.SubjectId = input.SubjectId;
                    couple.RoomId = input.RoomId;
                    _context.Couples.Update(couple);
                    _context.SaveChanges();
                }
            }
            else
            {
                _context.Couples.Add(
                        new Couple { Day = input.Day, Denomirator = denum, Numerator = num, ParaId = input.ParaId, RoomId = input.RoomId, SubjectId = input.SubjectId, TeacherId = input.TeacherId }
                        );
                _context.SaveChanges();
                _context.CoupleGroups.Add(
                    new CoupleGroup
                    {
                        GroupId = input.GroupId,
                        Couple =
                    _context.Couples.Where(g => g.Day == input.Day && g.Denomirator == true && g.Numerator == true && g.ParaId == input.ParaId && g.RoomId == input.RoomId).FirstOrDefault()
                    });
                _context.SaveChanges();
            }

            //_context.Customers.Add(Customer);
            //await _context.SaveChangesAsync();

            //return Page();
            return RedirectToPage("./Index");
        }
    }
}