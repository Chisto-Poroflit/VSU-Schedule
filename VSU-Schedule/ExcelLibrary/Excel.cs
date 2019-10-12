using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelLibrary
{
    public class Excel
    {
        public ExcelConfig Config { get; }

        private readonly ExcelPackage _excel;
        private readonly FileInfo _excelFileTmpPath;

        private readonly Dictionary<string, int> _groupsValue = new Dictionary<string, int>();
        private readonly Dictionary<int, int> _daysRows = new Dictionary<int, int>()
        {
            {1, 3},
            {2, 17},
            {3, 31},
            {4, 45},
            {5, 59},
            {6, 73}
        };
        private readonly Dictionary<string, int> _timesRows = new Dictionary<string, int>()
        {
            {"8:00", 0},
            {"9:45", 2},
            {"11:30", 4},
            {"13:25", 6},
            {"15:10", 8},
            {"16:55", 10},
            {"18:40", 12}
        };

        public Excel(ExcelConfig config)
        {
            Config = config;
            _excel = new ExcelPackage();
            for (int i = 1; i <= 6; i++)
            {
                if (i < 5)
                    _excel.Workbook.Worksheets.Add($"Курс {i}");
                else
                    _excel.Workbook.Worksheets.Add($"Маг Курс {i - 4}");
            }

            _excelFileTmpPath = new FileInfo($"{Directory.GetCurrentDirectory()}/{Guid.NewGuid().ToString()}.xlsx");
        }

        public void CreateStartExcel(List<List<int>> groups)
        {
            string[] days = Config.DaysOfWeek;
            string[] time = Config.DayTimes;
            // List<int> subgr = new List<int>();

            for (int w = 0; w < groups.Count; w++)
            {
                var worksheet = _excel.Workbook.Worksheets[w];
                List<int> gr = new List<int>();
                List<int> countSubGr = new List<int>();
                List<int> subGr = new List<int>();
                int lastelementgr = 0, lst = 1;
                int counter = 0;
                for (int i = 0; i < groups[w].Count; i++)
                {
                    if ((i == 0 || i % 2 == 0) && (groups[w][i] != lastelementgr))
                    {
                        gr.Add(groups[w][i]);
                        lastelementgr = groups[w][i];
                    }
                    else if (i % 2 == 1 && lst == lastelementgr)
                    {
                        subGr.Add(groups[w][i]);
                        counter++;
                    }
                    else if (i % 2 == 1 && lst != lastelementgr)
                    {
                        subGr.Add(groups[w][i]);
                        countSubGr.Add(counter);
                        counter = 1;
                        lst = lastelementgr;
                    }
                }

                countSubGr.Add(counter);


                int q = 0;
                for (int i = 1; i <= gr.Count; i++)
                {
                    using (ExcelRange rng = worksheet.Cells[1, i * 2 + 1, 1, i * 2 + 2])
                    {
                        rng.Value = $"Группа {gr[i - 1]}";
                        rng.Merge = true;

                        rng.Style.Font.Size = 12;
                        rng.Style.Font.Bold = true;

                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        rng.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                        rng.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        rng.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                        rng.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    }


                    for (int j = 1; j <= countSubGr[i - 1]; j++)
                    {
                        _groupsValue.Add($"{w + 1}+{gr[i - 1]}.{subGr[q]}", i * 2 + subGr[q]);
                        using (ExcelRange rng = worksheet.Cells[2, i * 2 + subGr[q]])
                        {
                            rng.Value = $"{gr[i - 1]}.{subGr[q]}";
                            q++;

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

            // Dobavlenie dney nedeli
            for (int w = 0; w < 6; w++)
            {
                var worksheet = _excel.Workbook.Worksheets[w];
                int q = 0;

                for (int i = 1; i < 7; i++)
                {
                    using (ExcelRange rng = worksheet.Cells[(i - 1) * 13 + 3 + q, 1, i * 13 + 3 + q, 1])
                    {
                        rng.Value = $"{days[i - 1]}";
                        rng.Merge = true;

                        rng.Style.Font.Size = 12;
                        rng.Style.Font.Bold = true;

                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        rng.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                        rng.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        rng.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                        rng.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    }

                    for (int j = 1; j < 8; j++)
                    {
                        using (ExcelRange rng = worksheet.Cells[(i - 1) * 14 + 3 + (j - 1) * 2, 2,
                            (i - 1) * 14 + 4 + (j - 1) * 2, 2])
                        {
                            rng.Value = $"{time[j - 1]}";
                            rng.Merge = true;

                            rng.AutoFitColumns();
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

                    q++;
                }
            }

            _excel.SaveAs(_excelFileTmpPath);
            //excel.SaveAs(new FileInfo(_path));
        }

        public void AddInfo(List<Dictionary<string, string>> couplesInfo)
        {
            foreach (var coup in couplesInfo)
            {
                var worksheet = _excel.Workbook.Worksheets[Convert.ToInt32(coup["Course"]) - 1];

                string info =
                    new StringBuilder(coup["Subject"] + " " + "(" + coup["Room"] + ")" + "\n" + coup["Teacher"])
                        .ToString();

                var cellsRows = FindNumberOfCell(coup["Groups"], Convert.ToInt32(coup["Day"]),
                    coup["Time"], coup["Numerator"], coup["Denomirator"]);

                foreach (int[] el in cellsRows)
                {
                    using (ExcelRange rng = worksheet.Cells[el[0], el[1], el[2], el[3]])
                    {
                        rng.Value = info;
                        rng.Merge = true;

                        rng.AutoFitColumns();
                        rng.Style.Font.Size = 12;
                        rng.Style.Font.Bold = true;

                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                }
            }

            _excel.SaveAs(_excelFileTmpPath);
            //excel.SaveAs(new FileInfo(_path));
        }

        private List<int[]> FindNumberOfCell(string groups, int day, string time, string num, string denum)
        {
            List<int[]> vss = new List<int[]>();
            int elem0 = 0, elem2 = 0;

            string[] gr = groups.Split(",");
            gr = gr.Take(gr.Count() - 1).ToArray();

            if (num == "false")
            {
                elem0 = _daysRows[day] + _timesRows[time] + 1;
                elem2 = _daysRows[day] + _timesRows[time] + 1;
            }

            else if (denum == "false")
            {
                elem0 = _daysRows[day] + _timesRows[time];
                elem2 = _daysRows[day] + _timesRows[time];
            }

            else
            {
                elem0 = _daysRows[day] + _timesRows[time];
                elem2 = _daysRows[day] + _timesRows[time] + 1;
            }

            if (gr.Length == 1)
            {
                int[] vs = new int[4];
                vs[0] = elem0;
                vs[2] = elem2;
                vs[1] = _groupsValue[gr[0]];
                vs[3] = _groupsValue[gr[0]];
                vss.Add(vs);
            }
            else
            {
                for (int i = 0; i < gr.Length; i++)
                {
                    int[] vs = new int[4];
                    vs[0] = elem0;
                    vs[2] = elem2;
                    vs[1] = _groupsValue[gr[i]];
                    vs[3] = _groupsValue[gr[i]];
                    vss.Add(vs);
                }
            }

            return vss;
        }
    }
}