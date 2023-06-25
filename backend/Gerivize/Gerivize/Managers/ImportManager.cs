using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace Gerivize.Managers
{
    public class ImportManager
    {
        public List<string> ImportInstruments(IFormFile file){
            using (var stream = file.OpenReadStream())
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    List< WorksheetPart> worksheetParts = workbookPart.WorksheetParts.ToList();

                    foreach(WorksheetPart worksheetPart in worksheetParts)
                    {
                        if (worksheetPart != null)
                        {
                            Worksheet worksheet = worksheetPart.Worksheet;
                            Console.WriteLine("Worksheet");

                            // Read column names from the first row
                            Row headerRow = worksheet.GetFirstChild<SheetData>().Elements<Row>().FirstOrDefault();
                            if (headerRow != null)
                            {
                                List<string> columnNames = new List<string>();
                                foreach (Cell cell in headerRow.Elements<Cell>())
                                {
                                    string columnName = GetCellValue(workbookPart, cell);
                                    columnNames.Add(columnName);
                                    Console.WriteLine(columnName);
                                }
                            }

                            // Read data from the remaining rows
                            IEnumerable<Row> dataRows = worksheet.GetFirstChild<SheetData>().Elements<Row>().Skip(1);
                            foreach (Row dataRow in dataRows)
                            {
                                List<string> rowData = new List<string>();
                                foreach (Cell cell in dataRow.Elements<Cell>())
                                {
                                    string cellData = GetCellValue(workbookPart, cell);
                                    rowData.Add(cellData);
                                }
                            }
                            Console.WriteLine("\n\n");
                        }
                    }
                }
            }

            return new List<string>();
        }

        private string GetCellValue(WorkbookPart workbookPart, Cell cell)
        {
            if (cell != null)
            {
                string value = cell.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    SharedStringTablePart stringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                    if (stringTablePart != null)
                    {
                        value = stringTablePart.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                    }
                }
                return value;
            }
            return string.Empty;
        }
    }
}
