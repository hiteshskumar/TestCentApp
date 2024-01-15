using System.Collections.Generic;

namespace ChargesWFM.UI.Models
{
    public class ExcelFeatures
    {
        public string FileName { get; set; }
        public string SheetName { get; set; }
        public IEnumerable<ExcelColumns> ExcelColumns { get; set; }
        public IEnumerable<ExcelColumns> SecondExcelColumns { get; set; }
    }

    public class ExcelColumns
    {
        public string Cell { get; set; }
        public string Column { get; set; }
    }
}