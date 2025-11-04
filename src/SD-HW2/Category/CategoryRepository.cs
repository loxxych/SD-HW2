using SD_HW2.Factories.CategoryFactory;
using SD_HW2.Operation;

namespace SD_HW2.Category;

/// <summary>
/// Представляет фасад-репозиторий категорий
/// </summary>
public static class CategoryRepository
{
    /// <summary>
    /// Фабрика для создания категорий
    /// </summary>
    private static readonly CategoryFactory CategoryFactory = new();
    
    /// <summary>
    /// Доступные категории
    /// </summary>
    public static List<Category> Categories { get; } = [];
    
    /// <summary>
    /// Список названий категорий
    /// </summary>
    public static List<string> CategoriesNames
    {
        get
        {
            List<string> categoriesNames = [];
            categoriesNames.AddRange(Categories.Select(category => category.Name));

            return categoriesNames;
        }
    }

    /// <summary>
    /// Формирует список категорий определенного типа
    /// </summary>
    /// <param name="typeStr">Тип</param>
    /// <returns>Список категорий данного типа</returns>
    public static List<string> CategoriesByType(string typeStr)
    {
        List<string> categoriesNames = [];

        var type = typeStr == "Расход" ? Type.Withdrawal : Type.Deposit;

        categoriesNames.AddRange(from category in Categories where category.Type == type select category.Name);

        return categoriesNames;
    }
    
    
    /// <summary>
    /// Добавляет новую категорию
    /// </summary>
    /// <param name="type">Тип категории</param>
    /// <param name="name">Название категории</param>
    public static void AddCategory(Type type, string name)
    {
        var category = CategoryFactory.CreateCategory(type, name);
        Categories.Add(category);
    }

    /// <summary>
    /// Удаляет категорию
    /// </summary>
    /// <param name="name">Название категории</param>
    public static void RemoveCategory(string name)
    {
        var category = FindCategory(name);
        Categories.Remove(category);
    }
    
    /// <summary>
    /// Находит категорию по названию (с учетом того, что все названия различны)
    /// </summary>
    /// <param name="name">Название категории</param>
    /// <returns>Найденную категорию</returns>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если не нашлось категории с данным названием</exception>
    public static Category FindCategory(string name)
    {
        foreach (var category in Categories.Where(category => category.Name == name))
        {
            return category;
        }

        throw new ArgumentOutOfRangeException($"Не существует категории с именем {name}");
    }
}