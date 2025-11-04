using SD_HW2.FileWork.ExportService;

namespace SD_HW2.BankAccount;

/// <summary>
/// Представляет банковский счет
/// </summary>
public class BankAccount : IComponent
{
    /// <summary>
    /// Уникальный ID счета
    /// </summary>
    public int Id { get; } = Guid.NewGuid().GetHashCode();
    
    /// <summary>
    /// Название счета
    /// </summary>
    public string Name { get; set; } = "N/A";
    
    /// <summary>
    /// Баланс счета
    /// </summary>
    public double Balance { get; set; } = 0;

    /// <summary>
    /// Пустой конструктор для корректной работы импорта
    /// </summary>
    public BankAccount() { }
    
    /// <summary>
    /// Конструктор банковского счета
    /// </summary>
    /// <param name="name">Название счета</param>
    public BankAccount(string name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Принимает посетителя (экспортера)
    /// </summary>
    /// <param name="visitor">Посетитель (А именно - экспортер)</param>
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}