namespace SD_HW2.FileWork.ExportService;

/// <summary>
/// Компонент, посещаемый посетителем
/// </summary>
public interface IComponent
{
    void Accept(IVisitor visitor);
}