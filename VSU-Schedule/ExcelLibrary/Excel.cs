using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ExcelLibrary.Infos;
using ExcelLibrary.NameResolvers;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelLibrary
{
    public class Excel
    {
        public ExcelConfig Config { get; }

        private readonly ExcelPackage _excel;
        public readonly FileInfo _excelFileTmpPath;

        public Excel(ExcelConfig config)
        {
            Config = config;
            _excel = new ExcelPackage();
            var directory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\excel"));
            if(!directory.Exists)
                directory.Create();
            string filepath = Path.Combine(directory.FullName, $"{Guid.NewGuid().ToString()}.xlsx");
            _excelFileTmpPath = new FileInfo(filepath);
        }

        public void CreateStartExcel(List<GroupInfo> groups)
        {
            var groupedByCourseType = groups.GroupBy(g => g.CourseType);
            foreach (var courseType in groupedByCourseType)
            {
                var groupedByCourse = courseType.GroupBy(x => x.CourseNum);
                foreach (var course in groupedByCourse)
                {
                    _excel.Workbook.Worksheets.Add($"{new CourseTypeNameResolver()[courseType.Key]} {course.Key}");
                    var worksheet = _excel.Workbook.Worksheets.Last();
                    PrintDaysAndTimes(worksheet);
                    PrintGroups(worksheet, course);
                }
            }

            if (_excel.Workbook.Worksheets.Count > 0)
                _excel.SaveAs(_excelFileTmpPath);
        }

        private void PrintDaysAndTimes(ExcelWorksheet worksheet)
        {
            int q = 0;

            for (int i = 0; i < Config.DaysOfWeek.Length; i++)
            {
                using (ExcelRange rng = worksheet.Cells[i * 13 + 3 + q, 1, (i + 1) * 13 + 3 + q, 1])
                {
                    rng.Value = $"{Config.DaysOfWeek[i]}";
                    worksheet.Column(1).Width = 20;
                    ApplyRangeStyle(rng);
                }

                for (int j = 0; j < Config.DayTimes.Length; j++)
                {
                    using (ExcelRange rng = worksheet.Cells[i * 14 + 3 + j * 2, 2,
                        i * 14 + 4 + j * 2, 2])
                    {
                        rng.Value = $"{Config.DayTimes[j]}";
                        rng.AutoFitColumns();
                        ApplyRangeStyle(rng);
                    }
                }
                q++;
            }
        }

        private void PrintGroups(ExcelWorksheet worksheet, IEnumerable<GroupInfo> groups)
        {
            var sortedGroups = groups.OrderBy(g => g.GroupNum).GroupBy(x => x.GroupNum);
            var column = 3;
            foreach (var group in sortedGroups)
            {
                var subgroups = group.OrderBy(x => x.SubGroupNum).ToList();
                using (var rng = worksheet.Cells[1, column, 1, column + subgroups.Count() - 1])
                {
                    rng.Value = $"Группа {group.Key}";
                    rng.AutoFitColumns();
                    ApplyRangeStyle(rng);
                }

                foreach (var subgroup in subgroups)
                {
                    using (var rng = worksheet.Cells[2, column, 2, column])
                    {
                        rng.Value = $"{subgroup.GroupNum}.{subgroup.SubGroupNum}";
                        rng.AutoFitColumns();
                        ApplyRangeStyle(rng);
                    }
                    column++;
                }

            }
        }

        private void ApplyRangeStyle(ExcelRange range)
        {
            range.Merge = true;

            range.Style.Font.Size = 12;
            range.Style.Font.Bold = true;

            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            range.Style.Border.Top.Style = ExcelBorderStyle.Medium;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            range.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            range.Style.Border.Left.Style = ExcelBorderStyle.Medium;
        }

        public void AddCouplesToExcel(List<CoupleInfo> couples, List<GroupInfo> groups)
        {
            foreach(var sem in couples.GroupBy(m => m.SemesterNumber))
            {
                var worksheet = _excel.Workbook.Worksheets[sem.Key / 2 - 1];
                var semGroups = groups.Where(m => m.CourseNum == sem.Key/2).ToList();

                List<string> semGrps = CreateSemGroupsList(semGroups.OrderBy(m => m.GroupNum).ThenBy(m => m.SubGroupNum).ToList());

                foreach(var coup in sem)
                {
                    List<int[]> coupInExcel = FindNumberOfCell(coup.GroupAndSubgroupsNumbers, coup.ParaId, coup.Day, coup.Numerator, coup.Denomirator, semGrps);
                    string Info = new StringBuilder(coup.SubjectName + " " + "(" + coup.RoomNumber + ")" + "\n" + coup.TeacherName)
                        .ToString();

                    foreach (var el in coupInExcel)
                    {
                        using (ExcelRange rng = worksheet.Cells[el[0], el[1], el[2], el[3]])
                        {
                            rng.Value = Info;
                            rng.Merge = true;

                            //rng.AutoFitColumns();
                            rng.Style.Font.Size = 12;
                            rng.Style.Font.Bold = true;

                            rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            rng.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                            rng.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                            rng.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                            rng.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                        }
                    }
                }
            }
            _excel.SaveAs(_excelFileTmpPath);
        }

        private List<string> CreateSemGroupsList(List<GroupInfo> groups)
        {
            List<string> grps = new List<string>();

            foreach(var gr in groups)
            {
                grps.Add($"{gr.GroupNum}.{gr.SubGroupNum}");
            }

            return grps;
        }

        private List<int[]> FindNumberOfCell(List<string> groupsInCouple, int para, int day, bool num, bool denum, List<string> allSemGroups)
        {
            List<int[]> numbersOfCell = new List<int[]>();
            int rowElem1 = (day - 1) * 14 + 3 + (para - 1) * 2,
                rowElem2 = (day - 1) * 14 + 3 + (para - 1) * 2;

            if (!num && denum)
            {
                rowElem1 += 1;
                rowElem2 += 1;
            }
            else if(num && denum)
            {
                rowElem2 += 1;
            }

            foreach(var grInCouple in groupsInCouple)
            {
                int cellElem = allSemGroups.IndexOf(grInCouple) + 3;
                numbersOfCell.Add(new int[4] { rowElem1, cellElem, rowElem2, cellElem });
            }

            return numbersOfCell;
        }
    }
}