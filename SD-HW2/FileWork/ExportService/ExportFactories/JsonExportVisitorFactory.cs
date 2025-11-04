namespace SD_HW2.FileWork.ExportService.ExportFactories;

public class JsonExportVisitorFactory : ExportVisitorFactory
{
    public ExportVisitor CreateVisitor(IFile file)
    {
        return new JsonExportVisitor(file);
    }
}