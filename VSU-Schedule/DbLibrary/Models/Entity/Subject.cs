
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class Subject
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Range(1,12, ErrorMessage = "Семестр должен быть в диапазоне от 1 до 12")]
        [Display(Name = "Номер Семестра")]
        public int Semester { get; set; }

        public List<Couple> Couples { get; set; }
        public CurriculumUnit CurriculumUnit { get; set; }
    }
}
