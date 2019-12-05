using System.Collections.Generic;

namespace DbLibrary.Models.Entity
{
    public class Para
    {
        public int Number { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public List<Couple> Couples { get; set; }
    }
}
