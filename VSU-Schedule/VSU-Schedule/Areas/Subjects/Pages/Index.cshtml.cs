using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;

namespace VSU_Schedule.Areas.Subjects.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public IndexModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Subject> Subject { get; set; }
        public async Task OnGetAsync()
        {
            Subject = await _context.Subjects.ToListAsync();
        }
    }
}