﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace VSU_Schedule.Areas.Specializations.Pages
{
    //[Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public IndexModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Specialization> Specialization { get;set; }

        public async Task OnGetAsync()
        {
            Specialization = await _context.Specializations.ToListAsync();
        }
    }
}
