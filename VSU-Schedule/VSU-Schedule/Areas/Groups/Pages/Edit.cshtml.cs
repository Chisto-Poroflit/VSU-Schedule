﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbLibrary;
using DbLibrary.Models.Entity;

namespace VSU_Schedule.Areas.Groups.Pages
{
    public class EditModel : PageModel
    {
        private readonly DbLibrary.ApplicationContext _context;

        public EditModel(DbLibrary.ApplicationContext context)
        {
            _context = context;
            Specializations = _context.Specializations.ToList();
        }

        [BindProperty]
        public Group Group { get; set; }
        public IList<Specialization> Specializations { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Group == null)
            {
                return NotFound();
            }
           ViewData["SpecializatioId"] = new SelectList(_context.Specializations, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(Group.Id))
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

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
