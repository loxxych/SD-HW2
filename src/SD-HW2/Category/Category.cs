namespace SD_HW2.Category;

/// <summary>
/// Представляет тип операции: доход или расход
/// </summary>
public enum Type
{
    Deposit,    // Доход
    Withdrawal, // Расход
}

public class Category
{
    /// <summary>
    /// Уникальный ID категории
    /// </summary>
    public int Id { get; } = Guid.NewGuid().GetHashCode();
    
    /// <summary>
    /// Тип категории: доход или расход
    /// </summary>
    public Type Type { get; set; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; set; } = "N/A";

    /// <summary>
    /// Пустой конструктор для корректной работы импорта
    /// </summary>
    public Category() { }

    /// <summary>
    /// Конструктор категории
    /// </summary>
    /// <param name="type">Тип категории</param>
    /// <param name="name">Название категории</param>
    public Category(Type type, string name)
    {
        Type = type;
        Name = name;
    }
}