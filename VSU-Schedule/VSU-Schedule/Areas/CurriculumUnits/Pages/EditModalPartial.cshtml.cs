using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class EditModalPartialModel : PageModel
    {
        public List<EditModel.SubjectInput> SubjectInputs { get; set; }
        public List<Subject> Subjects { get; set; }
        public EditModalPartialModel(List<EditModel.SubjectInput> subjectInputs, List<Subject> subjects)
        {
            SubjectInputs = subjectInputs;
            Subjects = subjects;
        }

        public void OnGet()
        {

        }
    }
}