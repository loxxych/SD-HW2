namespace SD_HW2.Factories.CategoryFactory;

/// <summary>
///  Фабрика категорий
/// </summary>
public class CategoryFactory : ICategoryFactory
{
    public Category.Category CreateCategory(Category.Type type, string name)
    {
        return new Category.Category(type, name);
    }
}