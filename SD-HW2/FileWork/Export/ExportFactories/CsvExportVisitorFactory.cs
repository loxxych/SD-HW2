using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork.ExportService.ExportFactories;

/// <summary>
/// Фабрика посетителей-экспортеров csv формата
/// </summary>
public class CsvExportVisitorFactory : IExportVisitorFactory
{
    public ExportVisitor CreateVisitor(IFile file)
    {
        return new CsvExportVisitor(file);
    }
}