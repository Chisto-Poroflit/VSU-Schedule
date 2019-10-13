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
        private readonly FileInfo _excelFileTmpPath;

        public Excel(ExcelConfig config)
        {
            Config = config;
            _excel = new ExcelPackage();
            _excelFileTmpPath = new FileInfo($"{Directory.GetCurrentDirectory()}/{Guid.NewGuid().ToString()}.xlsx");
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
    }
}