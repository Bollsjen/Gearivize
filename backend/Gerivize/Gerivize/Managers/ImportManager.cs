using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Gerivize.EnumTypes;
using Gerivize.Models;
using Gerivize.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.IO;

namespace Gerivize.Managers
{
    public class ImportManager
    {
        List<string> columnNames = new List<string>() 
        {
            "Instrument",
            "Manufacturer",
            "Type",
            "Test",
            "Ser.No.",
            "Bolls No.",
            "Last Cal. Date",
            "Next Cal. date",
            "Days left",
            "Comment.",
            "Ref. Cal. Instruction",
            "Cal. Data (Intern. Cal.)",
            "Cal. Report No.",
            "Note (Rep. o.l.)",
            "Værdi"
        };

        private UserRepository repository;
        private InstrumentRepository instrumentRepository;

        public ImportManager()
        {
            repository = new UserRepository();
            instrumentRepository = new InstrumentRepository();
        }

        public List<string> ImportInstruments(IFormFile file){
            List<string> errorMessages = new List<string>();
            List<Instrument> instruments = new List<Instrument>();
            using (var stream = file.OpenReadStream())
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    List< WorksheetPart> worksheetParts = workbookPart.WorksheetParts.ToList();

                    int i = 0;
                    foreach(WorksheetPart worksheetPart in worksheetParts)
                    { 
                        if (worksheetPart != null)
                        {
                            Worksheet worksheet = worksheetPart.Worksheet;
                            string sheetName = GetWorksheetName(workbookPart, worksheetPart);

                            // Read column names from the first row
                            Row headerRow = worksheet.GetFirstChild<SheetData>().Elements<Row>().FirstOrDefault();
                            if (headerRow != null)
                            {
                                List<string> columnNames = new List<string>();
                                int cellId = 0;
                                foreach (Cell cell in headerRow.Elements<Cell>())
                                {
                                    string columnName = GetCellValue(workbookPart, cell);
                                    columnNames.Add(columnName);
                                    if (!ValidateColumnName(columnName))
                                    {
                                        string cellReference = cell.CellReference.Value;
                                        string cellLetter = GetCellLetter(cellReference);
                                        uint rowNumber = GetRowNumber(cellReference);

                                        errorMessages.Add("Wrong column name at: row " + rowNumber + ", cell " + cellLetter+", you typed \"" + columnName + "\" but was supposed to be \"" + columnNames[cellId] + "\"");
                                        Console.WriteLine(errorMessages[errorMessages.Count - 1]);
                                    }
                                    cellId++;
                                }
                            }

                            // Read data from the remaining rows
                            IEnumerable<Row> dataRows = worksheet.GetFirstChild<SheetData>().Elements<Row>().Skip(1);
                            i++;
                            foreach (Row dataRow in dataRows)
                            {
                                Dictionary<string,string> rowData = new Dictionary<string,string>();
                                int cellId = 0;
                                foreach (Cell cell in dataRow.Elements<Cell>())
                                {
                                    string cellReference = cell.CellReference.Value;
                                    string cellLetter = GetCellLetter(cellReference);
                                    uint rowNumber = GetRowNumber(cellReference);

                                    string cellData = GetCellValue(workbookPart, cell);
                                    if (cellId == 0 && cellData == "") break;
                                        if (cellId + 1 <= columnNames.Count) rowData.Add(columnNames[cellId], cellData);
                                        else break;
                                        cellId++;
                                }
                                if(cellId == columnNames.Count)
                                    instruments.Add(CreateInstrument(rowData, i));
                            }
                            Console.WriteLine("\n\n");
                        }
                    }
                }
            }

            instruments.ForEach(instrument =>
            {
                Console.WriteLine(instrument != null ? instrument : "NULL");
                if(instrument != null)
                    instrumentRepository.CreateInstrumentWithANumber(instrument);
            });

            return errorMessages;
        }

        private Instrument CreateInstrument(Dictionary<string,string> rowData, int sheetId)
        {
            if (Enum.TryParse(typeof(Gerivize.EnumTypes.TestType), rowData["Test"], out object parsedValue))
            {
                TestType type = (TestType)parsedValue;
                int unixThing = 0;
                DateTime fromDate = int.TryParse(rowData["Last Cal. Date"], out unixThing) ? DateTime.FromOADate(Convert.ToInt32(rowData["Last Cal. Date"])) : DateTime.ParseExact("01/01/1970", "MM/dd/yyyy", new CultureInfo("da-DK"));
                DateTime toDate = int.TryParse(rowData["Next Cal. date"], out unixThing) ? DateTime.FromOADate(Convert.ToInt32(rowData["Next Cal. date"])) : DateTime.ParseExact("01/01/3000", "MM/dd/yyyy", new CultureInfo("da-DK"));
                int valueTing = 0;
                Console.WriteLine(fromDate.ToString() + " | " + sheetId);
                Console.WriteLine(rowData["Last Cal. Date"] + " | " + rowData["Next Cal. date"]);
                return new Instrument()
                {
                    ANumber = rowData["Bolls No."],
                    InstrumentName = rowData["Instrument"],
                    Manufacturer = rowData["Manufacturer"],
                    SerialNumber = rowData["Ser.No."],
                    Type = rowData["Type"],
                    Test = type,
                    LastCalibratedDate = fromDate,
                    NextCalibrationDate = toDate,
                    Value = rowData.ContainsKey("Værdi") && int.TryParse(rowData["Værdi"], out valueTing) ? Convert.ToInt32(rowData["Værdi"]) : 0,
                    Inactive = sheetId == 3 ? false : true,
                    NeedsCalibration = fromDate.Year == 1970 ? false : true,
                    ExternalCalibration = ExternalCalibration(rowData["Ref. Cal. Instruction"]) || ExternalCalibration(rowData["Cal. Data (Intern. Cal.)"]),
                    Comment = rowData["Ref. Cal. Instruction"] + "\n" + rowData["Cal. Data (Intern. Cal.)"],
                    UserId = repository.getAll().ToList().Find(u => u.SuperUser == true).Id,
                    Image = ""
                };
            }
            return null;
        }

        private bool ExternalCalibration(string string1)
        {
            return (string1.Contains("External"));
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

        private string GetWorksheetName(WorkbookPart workbookPart, WorksheetPart worksheetPart)
        {
            string relationshipId = workbookPart.GetIdOfPart(worksheetPart);
            Sheet sheet = workbookPart.Workbook.Sheets.Elements<Sheet>().FirstOrDefault(s => s.Id == relationshipId);
            if (sheet != null)
            {
                return sheet.Name;
            }
            return string.Empty;
        }

        private string GetCellLetter(string cellReference)
        {
            // Remove digits from cell reference to get the letter (column name)
            string cellLetter = new string(cellReference.Where(char.IsLetter).ToArray());
            return cellLetter;
        }

        private uint GetRowNumber(string cellReference)
        {
            // Extract digits from cell reference to get the row number
            string rowNumberString = new string(cellReference.Where(char.IsDigit).ToArray());
            uint rowNumber = uint.Parse(rowNumberString);
            return rowNumber;
        }

        private bool ValidateColumnName(string column)
        {
            bool match = false;
            columnNames.ForEach(columnName =>
            {
                if(column == columnName) match = true;
            });
            return match;
        }
    }
}
