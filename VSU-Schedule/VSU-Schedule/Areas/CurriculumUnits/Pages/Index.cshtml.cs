using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public IndexModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        public IList<CurriculumUnit> CurriculumUnit { get;set; }

        public async Task OnGetAsync()
        {
            CurriculumUnit = await _context.CurriculumUnits
                .Include(c => c.Specialization).ToListAsync();
        }
    }
}
