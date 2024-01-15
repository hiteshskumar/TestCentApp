using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ChargesWFM.UI.Models;
public static class ExcelService
{
    public static byte[] GenerateExcelWorkbook<T>(IEnumerable<T> list)
    {
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            // simple way
            workSheet.Cells.LoadFromCollection(list, true);

            ////// mutual
            ////workSheet.Row(1).Height = 20;
            ////workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ////workSheet.Row(1).Style.Font.Bold = true;
            ////workSheet.Cells[1, 1].Value = "No";
            ////workSheet.Cells[1, 2].Value = "Name";
            ////workSheet.Cells[1, 3].Value = "Age";

            ////int recordIndex = 2;
            ////foreach (var item in list)
            ////{
            ////    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
            ////    workSheet.Cells[recordIndex, 2].Value = item.UserName;
            ////    workSheet.Cells[recordIndex, 3].Value = item.Age;
            ////    recordIndex++;
            ////}

            return package.GetAsByteArray();
        }
    }

    public static byte[] GenerateGlobalDashboardExcelWorkbook(GetGlobalDashboardDownloadResult[] DownloadSummary)
    {
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {

            var workSheet1 = package.Workbook.Worksheets.Add("Summary");
            var workSheet2 = package.Workbook.Worksheets.Add("BackLog");
            var workSheet3 = package.Workbook.Worksheets.Add("Completed");
            var workSheet4 = package.Workbook.Worksheets.Add("Efficiency");

            workSheet1.Row(1).Style.Font.Bold = true;
            workSheet1.Row(1).Style.Border.Top.Style = ExcelBorderStyle.Medium;
            workSheet1.Row(1).Style.Border.Left.Style = ExcelBorderStyle.Medium;
            workSheet1.Row(1).Style.Border.Right.Style = ExcelBorderStyle.Medium;
            workSheet1.Row(1).Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet1.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet1.Row(1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            workSheet1.Cells[1, 1].Value = "ProjectGroup";
            workSheet1.Cells[1, 2].Value = "Type";
            workSheet1.Cells[1, 3].Value = "Total";
            workSheet1.Cells[1, 4].Value = "6AM-7AM";
            workSheet1.Cells[1, 5].Value = "7AM-8AM";
            workSheet1.Cells[1, 6].Value = "8AM-9AM";
            workSheet1.Cells[1, 7].Value = "9AM-10AM";
            workSheet1.Cells[1, 8].Value = "10AM-11AM";
            workSheet1.Cells[1, 9].Value = "11AM-12PM";
            workSheet1.Cells[1, 10].Value = "12PM-1PM";
            workSheet1.Cells[1, 11].Value = "1PM-2PM";
            workSheet1.Cells[1, 12].Value = "2PM-3PM";
            workSheet1.Cells[1, 13].Value = "3PM-4PM";
            workSheet1.Cells[1, 14].Value = "4PM-5PM";
            workSheet1.Cells[1, 15].Value = "5PM-6PM";
            workSheet1.Cells[1, 16].Value = "6PM-7PM";
            workSheet1.Cells[1, 17].Value = "7PM-8PM";
            workSheet1.Cells[1, 18].Value = "8PM-9PM";
            workSheet1.Cells[1, 19].Value = "9PM-10PM";
            workSheet1.Cells[1, 20].Value = "10PM-11PM";
            workSheet1.Cells[1, 21].Value = "11PM-12AM";
            workSheet1.Cells[1, 22].Value = "12AM-1AM";
            workSheet1.Cells[1, 23].Value = "1AM-2AM";
            workSheet1.Cells[1, 24].Value = "2AM-3AM";
            workSheet1.Cells[1, 25].Value = "3AM-4AM";
            workSheet1.Cells[1, 26].Value = "4AM-5AM";
            workSheet1.Cells[1, 27].Value = "5AM-6AM";

            workSheet1.Column(1).Width = 15;
            workSheet1.Column(2).Width = 15;
            Console.WriteLine(workSheet1.Columns.Count());
            for (int i = 3; workSheet1.Columns.Count() >= i; i++)
            {
                workSheet1.Column(i).Width = 10;
            }


            int SummaryrecordIndex = 2;
            foreach (var item in DownloadSummary[0].Summary)
            {
                workSheet1.Row(SummaryrecordIndex).Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet1.Row(SummaryrecordIndex).Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet1.Row(SummaryrecordIndex).Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet1.Row(SummaryrecordIndex).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet1.Cells[SummaryrecordIndex, 1].Value = item.ProjectGroup;
                workSheet1.Cells[SummaryrecordIndex, 2].Value = item.Type;
                workSheet1.Cells[SummaryrecordIndex, 3].Value = item.Total;
                workSheet1.Cells[SummaryrecordIndex, 4].Value = item.st0;
                workSheet1.Cells[SummaryrecordIndex, 5].Value = item.st1;
                workSheet1.Cells[SummaryrecordIndex, 6].Value = item.st2;
                workSheet1.Cells[SummaryrecordIndex, 7].Value = item.st3;
                workSheet1.Cells[SummaryrecordIndex, 8].Value = item.st4;
                workSheet1.Cells[SummaryrecordIndex, 9].Value = item.st5;
                workSheet1.Cells[SummaryrecordIndex, 10].Value = item.st6;
                workSheet1.Cells[SummaryrecordIndex, 11].Value = item.st7;
                workSheet1.Cells[SummaryrecordIndex, 12].Value = item.st8;
                workSheet1.Cells[SummaryrecordIndex, 13].Value = item.st9;
                workSheet1.Cells[SummaryrecordIndex, 14].Value = item.st10;
                workSheet1.Cells[SummaryrecordIndex, 15].Value = item.st11;
                workSheet1.Cells[SummaryrecordIndex, 16].Value = item.st12;
                workSheet1.Cells[SummaryrecordIndex, 17].Value = item.st13;
                workSheet1.Cells[SummaryrecordIndex, 18].Value = item.st14;
                workSheet1.Cells[SummaryrecordIndex, 19].Value = item.st15;
                workSheet1.Cells[SummaryrecordIndex, 20].Value = item.st16;
                workSheet1.Cells[SummaryrecordIndex, 21].Value = item.st17;
                workSheet1.Cells[SummaryrecordIndex, 22].Value = item.st18;
                workSheet1.Cells[SummaryrecordIndex, 23].Value = item.st19;
                workSheet1.Cells[SummaryrecordIndex, 24].Value = item.st20;
                workSheet1.Cells[SummaryrecordIndex, 25].Value = item.st21;
                workSheet1.Cells[SummaryrecordIndex, 26].Value = item.st22;
                workSheet1.Cells[SummaryrecordIndex, 27].Value = item.st23;

                SummaryrecordIndex++;
            }

            workSheet2.Row(1).Style.Font.Bold = true;
            workSheet2.Row(1).Style.Border.Top.Style = ExcelBorderStyle.Medium;
            workSheet2.Row(1).Style.Border.Left.Style = ExcelBorderStyle.Medium;
            workSheet2.Row(1).Style.Border.Right.Style = ExcelBorderStyle.Medium;
            workSheet2.Row(1).Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet2.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet2.Row(1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            workSheet2.Cells[1, 1].Value = "ProjectGroup";
            workSheet2.Cells[1, 2].Value = "WorkedBy";
            workSheet2.Cells[1, 3].Value = "Total";
            workSheet2.Cells[1, 4].Value = "6AM-7AM";
            workSheet2.Cells[1, 5].Value = "7AM-8AM";
            workSheet2.Cells[1, 6].Value = "8AM-9AM";
            workSheet2.Cells[1, 7].Value = "9AM-10AM";
            workSheet2.Cells[1, 8].Value = "10AM-11AM";
            workSheet2.Cells[1, 9].Value = "11AM-12PM";
            workSheet2.Cells[1, 10].Value = "12PM-1PM";
            workSheet2.Cells[1, 11].Value = "1PM-2PM";
            workSheet2.Cells[1, 12].Value = "2PM-3PM";
            workSheet2.Cells[1, 13].Value = "3PM-4PM";
            workSheet2.Cells[1, 14].Value = "4PM-5PM";
            workSheet2.Cells[1, 15].Value = "5PM-6PM";
            workSheet2.Cells[1, 16].Value = "6PM-7PM";
            workSheet2.Cells[1, 17].Value = "7PM-8PM";
            workSheet2.Cells[1, 18].Value = "8PM-9PM";
            workSheet2.Cells[1, 19].Value = "9PM-10PM";
            workSheet2.Cells[1, 20].Value = "10PM-11PM";
            workSheet2.Cells[1, 21].Value = "11PM-12AM";
            workSheet2.Cells[1, 22].Value = "12AM-1AM";
            workSheet2.Cells[1, 23].Value = "1AM-2AM";
            workSheet2.Cells[1, 24].Value = "2AM-3AM";
            workSheet2.Cells[1, 25].Value = "3AM-4AM";
            workSheet2.Cells[1, 26].Value = "4AM-5AM";
            workSheet2.Cells[1, 27].Value = "5AM-6AM";

            workSheet2.Column(1).Width = 15;
            workSheet2.Column(2).Width = 15;
            for (int p = 3; workSheet2.Columns.Count() >= p; p++)
            {
                workSheet2.Column(p).Width = 10;
            }

            int BackLogrecordIndex = 2;
            foreach (var item in DownloadSummary[0].Backlog)
            {
                workSheet2.Row(BackLogrecordIndex).Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet2.Row(BackLogrecordIndex).Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet2.Row(BackLogrecordIndex).Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet2.Row(BackLogrecordIndex).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet2.Cells[BackLogrecordIndex, 1].Value = item.ProjectGroup;
                workSheet2.Cells[BackLogrecordIndex, 2].Value = item.WorkedBy;
                workSheet2.Cells[BackLogrecordIndex, 3].Value = item.Total;
                workSheet2.Cells[BackLogrecordIndex, 4].Value = item.st0;
                workSheet2.Cells[BackLogrecordIndex, 5].Value = item.st1;
                workSheet2.Cells[BackLogrecordIndex, 6].Value = item.st2;
                workSheet2.Cells[BackLogrecordIndex, 7].Value = item.st3;
                workSheet2.Cells[BackLogrecordIndex, 8].Value = item.st4;
                workSheet2.Cells[BackLogrecordIndex, 9].Value = item.st5;
                workSheet2.Cells[BackLogrecordIndex, 10].Value = item.st6;
                workSheet2.Cells[BackLogrecordIndex, 11].Value = item.st7;
                workSheet2.Cells[BackLogrecordIndex, 12].Value = item.st8;
                workSheet2.Cells[BackLogrecordIndex, 13].Value = item.st9;
                workSheet2.Cells[BackLogrecordIndex, 14].Value = item.st10;
                workSheet2.Cells[BackLogrecordIndex, 15].Value = item.st11;
                workSheet2.Cells[BackLogrecordIndex, 16].Value = item.st12;
                workSheet2.Cells[BackLogrecordIndex, 17].Value = item.st13;
                workSheet2.Cells[BackLogrecordIndex, 18].Value = item.st14;
                workSheet2.Cells[BackLogrecordIndex, 19].Value = item.st15;
                workSheet2.Cells[BackLogrecordIndex, 20].Value = item.st16;
                workSheet2.Cells[BackLogrecordIndex, 21].Value = item.st17;
                workSheet2.Cells[BackLogrecordIndex, 22].Value = item.st18;
                workSheet2.Cells[BackLogrecordIndex, 23].Value = item.st19;
                workSheet2.Cells[BackLogrecordIndex, 24].Value = item.st20;
                workSheet2.Cells[BackLogrecordIndex, 25].Value = item.st21;
                workSheet2.Cells[BackLogrecordIndex, 26].Value = item.st22;
                workSheet2.Cells[BackLogrecordIndex, 27].Value = item.st23;
                BackLogrecordIndex++;
            }

            workSheet3.Row(1).Style.Font.Bold = true;
            workSheet3.Row(1).Style.Border.Top.Style = ExcelBorderStyle.Medium;
            workSheet3.Row(1).Style.Border.Left.Style = ExcelBorderStyle.Medium;
            workSheet3.Row(1).Style.Border.Right.Style = ExcelBorderStyle.Medium;
            workSheet3.Row(1).Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet3.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet3.Row(1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            workSheet3.Cells[1, 1].Value = "ProjectGroup";
            workSheet3.Cells[1, 2].Value = "WorkedBy";
            workSheet3.Cells[1, 3].Value = "Total";
            workSheet3.Cells[1, 4].Value = "6AM-7AM";
            workSheet3.Cells[1, 5].Value = "7AM-8AM";
            workSheet3.Cells[1, 6].Value = "8AM-9AM";
            workSheet3.Cells[1, 7].Value = "9AM-10AM";
            workSheet3.Cells[1, 8].Value = "10AM-11AM";
            workSheet3.Cells[1, 9].Value = "11AM-12PM";
            workSheet3.Cells[1, 10].Value = "12PM-1PM";
            workSheet3.Cells[1, 11].Value = "1PM-2PM";
            workSheet3.Cells[1, 12].Value = "2PM-3PM";
            workSheet3.Cells[1, 13].Value = "3PM-4PM";
            workSheet3.Cells[1, 14].Value = "4PM-5PM";
            workSheet3.Cells[1, 15].Value = "5PM-6PM";
            workSheet3.Cells[1, 16].Value = "6PM-7PM";
            workSheet3.Cells[1, 17].Value = "7PM-8PM";
            workSheet3.Cells[1, 18].Value = "8PM-9PM";
            workSheet3.Cells[1, 19].Value = "9PM-10PM";
            workSheet3.Cells[1, 20].Value = "10PM-11PM";
            workSheet3.Cells[1, 21].Value = "11PM-12AM";
            workSheet3.Cells[1, 22].Value = "12AM-1AM";
            workSheet3.Cells[1, 23].Value = "1AM-2AM";
            workSheet3.Cells[1, 24].Value = "2AM-3AM";
            workSheet3.Cells[1, 25].Value = "3AM-4AM";
            workSheet3.Cells[1, 26].Value = "4AM-5AM";
            workSheet3.Cells[1, 27].Value = "5AM-6AM";

            workSheet3.Column(1).Width = 15;
            workSheet3.Column(2).Width = 15;
            for (int j = 3; workSheet3.Columns.Count() >= j; j++)
            {
                workSheet3.Column(j).Width = 10;
            }

            int CompletedrecordIndex = 2;
            foreach (var item in DownloadSummary[0].CompletedAccounts)
            {
                workSheet3.Row(CompletedrecordIndex).Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet3.Row(CompletedrecordIndex).Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet3.Row(CompletedrecordIndex).Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet3.Row(CompletedrecordIndex).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet3.Cells[CompletedrecordIndex, 1].Value = item.ProjectGroup;
                workSheet3.Cells[CompletedrecordIndex, 2].Value = item.WorkedBy;
                workSheet3.Cells[CompletedrecordIndex, 3].Value = item.Total;
                workSheet3.Cells[CompletedrecordIndex, 4].Value = item.st0;
                workSheet3.Cells[CompletedrecordIndex, 5].Value = item.st1;
                workSheet3.Cells[CompletedrecordIndex, 6].Value = item.st2;
                workSheet3.Cells[CompletedrecordIndex, 7].Value = item.st3;
                workSheet3.Cells[CompletedrecordIndex, 8].Value = item.st4;
                workSheet3.Cells[CompletedrecordIndex, 9].Value = item.st5;
                workSheet3.Cells[CompletedrecordIndex, 10].Value = item.st6;
                workSheet3.Cells[CompletedrecordIndex, 11].Value = item.st7;
                workSheet3.Cells[CompletedrecordIndex, 12].Value = item.st8;
                workSheet3.Cells[CompletedrecordIndex, 13].Value = item.st9;
                workSheet3.Cells[CompletedrecordIndex, 14].Value = item.st10;
                workSheet3.Cells[CompletedrecordIndex, 15].Value = item.st11;
                workSheet3.Cells[CompletedrecordIndex, 16].Value = item.st12;
                workSheet3.Cells[CompletedrecordIndex, 17].Value = item.st13;
                workSheet3.Cells[CompletedrecordIndex, 18].Value = item.st14;
                workSheet3.Cells[CompletedrecordIndex, 19].Value = item.st15;
                workSheet3.Cells[CompletedrecordIndex, 20].Value = item.st16;
                workSheet3.Cells[CompletedrecordIndex, 21].Value = item.st17;
                workSheet3.Cells[CompletedrecordIndex, 22].Value = item.st18;
                workSheet3.Cells[CompletedrecordIndex, 23].Value = item.st19;
                workSheet3.Cells[CompletedrecordIndex, 24].Value = item.st20;
                workSheet3.Cells[CompletedrecordIndex, 25].Value = item.st21;
                workSheet3.Cells[CompletedrecordIndex, 26].Value = item.st22;
                workSheet3.Cells[CompletedrecordIndex, 27].Value = item.st23;
                CompletedrecordIndex++;
            }

            workSheet4.Row(1).Style.Font.Bold = true;
            workSheet4.Row(1).Style.Border.Top.Style = ExcelBorderStyle.Medium;
            workSheet4.Row(1).Style.Border.Left.Style = ExcelBorderStyle.Medium;
            workSheet4.Row(1).Style.Border.Right.Style = ExcelBorderStyle.Medium;
            workSheet4.Row(1).Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet4.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet4.Row(1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            workSheet4.Cells[1, 1].Value = "ProjectGroup";
            workSheet4.Cells[1, 2].Value = "WorkedBy";
            workSheet4.Cells[1, 3].Value = "AchievedPercentageWithCategoryTarget";
            workSheet4.Cells[1, 4].Value = "AchievedPercentageWithFullTarget";
            workSheet4.Cells[1, 5].Value = "Total";
            workSheet4.Cells[1, 6].Value = "6AM-7AM";
            workSheet4.Cells[1, 7].Value = "7AM-8AM";
            workSheet4.Cells[1, 8].Value = "8AM-9AM";
            workSheet4.Cells[1, 9].Value = "9AM-10AM";
            workSheet4.Cells[1, 10].Value = "10AM-11AM";
            workSheet4.Cells[1, 11].Value = "11AM-12PM";
            workSheet4.Cells[1, 12].Value = "12PM-1PM";
            workSheet4.Cells[1, 13].Value = "1PM-2PM";
            workSheet4.Cells[1, 14].Value = "2PM-3PM";
            workSheet4.Cells[1, 15].Value = "3PM-4PM";
            workSheet4.Cells[1, 16].Value = "4PM-5PM";
            workSheet4.Cells[1, 17].Value = "5PM-6PM";
            workSheet4.Cells[1, 18].Value = "6PM-7PM";
            workSheet4.Cells[1, 19].Value = "7PM-8PM";
            workSheet4.Cells[1, 20].Value = "8PM-9PM";
            workSheet4.Cells[1, 21].Value = "9PM-10PM";
            workSheet4.Cells[1, 22].Value = "10PM-11PM";
            workSheet4.Cells[1, 23].Value = "11PM-12AM";
            workSheet4.Cells[1, 24].Value = "12AM-1AM";
            workSheet4.Cells[1, 25].Value = "1AM-2AM";
            workSheet4.Cells[1, 26].Value = "2AM-3AM";
            workSheet4.Cells[1, 27].Value = "3AM-4AM";
            workSheet4.Cells[1, 28].Value = "4AM-5AM";
            workSheet4.Cells[1, 29].Value = "5AM-6AM";

            workSheet4.Column(1).Width = 15;
            workSheet4.Column(2).Width = 15;
            for (int k = 3; workSheet4.Columns.Count() >= k; k++)
            {
                workSheet4.Column(k).Width = 10;
            }

            int EfficiencyrecordIndex = 2;
            foreach (var item in DownloadSummary[0].Efficiency)
            {
                workSheet4.Row(EfficiencyrecordIndex).Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet4.Row(EfficiencyrecordIndex).Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet4.Row(EfficiencyrecordIndex).Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet4.Row(EfficiencyrecordIndex).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet4.Cells[EfficiencyrecordIndex, 1].Value = item.ProjectGroup;
                workSheet4.Cells[EfficiencyrecordIndex, 2].Value = item.WorkedBy;
                workSheet4.Cells[EfficiencyrecordIndex, 3].Value = item.AchievedPercentageWithCategoryTarget;
                workSheet4.Cells[EfficiencyrecordIndex, 4].Value = item.AchievedPercentageWithFullTarget;
                workSheet4.Cells[EfficiencyrecordIndex, 5].Value = item.Total;
                workSheet4.Cells[EfficiencyrecordIndex, 6].Value = item.st0;
                workSheet4.Cells[EfficiencyrecordIndex, 7].Value = item.st1;
                workSheet4.Cells[EfficiencyrecordIndex, 8].Value = item.st2;
                workSheet4.Cells[EfficiencyrecordIndex, 9].Value = item.st3;
                workSheet4.Cells[EfficiencyrecordIndex, 10].Value = item.st4;
                workSheet4.Cells[EfficiencyrecordIndex, 11].Value = item.st5;
                workSheet4.Cells[EfficiencyrecordIndex, 12].Value = item.st6;
                workSheet4.Cells[EfficiencyrecordIndex, 13].Value = item.st7;
                workSheet4.Cells[EfficiencyrecordIndex, 14].Value = item.st8;
                workSheet4.Cells[EfficiencyrecordIndex, 15].Value = item.st9;
                workSheet4.Cells[EfficiencyrecordIndex, 16].Value = item.st10;
                workSheet4.Cells[EfficiencyrecordIndex, 17].Value = item.st11;
                workSheet4.Cells[EfficiencyrecordIndex, 18].Value = item.st12;
                workSheet4.Cells[EfficiencyrecordIndex, 19].Value = item.st13;
                workSheet4.Cells[EfficiencyrecordIndex, 20].Value = item.st14;
                workSheet4.Cells[EfficiencyrecordIndex, 21].Value = item.st15;
                workSheet4.Cells[EfficiencyrecordIndex, 22].Value = item.st16;
                workSheet4.Cells[EfficiencyrecordIndex, 23].Value = item.st17;
                workSheet4.Cells[EfficiencyrecordIndex, 24].Value = item.st18;
                workSheet4.Cells[EfficiencyrecordIndex, 25].Value = item.st19;
                workSheet4.Cells[EfficiencyrecordIndex, 26].Value = item.st20;
                workSheet4.Cells[EfficiencyrecordIndex, 27].Value = item.st21;
                workSheet4.Cells[EfficiencyrecordIndex, 28].Value = item.st22;
                workSheet4.Cells[EfficiencyrecordIndex, 29].Value = item.st23;
                EfficiencyrecordIndex++;
            }

            package.Save();
            return package.GetAsByteArray();
        }
    }
}
