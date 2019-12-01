using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ExcelLibrary.Infos;

namespace ExcelLibrary.NameResolvers
{
    public class CourseTypeNameResolver
    {
        private static Dictionary<CourseType, string> _names = new Dictionary<CourseType, string>()
        {
            {CourseType.Bachelor, "Курс"},
            {CourseType.Mag, "Маг. курс"},
            {CourseType.Aspirant, "Асп. курс"},

        };

        public string this[CourseType type] => _names[type];
    }
}
