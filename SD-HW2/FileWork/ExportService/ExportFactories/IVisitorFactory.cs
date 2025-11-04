namespace SD_HW2.FileWork.ExportService.ExportFactories;

public interface ExportVisitorFactory
{
    public ExportVisitor CreateVisitor(IFile file);
}