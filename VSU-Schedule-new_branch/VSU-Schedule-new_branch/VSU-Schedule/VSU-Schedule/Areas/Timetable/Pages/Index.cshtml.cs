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
            bool num = false, denum = false;

            if (Input.NumOrDenum == 0)
            {
                num = true;
                if (!_context.Couples.Any(g =>
                    g.Day == Input.Day
                    && g.Numerator == num
                    && g.ParaId == Input.ParaId
                    && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)))
                    return Page();
            }

            else
            {
                denum = true;
                if (!_context.Couples.Any(g =>
                    g.Day == Input.Day
                    && g.Denomirator == denum
                    && g.ParaId == Input.ParaId
                    && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)))
                    return Page();
            }

            Couple coup;

            if (_context.Couples.Any(g =>
                g.Day == Input.Day
                && g.Numerator == num
                && g.Denomirator == denum
                && g.ParaId == Input.ParaId
                && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)))
            {
                coup = _context.Couples.FirstOrDefault(g =>
                    g.Day == Input.Day
                    && g.Numerator == num
                    && g.Denomirator == denum
                    && g.ParaId == Input.ParaId
                    && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId));

                if (_context.CoupleGroups.Count(g => g.CoupleId == coup.Id) > 1)
                {
                    _context.CoupleGroups.Remove(_context.CoupleGroups.FirstOrDefault(g =>
                        g.CoupleId == coup.Id && g.GroupId == Input.GroupId));
                }

                else
                {
                    _context.Couples.Remove(coup);
                }
            }


            else
            {
                coup = _context.Couples.FirstOrDefault(g =>
                    g.Day == Input.Day
                    && g.Numerator == true
                    && g.Denomirator == true
                    && g.ParaId == Input.ParaId
                    && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId));

                if (_context.CoupleGroups.Count(g => g.CoupleId == coup.Id) > 1)
                {
                    _context.CoupleGroups.Remove(_context.CoupleGroups.FirstOrDefault(g =>
                        g.CoupleId == coup.Id && g.GroupId == Input.GroupId));
                }

                else if (Input.NumAndDenum)
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
            //        g.CoupleId == coup.Id && g.GroupId == Input.GroupId));
            //}

            //else
            //{
            //    _context.Couples.Remove(coup);
            //}


            //if (Input.NumAndDenum)
            //{
            //    if(_context.Couples.Any(g =>
            //    g.Day == Input.Day
            //    && g.Numerator == true
            //    && g.Denomirator == true
            //    && g.ParaId == Input.ParaId
            //    && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)))
            //    {
            //        _context.Couples.Remove(await _context.Couples.FirstOrDefaultAsync(g =>
            //            g.Day == Input.Day
            //            && g.Numerator == true
            //            && g.Denomirator == true
            //            && g.ParaId == Input.ParaId
            //            && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)));
            //    }
            //    else
            //    {                    
            //        _context.Couples.Remove(await _context.Couples.FirstOrDefaultAsync(g =>
            //            g.Day == Input.Day
            //            && g.Numerator == num
            //            && g.Denomirator == denum
            //            && g.ParaId == Input.ParaId
            //            && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)));
            //    }
            //}
            //else
            //{
            //    if (_context.Couples.Any(g =>
            //     g.Day == Input.Day
            //     && g.Numerator == num
            //     && g.Denomirator == denum
            //     && g.ParaId == Input.ParaId
            //     && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)))
            //    {
            //        _context.Couples.Remove(await _context.Couples.FirstOrDefaultAsync(g =>
            //          g.Day == Input.Day
            //          && g.Numerator == num
            //          && g.Denomirator == denum
            //          && g.ParaId == Input.ParaId
            //          && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)));
            //    }
            //    else
            //    {
            //        var coup = _context.Couples.FirstOrDefault(g =>
            //          g.Day == Input.Day
            //          && g.Numerator == true
            //          && g.Denomirator == true
            //          && g.ParaId == Input.ParaId
            //          && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId));
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

            bool num = false, denum = false;
            if (Input.NumAndDenum)
            {
                num = true;
                denum = true;
            }
            else if (!Input.NumAndDenum && Input.NumOrDenum == 0)
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
            var gr = _context.Groups.FirstOrDefault(g => g.Id == Input.GroupId);

            //var groupCouples = gr?.CoupleGroups.Where(c=>c.Couple.Day == Input.Day && c.Couple.ParaId == Input.ParaId).ToList();
            //if (groupCouples?.Count == 0)
            //{
            //    var sameCouples = _context.Couples.Where(c =>
            //        c.Day == Input.Day &&
            //        c.ParaId == Input.ParaId &&
            //        c.Numerator == num &&
            //        c.Denomirator == denum &&
            //        c.CoupleGroups.Any(cg => cg.Group.SemesterNumber == gr.SemesterNumber) &&
            //        c.RoomId == Input.RoomId);
            //    if (sameCouples.Any())
            //    {
            //        _context.CoupleGroups.Add(new CoupleGroup()
            //        {
            //            CoupleId = sameCouples.First().Id,
            //            GroupId = Input.GroupId
            //        });
            //    }
            //}


            if (_context.Couples.Any(g =>
                g.Day == Input.Day &&
                g.ParaId == Input.ParaId &&
                g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)))
            {
                var existCouples = _context.Couples
                    .Include(c => c.CoupleGroups)
                    .Where(g =>
                        g.Day == Input.Day && g.ParaId == Input.ParaId
                                           && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId)).ToList();
                if (existCouples.Any(c => c.Numerator == num && c.Denomirator == denum))
                {
                    var co = existCouples.FirstOrDefault(g => g.Denomirator == denum && g.Numerator == num);
                    var couplegroups = co.CoupleGroups.ToList();

                    if (couplegroups.Count() > 1)
                    {
                        var couplegroup = couplegroups.FirstOrDefault(g => g.GroupId == Input.GroupId);
                        _context.Remove(couplegroup);

                        var newCouple = new Couple
                        {
                            Day = Input.Day,
                            Denomirator = denum,
                            Numerator = num,
                            ParaId = Input.ParaId,
                            RoomId = Input.RoomId,
                            SubjectId = Input.SubjectId,
                            TeacherId = Input.TeacherId
                        };
                        _context.Couples.Add(newCouple);
                        _context.SaveChanges();
                        _context.CoupleGroups.Add(
                            new CoupleGroup
                            {
                                GroupId = Input.GroupId,
                                CoupleId = newCouple.Id
                            });
                        _context.SaveChanges();
                    }
                    else
                    {
                        co.Numerator = num;
                        co.Denomirator = denum;
                        co.TeacherId = Input.TeacherId;
                        co.SubjectId = Input.SubjectId;
                        co.RoomId = Input.RoomId;


                        _context.Couples.Update(co);
                        _context.SaveChanges();
                    }
                }
                else if (existCouples.Any(c => c.Numerator && c.Denomirator))
                {
                    var co = existCouples.FirstOrDefault(g => g.Denomirator && g.Numerator);
                    var couplegroups = co.CoupleGroups.ToList();
                    if (couplegroups.Count() > 1)
                    {
                        var couplegroup = couplegroups.FirstOrDefault(g => g.GroupId == Input.GroupId);
                        _context.Remove(couplegroup);

                        var newCouple = new Couple
                        {
                            Day = Input.Day,
                            Denomirator = denum,
                            Numerator = num,
                            ParaId = Input.ParaId,
                            RoomId = Input.RoomId,
                            SubjectId = Input.SubjectId,
                            TeacherId = Input.TeacherId
                        };
                        _context.Couples.Add(newCouple);
                        _context.SaveChanges();
                        _context.CoupleGroups.Add(
                            new CoupleGroup
                            {
                                GroupId = Input.GroupId,
                                CoupleId = newCouple.Id
                            });
                        _context.SaveChanges();
                    }
                    else
                    {
                        co.Numerator = !num;
                        co.Denomirator = !denum;


                        _context.Couples.Update(co);
                        var newCouple = new Couple
                        {
                            Day = Input.Day,
                            Denomirator = denum,
                            Numerator = num,
                            ParaId = Input.ParaId,
                            RoomId = Input.RoomId,
                            SubjectId = Input.SubjectId,
                            TeacherId = Input.TeacherId
                        };
                        _context.Couples.Add(newCouple);
                        _context.SaveChanges();
                        _context.CoupleGroups.Add(new CoupleGroup
                        {
                            CoupleId = newCouple.Id,
                            GroupId = Input.GroupId
                        });
                        _context.SaveChanges();
                    }
                }
                else
                {
                    var newCouple = new Couple
                    {
                        Day = Input.Day,
                        Denomirator = denum,
                        Numerator = num,
                        ParaId = Input.ParaId,
                        RoomId = Input.RoomId,
                        SubjectId = Input.SubjectId,
                        TeacherId = Input.TeacherId
                    };
                    _context.Couples.Add(newCouple);
                    _context.SaveChanges();
                    _context.CoupleGroups.Add(new CoupleGroup
                    {
                        CoupleId = newCouple.Id,
                        GroupId = Input.GroupId
                    });
                }
                //couplegroups = existCouples.CoupleGroups.ToList();

                //if (couplegroups.Count() > 1)
                //{
                //    var couplegroup = couplegroups.FirstOrDefault(g => g.GroupId == Input.GroupId);
                //    _context.Remove(couplegroup);

                //    var newCouple = new Couple
                //    {
                //        Day = Input.Day,
                //        Denomirator = denum,
                //        Numerator = num,
                //        ParaId = Input.ParaId,
                //        RoomId = Input.RoomId,
                //        SubjectId = Input.SubjectId,
                //        TeacherId = Input.TeacherId
                //    };
                //    _context.Couples.Add(newCouple);
                //    _context.SaveChanges();
                //    _context.CoupleGroups.Add(
                //        new CoupleGroup
                //        {
                //            GroupId = Input.GroupId,
                //            CoupleId = newCouple.Id
                //        });
                //    _context.SaveChanges();
                //}
                //else
                //{
                //    existCouples.Numerator = num;
                //    existCouples.Denomirator = denum;
                //    existCouples.TeacherId = Input.TeacherId;
                //    existCouples.SubjectId = Input.SubjectId;
                //    existCouples.RoomId = Input.RoomId;


                //    _context.Couples.Update(existCouples);
                //    _context.SaveChanges();
                //}
            }

            else if (_context.Couples.Any(g =>
                g.Day == Input.Day && g.Numerator == num
                                   && g.Denomirator == denum && g.ParaId == Input.ParaId &&
                                   g.CoupleGroups.Any(s => s.Group.SemesterNumber == gr.SemesterNumber)
                                   && (g.RoomId == Input.RoomId || g.TeacherId == Input.TeacherId)))
            {
                var coup = _context.Couples.FirstOrDefault(g =>
                    g.Day == Input.Day && g.Numerator == num
                                       && g.Denomirator == denum && g.ParaId == Input.ParaId &&
                                       g.CoupleGroups.Any(s => s.Group.SemesterNumber == gr.SemesterNumber)
                                       && (g.RoomId == Input.RoomId || g.TeacherId == Input.TeacherId));

                if (coup.RoomId != Input.RoomId || coup.TeacherId != Input.TeacherId ||
                    coup.SubjectId != Input.SubjectId)
                    return RedirectToPage("./Index");
                else
                {
                    _context.CoupleGroups.Add(new CoupleGroup {CoupleId = coup.Id, GroupId = Input.GroupId});
                    _context.SaveChanges();
                }
            }

            else
            {
                var couple = new Couple()
                {
                    Day = Input.Day,
                    Denomirator = denum,
                    Numerator = num,
                    ParaId = Input.ParaId,
                    RoomId = Input.RoomId,
                    SubjectId = Input.SubjectId,
                    TeacherId = Input.TeacherId
                };
                _context.Couples.Add(couple);
                _context.SaveChanges();

                var numdenumCouple = _context.Couples.FirstOrDefault(g =>
                    g.Day == Input.Day && g.Numerator && g.Denomirator && g.ParaId == Input.ParaId
                    && g.CoupleGroups.Any(s => s.GroupId == Input.GroupId));
                //if (numdenumCouple != null)
                //{
                //    numdenumCouple.Numerator = !num;
                //    numdenumCouple.Denomirator = !denum;
                //    _context.Couples.Update(numdenumCouple);
                //}

                var couplegroup = new CoupleGroup()
                {
                    GroupId = Input.GroupId,
                    CoupleId = couple.Id
                };
                _context.CoupleGroups.Add(couplegroup);

                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}