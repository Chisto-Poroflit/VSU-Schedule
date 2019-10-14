using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models.Entity
{
    public class Specialization
    {
        public int Id { get; set; }

        [Display(Name = "Специальность")]
        public string Name { get; set; }

      //  public int ParentSpecId { get; set; }

        public List<Specialization> ChildSpecializations { get; set; } 

        public Specialization ParentSpec { get; set; }

        public List<Group> Group { get; set; }

        public CurriculumUnit CurriculumUnit { get; set; }
    }
}
