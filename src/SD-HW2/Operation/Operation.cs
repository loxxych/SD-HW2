using Newtonsoft.Json;
using SD_HW2.FileWork.ExportService;

namespace SD_HW2.Operation;

/// <summary>
/// Класс операции
/// </summary>
public class Operation : IComponent
{
    /// <summary>
    /// Уникальный ID операции
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; init; } = Guid.NewGuid().GetHashCode();
    
    /// <summary>
    /// Банковский счет, на который производится операция
    /// </summary>
    [JsonIgnore]
    public BankAccount.BankAccount BankAccount { get; set; }
    
    /// <summary>
    /// Название счета
    /// </summary>
    [JsonProperty("bankAccountName")]
    public string BankAccountName => BankAccount.Name;
    
    /// <summary>
    /// Сумма операции
    /// </summary>
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    /// <summary>
    /// Дата произведения операции
    /// </summary>
    [JsonProperty("date")]
    public DateTime Date { get; init; }
    
    /// <summary>
    /// Описание операции
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Категория операции
    /// </summary>
    [JsonProperty("category")]
    public Category.Category Category { get; set; }
    
    /// <summary>
    /// Время выполнения операции (для аналитики)
    /// </summary>
    [JsonIgnore]
    public TimeSpan TimeToComplete { get; set; }

    /// <summary>
    /// Пустой конструктор для корректной работы импорта
    /// </summary>
    public Operation() { }
    
    public Operation(double amount, BankAccount.BankAccount bankAccount, DateTime date, string? description, Category.Category category)
    {
        BankAccount = bankAccount;
        Amount = amount;
        Date = date;
        Description = description;
        Category = category;
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