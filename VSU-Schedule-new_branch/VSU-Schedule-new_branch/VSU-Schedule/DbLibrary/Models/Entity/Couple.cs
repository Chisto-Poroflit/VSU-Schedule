using System.Collections.Generic;

namespace DbLibrary.Models.Entity
{
    public class Couple
    {
        public int Id { get; set; }

        public string RoomId { get; set; }
        public Room Room { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int ParaId { get; set; }
        public Para Para { get; set; }

        public int Day { get; set; }

        public bool Numerator { get; set; }

        public bool Denomirator { get; set; }

        public int LessonType { get; set; }

        public List<CoupleGroup> CoupleGroups { get; set; }
    }
}
