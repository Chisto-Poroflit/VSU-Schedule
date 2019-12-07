using System;
using System.Collections.Generic;
using System.Text;

namespace DbLibrary.Models.Entity
{
    public class CurriculumSubject
    {
        public int Id { get; set; }
        public int QuantityAll { get; set; }

        public int CurriculumId { get; set; }
        public CurriculumUnit CurriculumUnit { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
