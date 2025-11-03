using System.Data;
using Newtonsoft.Json;
using SD_HW2.FileWork;

namespace SD_HW2.Operation;

public class Operation : IComponent
{
    [JsonProperty("id")]
    public int Id;
    
    [JsonIgnore]
    public BankAccount.BankAccount BankAccount;
    
    [JsonProperty("bankAccountName")]
    public string BankAccountName => BankAccount.Name;
    
    [JsonProperty("amount")]
    public double Amount;
    
    [JsonProperty("date")]
    public DateTime Date;
    
    [JsonProperty("description")]
    public string? Description;
    
    [JsonProperty("category")]
    public Category.Category Category;

    public Operation()
    {
        Id = Guid.NewGuid().GetHashCode();
    }
    
    public Operation(double amount, BankAccount.BankAccount bankAccount, DateTime date, string? description, Category.Category category)
    {
        Id = Guid.NewGuid().GetHashCode();
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