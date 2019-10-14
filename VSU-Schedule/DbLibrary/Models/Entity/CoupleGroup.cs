
namespace DbLibrary.Models.Entity
{
    public class CoupleGroup
    {
        public int CoupleId { get; set; }
        public Couple Couple { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
