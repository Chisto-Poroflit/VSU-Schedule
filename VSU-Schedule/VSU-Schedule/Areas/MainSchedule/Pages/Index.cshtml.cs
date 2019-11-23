using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace VSU_Schedule.Areas.MainSchedule.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }
        public List<Couple> Couples { get; set; }
        public List<Para> Para { get; private set; }
        public List<Group> Groups { get; private set; }
        public void OnGet()
        {
            Couples = _context.Couples
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .Include(g => g.CoupleGroups)
                .ThenInclude(g => g.Group)
                .ToList();
            Para = _context.Para.ToList();
            Groups = _context.Groups.ToList();
        }
    }
}