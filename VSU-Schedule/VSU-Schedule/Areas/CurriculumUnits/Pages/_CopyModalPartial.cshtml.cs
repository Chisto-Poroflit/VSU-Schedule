using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbLibrary.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VSU_Schedule.Areas.CurriculumUnits.Pages
{
    public class _CopyModalPartialModel : PageModel
    {
        public List<CreateModel.SubjectInput> SubjectInputs { get; set; }
        public List<Subject> Subjects { get; set; }
        public CreateModel.CopySpecInput CopySpecInput { get; set; }

        public _CopyModalPartialModel(List<CreateModel.SubjectInput> subjectInputs, List<Subject> subjects, CreateModel.CopySpecInput csi, List<Specialization> specializations)
        {
            SubjectInputs = subjectInputs;
            Subjects = subjects;
            CopySpecInput = csi;
            ViewData["SpecializationId"] = new SelectList(specializations, "Id", "Name");
        }
        public void OnGet()
        {

        }
    }
}