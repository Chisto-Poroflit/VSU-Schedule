
using System.Collections.Generic;

namespace DbLibrary.Models.Entity
{
    public class Teacher
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public List<Couple> Couples { get; set; }
    }
}
