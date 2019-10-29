using System;
using System.Collections.Generic;
using System.Text;

namespace DbLibrary.Models.Entity
{
    public class Semester
    {
        public int Number { get; set; }
        public int Course { get; set; }
        public List<SemesterSubject> SemesterSubjects { get; set; }
    }
}
