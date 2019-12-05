using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelLibrary.Infos
{
    public class GroupInfo
    {
        public GroupInfo(int groupNum, int subGroupNum, int courseNum, CourseType courseType)
        {
            GroupNum = groupNum;
            SubGroupNum = subGroupNum;
            CourseNum = courseNum;
            CourseType = courseType;
        }
        public CourseType CourseType { get; }
        public int CourseNum { get; }
        public int GroupNum { get; }
        public int SubGroupNum { get; }
    }

    public enum CourseType
    {
        Bachelor,
        Mag,
        Aspirant
    }


}
