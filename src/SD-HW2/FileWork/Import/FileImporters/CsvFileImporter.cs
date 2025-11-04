using System.Globalization;
using CsvHelper;
using SD_HW2.FileWork.Files.Maps;

namespace SD_HW2.FileWork.Import.FileImporters;

/// <summary>
/// Импортер из csv файлов
/// </summary>
public class CsvFileImporter : FileImporter
{
    /// <summary>
    /// Читает информацию об операциях из файла
    /// </summary>
    /// <returns>Список извлеченных операций</returns>
    protected override List<Operation.Operation> ReadInfoFromFile()
    {
        using (var csv = new CsvReader(Reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<OperationMap>();
            var operations = csv.GetRecords<Operation.Operation>().ToList();
            return operations;
        }
    }
}