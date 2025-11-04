using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork.ExportService.ExportFactories;

/// <summary>
/// Интерфейс фабрики посетителей
/// </summary>
public interface IExportVisitorFactory
{
    public ExportVisitor CreateVisitor(IFile file);
}