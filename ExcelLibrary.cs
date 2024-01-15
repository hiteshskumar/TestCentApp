using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.JSInterop;
using ChargesWFM.UI.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ChargesWFM.UI
{
    public static class ExcelPackage
    {
        public static StringBuilder excelResult;

        public static Task<byte[]> GenerateTemplateAsync(string[] headers)
        {
            var stream = new MemoryStream();
            using var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            var workbookPart = document.AddWorkbookPart();
            document.WorkbookPart.Workbook = new Workbook();
            Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            sheetPart.Worksheet = new Worksheet(sheetData);
            Sheet sheet = new()
            {
                Id = document.WorkbookPart.GetIdOfPart(sheetPart),
                SheetId = 1,
                Name = "Data"
            };
            sheets.Append(sheet);
            var headerRow = new Row();
            foreach (var header in headers)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(header)
                };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            workbookPart.Workbook.Save();
            document.Dispose();
            return Task.FromResult(stream.ToArray());
        }

        public static Task<byte[]> GenerateTemplateAsync1<T>(string[] headers,string[] headerDisplayname, IEnumerable<T> list)
        {
            var stream = new MemoryStream();
            using var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            var workbookPart = document.AddWorkbookPart();
            document.WorkbookPart.Workbook = new Workbook();
            Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            sheetPart.Worksheet = new Worksheet(sheetData);
            
            Sheet sheet = new()
            {
                Id = document.WorkbookPart.GetIdOfPart(sheetPart),
                SheetId = 1,
                Name = "Data"
            };
            sheets.Append(sheet);
            var headerRow = new Row();
            int i = 0;
            foreach (var header in headers)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(headerDisplayname[i])
                };
                
                headerRow.AppendChild(cell);
                i++;
            }
            sheetData.AppendChild(headerRow);
            foreach (var data in list)
            {
                var newRow = new Row();
                foreach (var header in headers)
                {
                    var value = typeof(T)
                        .GetProperty(header)
                        .GetValue(data);
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(value?.ToString())
                    };
                    newRow.AppendChild(cell);
                }
                sheetData.AppendChild(newRow);
            }
            workbookPart.Workbook.Save();
            document.Dispose();
            return Task.FromResult(stream.ToArray());
        }

        public static Task<byte[]> GenerateTemplateAsync<T>(string[] headers, IEnumerable<T> list)
        {
            var stream = new MemoryStream();
            using var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            var workbookPart = document.AddWorkbookPart();
            document.WorkbookPart.Workbook = new Workbook();
            Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            sheetPart.Worksheet = new Worksheet(sheetData);
            Sheet sheet = new()
            {
                Id = document.WorkbookPart.GetIdOfPart(sheetPart),
                SheetId = 1,
                Name = "Data"
            };
            sheets.Append(sheet);
            var headerRow = new Row();
            foreach (var header in headers)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(header)
                };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (var data in list)
            {
                var newRow = new Row();
                foreach (var header in headers)
                {
                    var value = typeof(T)
                        .GetProperty(header)
                        .GetValue(data);
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(value?.ToString())
                    };
                    newRow.AppendChild(cell);
                }
                sheetData.AppendChild(newRow);
            }
            workbookPart.Workbook.Save();
            document.Dispose();
            return Task.FromResult(stream.ToArray());
        }

        public static Task<byte[]> GenerateTemplateAsync<T>(IEnumerable<T> list)
        {
            var stream = new MemoryStream();
            using var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            var workbookPart = document.AddWorkbookPart();
            document.WorkbookPart.Workbook = new Workbook();
            Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            sheetPart.Worksheet = new Worksheet(sheetData);
            Sheet sheet = new()
            {
                Id = document.WorkbookPart.GetIdOfPart(sheetPart),
                SheetId = 1,
                Name = "Data"
            };
            sheets.Append(sheet);
            var headerRow = new Row();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(property.Name)
                };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (var data in list)
            {
                var newRow = new Row();
                foreach (var property in properties)
                {
                    var value = property.GetValue(data);
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(value?.ToString())
                    };
                    newRow.AppendChild(cell);
                }
                sheetData.AppendChild(newRow);
            }
            workbookPart.Workbook.Save();
            document.Dispose();
            return Task.FromResult(stream.ToArray());
        }

        public static Task<byte[]> GenerateTemplateAsync(DataTable table)
        {
            var stream = new MemoryStream();
            using var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            var workbookPart = document.AddWorkbookPart();
            document.WorkbookPart.Workbook = new Workbook();
            Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            sheetPart.Worksheet = new Worksheet(sheetData);
            Sheet sheet = new()
            {
                Id = document.WorkbookPart.GetIdOfPart(sheetPart),
                SheetId = 1,
                Name = "Data"
            };
            sheets.Append(sheet);
            var headerRow = new Row();
            foreach (DataColumn column in table.Columns)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(column.ColumnName)
                };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);
            foreach (DataRow data in table.Rows)
            {
                var newRow = new Row();
                foreach (DataColumn column in table.Columns)
                {
                    var value = data[column.ColumnName];
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(value?.ToString())
                    };
                    newRow.AppendChild(cell);
                }
                sheetData.AppendChild(newRow);
            }
            workbookPart.Workbook.Save();
            document.Dispose();
            return Task.FromResult(stream.ToArray());
        }

        public static async Task GenerateExcel(IJSRuntime ijsRuntime, ExcelFeatures excelFeatures)
        {
            byte[] fileContents;
            MemoryStream memoryStream = new MemoryStream();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = excelFeatures.SheetName
                };

                Row row = new Row() { RowIndex = 1 };
                foreach (var excelColumn in excelFeatures.ExcelColumns)
                {
                    Cell header = new Cell() { CellReference = excelColumn.Cell, CellValue = new CellValue(excelColumn.Column), DataType = CellValues.String };
                    row.Append(header);
                }

                sheetData.Append(row);
                sheets.Append(sheet);

                if (excelFeatures.SecondExcelColumns != null)
                {
                    // Add a WorksheetPart to the WorkbookPart.
                    WorksheetPart worksheetPart2 = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData2 = new SheetData();
                    worksheetPart2.Worksheet = new Worksheet(sheetData2);

                    // Append a new worksheet and associate it with the workbook.
                    Sheet sheet2 = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart2),
                        SheetId = 2,
                        Name = "PlacementWithDataTypes"
                    };

                    Row row2 = new Row() { RowIndex = 1 };
                    foreach (var excelColumn in excelFeatures.SecondExcelColumns)
                    {
                        Cell header2 = new Cell() { CellReference = excelColumn.Cell, CellValue = new CellValue(excelColumn.Column), DataType = CellValues.String };
                        row2.Append(header2);
                    }

                    sheetData2.Append(row2);
                    sheets.Append(sheet2);
                }

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Dispose();

                fileContents = memoryStream.ToArray();
            }

            await ijsRuntime.InvokeVoidAsync(
                "saveAsFile",
                new[] {
                    excelFeatures.FileName,
                    Convert.ToBase64String(fileContents)
                }
            );
        }

        public static DataTable ReadAsDataTable(Stream stream)
        {
            DataTable dataTable = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(stream, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }

                foreach (Row row in rows)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        Cell cell = row.Descendants<Cell>().ElementAt(i);
                        int actualCellIndex = CellReferenceToIndex(cell);
                        dataRow[actualCellIndex] = GetCellValue(spreadSheetDocument, cell);
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }
            dataTable.Rows.RemoveAt(0);
            return dataTable;
        }

        private static int CellReferenceToIndex(Cell cell)
        {
            int index = 0;
            string reference = cell.CellReference.ToString().ToUpper();
            foreach (char ch in reference)
            {
                if (Char.IsLetter(ch))
                {
                    int value = (int)ch - (int)'A';
                    index = (index == 0) ? value : ((index + 1) * 26) + value;
                }
                else
                {
                    return index;
                }
            }
            return index;
        }

        public static StringBuilder ReadFromExcel(Stream stream)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, false))
            {
                //create the object for workbook part
                WorkbookPart workbookPart = doc.WorkbookPart;
                Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
                excelResult = new StringBuilder();

                int count = 0;

                //using for each loop to get the sheet from the sheetcollection
                foreach (Sheet thesheet in thesheetcollection)
                {
                    if (count == 0)
                    {
                        //statement to get the worksheet object by using the sheet id
                        Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;

                        SheetData thesheetdata = (SheetData)theWorksheet.GetFirstChild<SheetData>();
                        foreach (Row thecurrentrow in thesheetdata)
                        {
                            foreach (Cell thecurrentcell in thecurrentrow)
                            {
                                //statement to take the integer value
                                string currentcellvalue = string.Empty;
                                if (thecurrentcell.DataType != null)
                                {
                                    if (thecurrentcell.DataType == CellValues.SharedString)
                                    {
                                        int id;
                                        if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                        {
                                            SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                            if (item.Text != null)
                                            {
                                                //code to take the string value
                                                excelResult.Append(item.Text.Text + ",");
                                            }
                                            else if (item.InnerText != null)
                                            {
                                                currentcellvalue = item.InnerText;
                                            }
                                            else if (item.InnerXml != null)
                                            {
                                                currentcellvalue = item.InnerXml;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    excelResult.Append(Convert.ToInt16(thecurrentcell.InnerText) + ",");
                                }
                            }
                            excelResult.AppendLine();
                        }
                        excelResult.Append("");
                        count++;
                    }
                }
            }

            return excelResult;
        }

        public static Task<IEnumerable<string>> ReadHeadersAsync(Stream stream)
        {
            return Task.Factory.StartNew(() =>
            {
                stream.Seek(0, SeekOrigin.Begin);
                using var document = SpreadsheetDocument.Open(stream, false);
                WorkbookPart workbookPart = document.WorkbookPart;
                foreach (var worksheetPart in workbookPart.WorksheetParts)
                {
                    var headers = new List<string>();
                    OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
                    while (reader.Read())
                    {
                        if (reader.ElementType == typeof(Row))
                        {
                            do
                            {
                                if (reader.ElementType == typeof(Cell))
                                {
                                    var cell = (Cell)reader.LoadCurrentElement();
                                    var value = cell.ParseValue<string>(workbookPart);
                                    headers.Add(value);
                                }
                                reader.Read();
                            }
                            while (reader.ElementType != typeof(Row));
                            break;
                        }
                    }
                    if (headers.Count > 0)
                    {
                        return headers.AsEnumerable();
                    }
                }
                return Enumerable.Empty<string>();
            });
        }

        public static DataTable ExtractExcelSheetValuesToDataTable(Stream stream)
        {
            DataTable dt = new DataTable();

            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(stream, false))
            {

                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }
                foreach (Row row in rows) //this will also include your header row...
                {
                    DataRow tempRow = dt.NewRow();
                    int columnIndex = 0;
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        // Gets the column index of the cell with data
                        int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                        cellColumnIndex--; //zero based index
                        if (columnIndex < cellColumnIndex)
                        {
                            do
                            {
                                tempRow[columnIndex] = ""; //Insert blank data here;
                                columnIndex++;
                            }
                            while (columnIndex < cellColumnIndex);
                        }
                        tempRow[columnIndex] = GetCellValue(spreadSheetDocument, cell);

                        columnIndex++;
                    }

                    dt.Rows.Add(tempRow);
                }

            }
            dt.Rows.RemoveAt(0); //...so i'm taking it out here.
            return dt;
        }

        public static MemoryStream SaveToExcel(IEnumerable<DataTable> dataTables)
        {
            var stream = new MemoryStream();
            using var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            var workbookPart = document.AddWorkbookPart();
            document.WorkbookPart.Workbook = new Workbook();
            Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            sheetPart.Worksheet = new Worksheet(sheetData);
            Sheet sheet = new Sheet()
            {
                Id = document.WorkbookPart.GetIdOfPart(sheetPart),
                SheetId = 1,
                Name = "Data"
            };
            sheets.Append(sheet);
            var columns = new List<string>();
            var headerRow = new Row();
            foreach (DataColumn column in dataTables.First().Columns)
            {
                columns.Add(column.ColumnName);
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(column.ColumnName)
                };
                headerRow.AppendChild(cell);
            }
            sheetData.AppendChild(headerRow);

            foreach (var dataTable in dataTables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var newRow = new Row();
                    foreach (var column in columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[column].ToString())
                        };
                        newRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(newRow);
                }
            }
            workbookPart.Workbook.Save();
            document.Dispose();
            return stream;
        }

        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue?.InnerXml;

            if (cell.CellValue == null)
            {
                return "";
            }

            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        public static int? GetColumnIndexFromName(string columnName)
        {
            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }

        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

        public static async Task DownloadExcel(IJSRuntime ijsRuntime, ExcelFeatures excelFeatures, DataTable table)
        {
            byte[] fileContents;
            MemoryStream memoryStream = new MemoryStream();
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = excelFeatures.SheetName };

                sheets.Append(sheet);
                Row headerRow = new Row();
                List<String> columns = new List<string>();
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);
                foreach (DataRow dsrow in table.Rows)
                {
                    Row newRow = new Row();
                    foreach (String col in columns)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
                // Close the document.
                document.Dispose();

                fileContents = memoryStream.ToArray();
            }

            await ijsRuntime.InvokeVoidAsync("saveAsFile", new[] { excelFeatures.FileName, Convert.ToBase64String(fileContents)});
        }

        public static Task<DataTable> ToDataTable<T>(this IEnumerable<T> list)
        {
            return Task.Factory.StartNew(() =>
            {
                var dataTable = new DataTable();
                var type = typeof(T);
                var properties = type.GetProperties();
                foreach (var propertyInfo in properties)
                {
                    dataTable.Columns.Add(propertyInfo.Name);
                }
                foreach (var data in list)
                {
                    var row = dataTable.NewRow();
                    foreach (var propertyInfo in properties)
                    {
                        row[propertyInfo.Name] = propertyInfo.GetValue(data);
                    }
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            });
        }

        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                if (dr[column.ColumnName] != DBNull.Value)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            if (column.ColumnName == "FieldSectionDisplayOrder" || column.ColumnName == "FieldDisplayOrder")
                            {
                                pro.SetValue(obj, dr[column.ColumnName].ToString() == "" ? -1 : Convert.ToInt32(dr[column.ColumnName]), null);
                            }
                            else
                            {
                                pro.SetValue(obj, dr[column.ColumnName].ToString().Trim(), null);
                            }
                        }
                    }
                }
            }
            return obj;
        }
    }

    public static class ExcelReader
    {
        /// <summary>
        /// Parses the value from a cell
        /// </summary>
        /// <param name="cell">Cell to parse the value</param>
        /// <param name="workbookPart">Workbook part</param>
        /// <param name="type">Type of the value</param>
        /// <returns>Parsed value</returns>
        public static object ParseValue(this Cell cell, WorkbookPart workbookPart, Type type)
        {
            if (cell == null)
            {
                return default;
            }
            else if (type.IsPrimitive)
            {
                var value = cell.ParsePrimitiveValue(workbookPart, type);
                return Convert.ChangeType(value, type);
            }
            else if (type == typeof(string))
            {
                var value = cell.ParseStringValue(workbookPart);
                return Convert.ChangeType(value, type);
            }
            else if (type == typeof(DateTime))
            {
                var value = cell.ParseDateTimeValue(workbookPart);
                return Convert.ChangeType(value, type);
            }
            else if (type == typeof(DateTime?))
            {
                // var value = cell.ParseDateTimeValue(workbookPart);
                // DateTime? nullableValue = new DateTime?(value);
                // return nullableValue;
                return (DateTime?)cell.ParseDateTimeValue(workbookPart);
            }
            else
            {
                return Convert.ChangeType(cell.CellValue.InnerText, type);
            }
        }

        /// <summary>
        /// Parses T value from a cell
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="workbookPart">Workbook part</param>
        /// <typeparam name="T">Value type</typeparam>
        /// <returns>Parsed value</returns>
        public static T ParseValue<T>(this Cell cell, WorkbookPart workbookPart)
        {
            return (T)cell.ParseValue(workbookPart, typeof(T));
        }

        private static DateTime ParseDateTimeValue(this Cell cell, WorkbookPart workbookPart)
        {
            if (cell == null)
            {
                return DateTime.MaxValue;
            }
            var text = cell.ParseStringValue(workbookPart);
            if (string.IsNullOrEmpty(text))
            {
                return DateTime.MaxValue;
            }
            else if (DateTime.TryParse(text, out DateTime dt))
            {
                return dt;
            }
            else
            {
                var convertedValue = Convert.ChangeType(text, typeof(double));
                double dblValue = Convert.ToDouble(convertedValue);
                return DateTime.FromOADate(dblValue);
            }
        }

        private static object ParsePrimitiveValue(this Cell cell, WorkbookPart workbookPart, Type type)
        {
            if (cell == null || !type.IsPrimitive)
            {
                return default;
            }
            var text = cell.ParseStringValue(workbookPart);
            if (string.IsNullOrEmpty(text))
            {
                return default;
            }
            return Convert.ChangeType(text, type);
        }

        private static string ParseStringValue(this Cell cell, WorkbookPart workbookPart)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            else if (cell.CellValue == null)
            {
                return string.Empty;
            }
            else if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                var value = workbookPart.GetSharedStringItemById(int.Parse(cell.CellValue.InnerText));
                return value?.Text.Text.Trim();
            }
            else
            {
                return cell.CellValue.InnerText.Trim();
            }
        }

        /// <summary>
        /// Gets the string from shared string table in the workbook for the Id
        /// </summary>
        /// <param name="workbookPart">Workbook part</param>
        /// <param name="id">Shared string Id</param>
        /// <returns>Shared string item</returns>
        public static SharedStringItem GetSharedStringItemById(this WorkbookPart workbookPart, int id)
        {
            return workbookPart
                .SharedStringTablePart
                .SharedStringTable
                .Elements<SharedStringItem>()
                .ElementAtOrDefault(id);
        }
    }
}
