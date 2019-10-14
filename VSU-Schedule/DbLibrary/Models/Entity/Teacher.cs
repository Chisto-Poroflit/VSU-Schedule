
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class Teacher
    {
        public int Id { get; set; }

        [Display(Name = "Ф.И.О")]
        [Required(ErrorMessage = "Поле \"Ф.И.О\" обязательно для заполнения")]
        public string FullName { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        public List<Couple> Couples { get; set; }
    }
}
