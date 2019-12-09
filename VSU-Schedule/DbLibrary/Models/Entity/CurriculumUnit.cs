
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class CurriculumUnit
    {
        public int Id { get; set; }

        [Range(1, 12, ErrorMessage = "Семестр должен быть в диапазоне от 1 до 12")]
        [Display(Name = "Номер Семестра")]
        public int SemesterNumber { get; set; }

        //[Display(Name="Лекционных часов")]
        //public int QuantityLect { get; set; }

        //[Display(Name = "Практических часов")]
        //public int QuantityPrac { get; set; }

        //[Display(Name = "Лабораторных часов")]
        //public int QuantityLab { get; set; }

        public int SpecializationId { get; set; }

        [Display(Name = "Специальность")]
        public Specialization Specialization { get; set; }
        public List<CurriculumSubject> CurriculumSubjects { get; set; }
        //public List<Group> Groups { get; set; }
    }
}
