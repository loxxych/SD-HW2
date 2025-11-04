using SD_HW2.FileWork.ExportService.Visitors;
using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork.ExportService.ExportFactories;

/// <summary>
/// Фабрика посетителей-экспортеров json формата
/// </summary>
public class JsonExportVisitorFactory : IExportVisitorFactory
{
    public ExportVisitor CreateVisitor(IFile file)
    {
        return new JsonExportVisitor(file);
    }
}