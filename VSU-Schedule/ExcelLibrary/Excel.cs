using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelLibrary
{
    public class Excel
    {
        ExcelPackage excel = new ExcelPackage();
        ExcelWorkbook wb;
        ExcelWorksheet ws;
       // string _path;
        FileInfo fileInfo;

        private Dictionary<string, int> groupsValue;

        private static Dictionary<int, int> daysValue = new Dictionary<int, int>()
            {
                {1, 3 },
                {2, 17 },
                {3, 31 },
                {4, 45 },
                {5, 59 },
                {6, 73 }
            };
        private static Dictionary<string, int> timesValue = new Dictionary<string, int>()
            {
                {"8:00", 0 },
                {"9:45", 2 },
                {"11:30", 4 },
                {"13:25", 6 },
                {"15:10", 8 },
                {"16:55", 10 },
                {"18:40", 12 }
            };
        //private static Dictionary<string, int> numDenumValue = new Dictionary<string, int>()
        //    {
        //        {"true,false", 0 },
        //        {"false,true", 1 },
        //        {"true,true", 2 }
        //    };

        public Excel()
        {
            groupsValue = new Dictionary<string, int>();
            wb = excel.Workbook;
           // groupsValue = new Dictionary<string, int>();
            for(int i = 1; i <= 6; i++)
            {
                if (i < 5)
                    wb.Worksheets.Add($"Курс {i}");
                else
                    wb.Worksheets.Add($"Маг Курс {i - 4}");
            }
            ws = wb.Worksheets[1];
            fileInfo = new FileInfo(Directory.GetCurrentDirectory() + "/Test.xlsx");
            //_path = Directory.GetCurrentDirectory() + "/Test.xlsx";
        }

        public Excel(string path)
        {
            groupsValue = new Dictionary<string, int>();
            wb = excel.Workbook;
           // groupsValue = new Dictionary<string, int>();
            for (int i = 1; i <= 6; i++)
            {
                if (i < 5)
                    wb.Worksheets.Add($"Курс {i}");
                else
                    wb.Worksheets.Add($"Маг Курс {i - 4}");
            }
            ws = wb.Worksheets[1];
            fileInfo = new FileInfo(path);
            //_path = path;
        }

        public void CreateStartExcel(List<List<int>> countGroups)
        {
            string[] days = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
            string[] time = new string[] { "8:00-9:35", "9:45-11:20", "11:30-13:05", "13:25-15:00", "15:10-16:45", "16:55-18:30", "18:40-20:00" };
           // List<int> subgr = new List<int>();

            for (int w = 0; w < countGroups.Count; w++)
            {
                ws = wb.Worksheets[w];
                List<int> gr = new List<int>();
                List<int> countSubGr = new List<int>();
                List<int> subGr = new List<int>();
                int lastelementgr = 0, lst = 1;
                int counter = 0;
                for (int i = 0; i < countGroups[w].Count; i++)
                {
                    if ( ( i == 0 || i % 2 == 0 ) && (countGroups[w][i] != lastelementgr) )
                    {
                        gr.Add(countGroups[w][i]);
                        lastelementgr = countGroups[w][i];
                    }
                    else if(i % 2 == 1 && lst == lastelementgr)
                    {
                        subGr.Add(countGroups[w][i]);
                        counter++;
                    }
                    else if(i % 2 == 1 && lst != lastelementgr)
                    {
                        subGr.Add(countGroups[w][i]);
                        countSubGr.Add(counter);
                        counter = 1;
                        lst = lastelementgr;
                    }
                }
                countSubGr.Add(counter);


                int q = 0;
                for (int i = 1; i <= gr.Count; i++)
                {
                    using (ExcelRange rng = ws.Cells[1, i * 2 + 1, 1, i * 2 + 2])
                    {
                        rng.Value = $"Группа {gr[i-1]}";
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
                        groupsValue.Add($"{w+1}+{gr[i - 1]}.{subGr[q]}", i * 2 + subGr[q]);
                        using (ExcelRange rng = ws.Cells[2, i * 2 + subGr[q] ])
                        {
                            rng.Value = $"{gr[i-1]}.{subGr[q]}";
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
                ws = wb.Worksheets[w];
                int q = 0;

                for (int i = 1; i < 7; i++)
                {
                    using (ExcelRange rng = ws.Cells[(i - 1) * 13 + 3 + q, 1, i * 13 + 3 + q, 1])
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
                        using (ExcelRange rng = ws.Cells[(i - 1) * 14 + 3 + (j - 1) * 2, 2, (i - 1) * 14 + 4 + (j - 1) * 2, 2])
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

            excel.SaveAs(fileInfo);
            //excel.SaveAs(new FileInfo(_path));
        }


        public void AddInfo(List<Dictionary<string,string>> couplesInfo)
        {
            foreach (var coup in couplesInfo)
            {
                ws = wb.Worksheets[Convert.ToInt32(coup["Course"])-1];

                List<int[]> cellsRows = new List<int[]>();

                string info = new StringBuilder (coup["Subject"] +" " + "(" + coup["Room"] + ")"+ "\n" + coup["Teacher"]).ToString();

                cellsRows = FindNumberOfCell(coup["Groups"], Convert.ToInt32(coup["Day"]),
                    coup["Time"], coup["Numerator"], coup["Denomirator"]);

                foreach(int[] el in cellsRows)
                {
                    using(ExcelRange rng = ws.Cells[el[0], el[1], el[2], el[3]])
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

            excel.SaveAs(fileInfo);
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
                elem0 = daysValue[day] + timesValue[time] + 1;
                elem2 = daysValue[day] + timesValue[time] + 1;
            }

            else if (denum == "false")
            {
                elem0 = daysValue[day] + timesValue[time];
                elem2 = daysValue[day] + timesValue[time];
            }

            else
            {
                elem0 = daysValue[day] + timesValue[time];
                elem2 = daysValue[day] + timesValue[time] + 1;
            }

            if(gr.Length == 1)
            {
                int[] vs = new int[4];
                vs[0] = elem0;
                vs[2] = elem2;
                vs[1] = groupsValue[gr[0]];
                vs[3] = groupsValue[gr[0]];
                vss.Add(vs);
            }
            else
            {
                for(int i = 0; i < gr.Length; i++)
                {
                    int[] vs = new int[4];
                    vs[0] = elem0;
                    vs[2] = elem2;
                    vs[1] = groupsValue[gr[i]];
                    vs[3] = groupsValue[gr[i]];
                    vss.Add(vs);
                }
            }
            return vss;
        }
    }
}
