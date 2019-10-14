
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class Room
    {
        [Display(Name ="Номер")]
        public string Number { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Вместимость является целым положительным числом")]
        [Display(Name ="Вместимость")]
        public int Capacity { get; set; }

        [Display(Name = "Лекционная")]
        public bool IsLecture { get; set; }

        [Display(Name = "Практическая")]
        public bool IsPractice { get; set; }

        [Display(Name = "Лаборатория")]
        public bool IsLaboratory { get; set; }

        [Display(Name = "Специальная")]
        public bool IsSpecial { get; set; }

        public List<Couple> Couples { get; set; }
    }
}
