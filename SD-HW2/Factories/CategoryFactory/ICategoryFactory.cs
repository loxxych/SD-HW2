namespace SD_HW2.Factories.CategoryFactory;

/// <summary>
/// Интерфейс фабрики категорий
/// </summary>
public interface ICategoryFactory
{
    public Category.Category CreateCategory(Category.Type type, string name);
}