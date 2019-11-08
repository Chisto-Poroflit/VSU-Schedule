using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class TeacherSubject
    {
        //public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int SubjectId { get; set; }
        [Display(Name = "Предметы")]
        public Subject Subject { get; set; }

        public List<Couple> Couples { get; set; }
    }
}
