using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbLibrary;
using ExcelLibrary.Infos;
using Microsoft.EntityFrameworkCore;

namespace ExcelLibrary
{
    public class DbToExcel
    {
        readonly ApplicationContext _applicationContext;

        public DbToExcel(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public List<GroupInfo> CreateGroupInfos()
        {
            List<GroupInfo> groups = new List<GroupInfo>();

            foreach (var gr in _applicationContext.Groups)
            {
                groups.Add(new GroupInfo(gr.GroupNumber, gr.SubgroupNumber, gr.SemesterNumber % 2 == 0 ? gr.SemesterNumber / 2 : gr.SemesterNumber + 1 / 2, gr.SemesterNumber > 8 ? CourseType.Mag : CourseType.Bachelor));
            }

            return groups;
        }

        public List<List<int>> GroupCounter()
        {
            using (_applicationContext)
            {
                List<List<int>> groupsInCourses = new List<List<int>>();
                //var couples = _applicationContext.Groups.ToList();
                int count = _applicationContext.Groups.Max(p => p.SemesterNumber);
                if (count % 2 == 1)
                    count = (count + 1) / 2;
                else
                    count /= 2;
                for (int i = 1; i <= count; i++)
                {
                    List<int> groupsInCourse = new List<int>();
                    var grChet = (from gr in _applicationContext.Groups
                               where gr.SemesterNumber == i * 2
                               select gr.GroupNumber).ToList();
                    var subgrChet = (from gr in _applicationContext.Groups
                               where gr.SemesterNumber == i * 2
                               select gr.SubgroupNumber).ToList();
                    var grNech = (from gr in _applicationContext.Groups
                               where gr.SemesterNumber == i * 2 - 1
                               select gr.GroupNumber).ToList();
                    var subgrNech = (from gr in _applicationContext.Groups
                               where gr.SemesterNumber == i * 2 - 1
                               select gr.SubgroupNumber).ToList();
                    if (grChet == null && subgrChet == null)
                    {
                        for(int j = 0; j < grNech.Count(); j++)
                        {
                            groupsInCourse.Add(grNech[j]);
                            groupsInCourse.Add(subgrNech[j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < grNech.Count(); j++)
                        {
                            groupsInCourse.Add(grNech[j]);
                            groupsInCourse.Add(subgrNech[j]);
                        }
                    }
                    //if (asd == null && dsa == null)
                    //    counter = 0;
                    //else
                    //    counter = asd.Count() + dsa.Count();
                    //int groupsCount = db.Groups.Count(p => p.SemesterNumber == i * 2);
                    //int groupsCountChet = db.Groups.Count(p => p.SemesterNumber == i * 2 - 1);
                    groupsInCourses.Add(groupsInCourse);
                }
                return groupsInCourses;
            }
        }

        public List<Dictionary<string, string>> CouplesToExcel()
        {
            List<Dictionary<string, string>> keyName = new List<Dictionary<string, string>>();
            using(_applicationContext)
            {
                var couples = _applicationContext.Couples
                    .Include(t => t.CoupleGroups)
                    .Include(p => p.Subject)
                    .Include(t => t.Teacher)
                    .Include(t => t.Para)
                    .Include(t => t.CoupleGroups)
                    .ThenInclude(c => c.Group);
                 //   .ThenInclude(t => t.).ToList();
                //_applicationContext.Subjects.Where(p => p.Id == couples)
                foreach (var couple in couples)
                {
                    Dictionary<string, string> elemetsForExcel = new Dictionary<string, string>();
                    elemetsForExcel.Add("Subject" , couple.Subject.Name);
                    elemetsForExcel.Add("Room", couple.RoomId);
                    elemetsForExcel.Add("Teacher", couple.Teacher.FullName);
                    elemetsForExcel.Add("Time", couple.Para.StartTime);
                    elemetsForExcel.Add("Day", couple.Day.ToString());
                    elemetsForExcel.Add("Numerator", couple.Numerator.ToString().ToLower());
                    elemetsForExcel.Add("Denomirator", couple.Denomirator.ToString().ToLower());
                    StringBuilder sb = new StringBuilder();
                    int q = 0;
                    foreach (var group in couple.CoupleGroups)
                    {
                        q = (group.Group.SemesterNumber + 1) / 2;
                        sb.Append(q + "+" + group.Group.GroupNumber + "." + group.Group.SubgroupNumber + ",");
                    }
                    elemetsForExcel.Add("Groups", sb.ToString());
                    elemetsForExcel.Add("Course", q.ToString());
                    keyName.Add(elemetsForExcel);
                }
            }
            return keyName;
        }
    }
}
