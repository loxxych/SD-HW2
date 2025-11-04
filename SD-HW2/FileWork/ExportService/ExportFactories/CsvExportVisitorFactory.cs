namespace SD_HW2.FileWork.ExportService.ExportFactories;

public class CsvExportVisitorFactory : ExportVisitorFactory
{
    public ExportVisitor CreateVisitor(IFile file)
    {
        return new CsvExportVisitor(file);
    }
}