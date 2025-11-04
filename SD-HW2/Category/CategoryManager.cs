using SD_HW2.Operation;

namespace SD_HW2.Category;

/// <summary>
/// Класс-фасад для управления полями категорий
/// </summary>
public class CategoryManager
{
    /// <summary>
    /// Меняет название категории
    /// </summary>
    /// <param name="name">Старое название</param>
    /// <param name="newName">Новое название</param>
    public static void ChangeCategoryName(string name, string newName)
    {
        var category = CategoryRepository.FindCategory(name);
        category.Name = newName;
    }
    
    /// <summary>
    /// Меняет тип категории
    /// </summary>
    /// <param name="name">Название</param>
    /// <param name="newType">Новый тип</param>
    public static void ChangeCategoryType(string name, Type newType)
    {
        var category = CategoryRepository.FindCategory(name);
        category.Type = newType;
        // Обновляем операции с такой категорией
        OperationRepository.RefactorOperationsWithCategory(category);
    }
}