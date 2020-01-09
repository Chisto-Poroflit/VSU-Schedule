using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelLibrary.Infos
{
    public class CoupleInfo
    {
        public CoupleInfo(string subjName, string roomNumber, string teacherName,
            int semNumber, int para, int day, bool num, bool denum, List<string> gr)
        {
            SubjectName = subjName;
            RoomNumber = roomNumber;
            TeacherName = teacherName;
            SemesterNumber = semNumber;
            ParaId = para;
            Day = day;
            Numerator = num;
            Denomirator = denum;
            GroupAndSubgroupsNumbers = gr;
        }

        public string SubjectName { get; }
        public string RoomNumber { get; }
        public string TeacherName { get; }
        public int SemesterNumber { get; }
        public int ParaId { get; }
        public int Day { get; }
        public bool Numerator { get; }
        public bool Denomirator { get; }
        public List<string> GroupAndSubgroupsNumbers { get; }
    }
}
