using System.Globalization;
using CsvHelper;

namespace SD_HW2.FileWork;

public class CsvFileImporter : FileImporter
{
    protected override List<Operation.Operation> ReadInfoFromFile()
    {
        using (var csv = new CsvReader(_reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<OperationMap>();
            var operations = csv.GetRecords<Operation.Operation>().ToList();
            return operations;
        }
    }
}