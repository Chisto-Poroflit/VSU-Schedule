using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class _CreateModalPartialModel : PageModel
    {
        public List<CreateModel.SubjectInput> SubjectInputs { get; set; }
        public List<Subject> Subjects { get; set; }

        public _CreateModalPartialModel(List<CreateModel.SubjectInput> subjectInputs, List<Subject> subjects)
        {
            SubjectInputs = subjectInputs;
            Subjects = subjects;
        }
        public void OnGet()
        {

        }
    }
}