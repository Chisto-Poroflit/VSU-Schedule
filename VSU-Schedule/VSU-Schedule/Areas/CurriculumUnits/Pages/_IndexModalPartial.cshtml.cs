using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class _IndexModalPartialModel : PageModel
    {
        public CurriculumUnit Unit { get; set; }
        public _IndexModalPartialModel(CurriculumUnit cUnit)
        {
            Unit = cUnit;
        }
        public void OnGet()
        {

        }
    }
}