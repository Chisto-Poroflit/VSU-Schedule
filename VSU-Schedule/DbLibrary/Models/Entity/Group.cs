using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class Group
    {
        public int Id { get; set; }

        [Range(1, 12, ErrorMessage = "Семестр должен быть в диапазоне от 1 до 12")]
        [Display(Name = "Номер семестра")]
        public int SemesterNumber { get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "Номер группы должен быть положительным числом")]
        [Display(Name = "Номер группы")]
        public int GroupNumber { get; set; }

        [Range(1, 2, ErrorMessage = "Номер подгруппы должен быть в диапазоне от 1 до 2")]
        [Display(Name = "Номер подгруппы")]
        public int SubgroupNumber { get; set; }

        public int SpecializatioId { get; set; }
        public Specialization Specialization { get; set; }

        public List<CoupleGroup> CoupleGroups { get; set; }
    }
}
