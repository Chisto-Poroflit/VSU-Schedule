using System;
using System.Collections.Generic;
using System.Text;

namespace DbLibrary.Models.Entity
{
    public class SemesterSubject
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}
