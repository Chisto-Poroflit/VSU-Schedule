using System.Collections.Generic;
using ExcelLibrary;
using ExcelLibrary.Infos;
using NUnit.Framework;

namespace UnitTests
{
    public class ExcelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GenerateTest()
        {
            var excel = new Excel(new SimpleExcelConfig());
            excel.CreateStartExcel(new List<GroupInfo>()
            {
                new GroupInfo(1, 1, 1, CourseType.Bachelor),
                new GroupInfo(1, 2, 1, CourseType.Bachelor)
            });
        }

        [Test]
        public void GenerateTest2Courses()
        {
            var excel = new Excel(new SimpleExcelConfig());
            excel.CreateStartExcel(new List<GroupInfo>()
            {
                new GroupInfo(1, 1, 1, CourseType.Bachelor),
                new GroupInfo(1, 2, 1, CourseType.Bachelor),
                new GroupInfo(2, 1, 2, CourseType.Bachelor),
                new GroupInfo(2, 2, 2, CourseType.Bachelor)
            });
        }

        [Test]
        public void GenerateTestEmpty()
        {
            var excel = new Excel(new SimpleExcelConfig());
            excel.CreateStartExcel(new List<GroupInfo>());
        }
    }
}