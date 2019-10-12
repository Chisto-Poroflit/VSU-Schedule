using System.Collections.Generic;
using ExcelLibrary;
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
            excel.CreateStartExcel(new List<List<int>>(){new List<int>(){1, 2}});
        }
    }
}