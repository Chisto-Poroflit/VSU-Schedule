using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class TeacherSubject
    {
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public int SubjectId { get; set; }
        [Display(Name = "Предметы")]
        public Subject Subject { get; set; }
    }
}
