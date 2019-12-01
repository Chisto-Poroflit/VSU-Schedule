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

        public JsonResult OnGetTeachersSubject(int subjectId)
        {
            return new JsonResult(_context.TeacherSubject.Where(s => s.Subject.Id == subjectId)
                .Select(s => s.Teacher).ToList());
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var input = Input;
            bool num = false, denum = false;

            if (input.NumOrDenum == 0)
            {
                num = true;
                if (!_context.Couples.Any(g =>
                 g.Day == input.Day
                 && g.Numerator == num
                 && g.ParaId == input.ParaId
                 && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
                    return Page();
            }

            else
            {
                denum = true;
                if (!_context.Couples.Any(g =>
                 g.Day == input.Day
                 && g.Denomirator == denum
                 && g.ParaId == input.ParaId
                 && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
                    return Page();
            }
            Couple coup;

            if (_context.Couples.Any(g =>
                 g.Day == input.Day
                 && g.Numerator == num
                 && g.Denomirator == denum
                 && g.ParaId == input.ParaId
                 && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
            {
                coup = _context.Couples.FirstOrDefault(g =>
                g.Day == input.Day
                && g.Numerator == num
                && g.Denomirator == denum
                && g.ParaId == input.ParaId
                && g.CoupleGroups.Any(s => s.GroupId == input.GroupId));

                if (_context.CoupleGroups.Count(g => g.CoupleId == coup.Id) > 1)
                {
                    _context.CoupleGroups.Remove(_context.CoupleGroups.FirstOrDefault(g =>
                        g.CoupleId == coup.Id && g.GroupId == input.GroupId));
                }

                else
                {
                    _context.Couples.Remove(coup);
                }
            }


            else
            {
                coup = _context.Couples.FirstOrDefault(g =>
                g.Day == input.Day
                && g.Numerator == true
                && g.Denomirator == true
                && g.ParaId == input.ParaId
                && g.CoupleGroups.Any(s => s.GroupId == input.GroupId));

                if (_context.CoupleGroups.Count(g => g.CoupleId == coup.Id) > 1)
                {
                    _context.CoupleGroups.Remove(_context.CoupleGroups.FirstOrDefault(g =>
                        g.CoupleId == coup.Id && g.GroupId == input.GroupId));
                }

                else if (input.NumAndDenum)
                {
                    _context.Couples.Remove(coup);
                }

                else
                {
                    coup.Numerator = !num;
                    coup.Denomirator = !denum;
                    _context.Couples.Update(coup);
                }
            }
            //if (coup.CoupleGroups.Count() > 1)
            //{
            //    _context.CoupleGroups.Remove(_context.CoupleGroups.FirstOrDefault(g =>
            //        g.CoupleId == coup.Id && g.GroupId == input.GroupId));
            //}

            //else
            //{
            //    _context.Couples.Remove(coup);
            //}


            //if (input.NumAndDenum)
            //{
            //    if(_context.Couples.Any(g =>
            //    g.Day == input.Day
            //    && g.Numerator == true
            //    && g.Denomirator == true
            //    && g.ParaId == input.ParaId
            //    && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
            //    {
            //        _context.Couples.Remove(await _context.Couples.FirstOrDefaultAsync(g =>
            //            g.Day == input.Day
            //            && g.Numerator == true
            //            && g.Denomirator == true
            //            && g.ParaId == input.ParaId
            //            && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)));
            //    }
            //    else
            //    {                    
            //        _context.Couples.Remove(await _context.Couples.FirstOrDefaultAsync(g =>
            //            g.Day == input.Day
            //            && g.Numerator == num
            //            && g.Denomirator == denum
            //            && g.ParaId == input.ParaId
            //            && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)));
            //    }
            //}
            //else
            //{
            //    if (_context.Couples.Any(g =>
            //     g.Day == input.Day
            //     && g.Numerator == num
            //     && g.Denomirator == denum
            //     && g.ParaId == input.ParaId
            //     && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
            //    {
            //        _context.Couples.Remove(await _context.Couples.FirstOrDefaultAsync(g =>
            //          g.Day == input.Day
            //          && g.Numerator == num
            //          && g.Denomirator == denum
            //          && g.ParaId == input.ParaId
            //          && g.CoupleGroups.Any(s => s.GroupId == input.GroupId)));
            //    }
            //    else
            //    {
            //        var coup = _context.Couples.FirstOrDefault(g =>
            //          g.Day == input.Day
            //          && g.Numerator == true
            //          && g.Denomirator == true
            //          && g.ParaId == input.ParaId
            //          && g.CoupleGroups.Any(s => s.GroupId == input.GroupId));
            //        coup.Denomirator = !denum;
            //        coup.Numerator = !num;
            //        _context.Couples.Update(coup);
            //    }
            //}

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
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

        [BindProperty] public InputModel Input { get; set; }

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
            else if (!input.NumAndDenum && input.NumOrDenum == 0)
            {
                num = true;
            }
            else
            {
                denum = true;
            }

            //Couples = _context.Couples
            //    .Include(g => g.Teacher)
            //    .Include(g => g.Subject)
            //    .Include(g => g.CoupleGroups)
            //    .ThenInclude(g => g.Group)
            //    .ToList();
            var gr = _context.Groups.FirstOrDefault(g => g.Id == input.GroupId);

            if (_context.Couples.Any(g =>
                g.Day == input.Day && g.Numerator == num && g.Denomirator == denum && g.ParaId == input.ParaId &&
                g.CoupleGroups.Any(s => s.GroupId == input.GroupId)))
            {
                var existCouple = _context.Couples.FirstOrDefault(g =>
                    g.Day == input.Day && g.Numerator == num && g.Denomirator == denum && g.ParaId == input.ParaId
                    && g.CoupleGroups.Any(s => s.GroupId == input.GroupId));

                var couplegroups = existCouple.CoupleGroups.ToList();

                if (couplegroups.Count() > 1)
                {
                    var couplegroup = couplegroups.FirstOrDefault(g => g.GroupId == input.GroupId);
                    _context.Remove(couplegroup);

                    var newCouple = new Couple
                    {
                        Day = input.Day,
                        Denomirator = denum,
                        Numerator = num,
                        ParaId = input.ParaId,
                        RoomId = input.RoomId,
                        SubjectId = input.SubjectId,
                        TeacherId = input.TeacherId
                    };
                    _context.Couples.Add(newCouple);
                    _context.SaveChanges();
                    _context.CoupleGroups.Add(
                        new CoupleGroup
                        {
                            GroupId = input.GroupId,
                            CoupleId = newCouple.Id
                        });
                    _context.SaveChanges();
                }
                else
                {
                    existCouple.Numerator = num;
                    existCouple.Denomirator = denum;
                    existCouple.TeacherId = input.TeacherId;
                    existCouple.SubjectId = input.SubjectId;
                    existCouple.RoomId = input.RoomId;



                    _context.Couples.Update(existCouple);
                    _context.SaveChanges();
                }
            }

            else if (_context.Couples.Any(g =>
                 g.Day == input.Day && g.Numerator == num
                 && g.Denomirator == denum && g.ParaId == input.ParaId &&
                 g.CoupleGroups.Any(s => s.Group.SemesterNumber == gr.SemesterNumber)
                 && (g.RoomId == input.RoomId || g.TeacherId == input.TeacherId)))
            {
                var coup = _context.Couples.FirstOrDefault(g =>
                g.Day == input.Day && g.Numerator == num
                && g.Denomirator == denum && g.ParaId == input.ParaId &&
                g.CoupleGroups.Any(s => s.Group.SemesterNumber == gr.SemesterNumber)
                && (g.RoomId == input.RoomId || g.TeacherId == input.TeacherId));

                if (coup.RoomId != input.RoomId || coup.TeacherId != input.TeacherId || coup.SubjectId != input.SubjectId)
                    return RedirectToPage("./Index");
                else
                {
                    _context.CoupleGroups.Add(new CoupleGroup { CoupleId = coup.Id, GroupId = input.GroupId });
                    _context.SaveChanges();
                }
            }

            else
            {
                var couple = new Couple()
                {
                    Day = input.Day,
                    Denomirator = denum,
                    Numerator = num,
                    ParaId = input.ParaId,
                    RoomId = input.RoomId,
                    SubjectId = input.SubjectId,
                    TeacherId = input.TeacherId
                };
                _context.Couples.Add(couple);
                _context.SaveChanges();

                var numdenumCouple = _context.Couples.FirstOrDefault(g =>
                    g.Day == input.Day && g.Numerator && g.Denomirator && g.ParaId == input.ParaId
                    && g.CoupleGroups.Any(s => s.GroupId == input.GroupId));
                //if (numdenumCouple != null)
                //{
                //    numdenumCouple.Numerator = !num;
                //    numdenumCouple.Denomirator = !denum;
                //    _context.Couples.Update(numdenumCouple);
                //}

                var couplegroup = new CoupleGroup()
                {
                    GroupId = input.GroupId,
                    CoupleId = couple.Id
                };
                _context.CoupleGroups.Add(couplegroup);

                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}