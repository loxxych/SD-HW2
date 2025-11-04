namespace SD_HW2.FileWork.ExportService;

public interface IComponent
{
    void Accept(IVisitor visitor);
}