
namespace DbLibrary.Models.Entity
{
    public class CurriculumUnit
    {
        public int Id { get; set; }

        public int QuantityLect { get; set; }

        public int QuantityPrac { get; set; }

        public int QuantityLab { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
