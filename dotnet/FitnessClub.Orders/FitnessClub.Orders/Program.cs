using ClosedXML.Excel;
using System.Text.Encodings.Web;
using System.Text.Json;
using FitnessClub.Orders;

const string ExchangeFolderPath = @"C:\FitnessClubExchange";
const string InputFileName = "orders.xlsx";
const string OutputFileName = "orders.json";
const int HeaderRowNumber = 1;
const int FirstDataRowNumber = 2;
const int FullNameColumnNumber = 1;
const int PhoneColumnNumber = 2;
const int MembershipTypeColumnNumber = 3;

Directory.CreateDirectory(ExchangeFolderPath);

string inputFilePath = Path.Combine(ExchangeFolderPath, InputFileName);
string outputFilePath = Path.Combine(ExchangeFolderPath, OutputFileName);


using var workbook = new XLWorkbook(inputFilePath);
var worksheet = workbook.Worksheet(1);

var lastRowUsed = worksheet.LastRowUsed();
int lastRowNumber = lastRowUsed?.RowNumber() ?? HeaderRowNumber;

var orders = new List<Order>();

for (int rowNumber = FirstDataRowNumber; rowNumber <= lastRowNumber; rowNumber++)
{
    var row = worksheet.Row(rowNumber);

    string fullName = row.Cell(FullNameColumnNumber).GetString().Trim();
    string phone = row.Cell(PhoneColumnNumber).GetString().Trim();
    string membershipTypeName = row.Cell(MembershipTypeColumnNumber).GetString().Trim();

    bool isRowEmpty = string.IsNullOrWhiteSpace(fullName) && string.IsNullOrWhiteSpace(phone) && string.IsNullOrWhiteSpace(membershipTypeName);

    if (isRowEmpty)
        continue;
    
    orders.Add(new Order(fullName, phone, membershipTypeName));
}

var serializerOptions = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
};

string json = JsonSerializer.Serialize(orders, serializerOptions);

File.WriteAllText(outputFilePath, json);

Console.WriteLine($"Заявок прочитано: {orders.Count}");
Console.WriteLine($"Файл с заявками создан: {outputFilePath}");