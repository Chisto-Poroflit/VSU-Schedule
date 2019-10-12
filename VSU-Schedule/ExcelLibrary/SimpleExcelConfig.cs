using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DbLibrary;

namespace ExcelLibrary
{
    public class SimpleExcelConfig : ExcelConfig
    {
        public SimpleExcelConfig()
        {


            DaysOfWeek = new[] {"Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"};
            DayTimes = new[] {"8:00-9:35", "9:45-11:20", "11:30-13:05", "13:25-15:00", "15:10-16:45", "16:55-18:30", "18:40-20:00"};
        }
    }
}
