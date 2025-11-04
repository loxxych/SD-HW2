using Newtonsoft.Json;
using SD_HW2.FileWork.ExportService;

namespace SD_HW2.Operation;

public class Operation : IComponent
{
    [JsonProperty("id")]
    public int Id { get; init; } = Guid.NewGuid().GetHashCode();
    
    [JsonIgnore]
    public BankAccount.BankAccount BankAccount { get; set; }
    
    [JsonProperty("bankAccountName")]
    public string BankAccountName => BankAccount.Name;
    
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    [JsonProperty("date")]
    public DateTime Date { get; init; }
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("category")]
    public Category.Category Category { get; set; }
    
    [JsonIgnore]
    public TimeSpan TimeToComplete { get; set; }

    public Operation() { }
    
    public Operation(double amount, BankAccount.BankAccount bankAccount, DateTime date, string? description, Category.Category category)
    {
        BankAccount = bankAccount;
        Amount = amount;
        Date = date;
        Description = description;
        Category = category;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}