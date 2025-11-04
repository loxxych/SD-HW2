using SD_HW2.BankAccount;
using SD_HW2.Category;
using SD_HW2.Factories.OperationFactory;
using Type = SD_HW2.Category.Type;

namespace SD_HW2.Operation;

/// <summary>
/// Представляет фасад-репозиторий операций
/// </summary>
public static class OperationRepository
{
    /// <summary>
    /// Фабрика операций
    /// </summary>
    private static readonly IOperationFactory OperationFactory = new OperationFactory();
    
    /// <summary>
    /// Произведенные операции
    /// </summary>
    public static List<Operation> Operations { get; } = [];
    
    /// <summary>
    /// Составляет список с информацией об операциях в виде строк
    /// </summary>
    public static List<string> OperationsInfo
    {
        get
        {
            List<string> ops = [];
            
            foreach (var op in Operations)
            {
                var id = op.Id.ToString();
                var amount = op.Amount.ToString("C");
                var account = op.BankAccount.Name;
                var date = op.Date.ToString("yyyy-MM-ddTHH:mm:ss");
                
                var opStr = id + ": Сумма - " + amount + ", Счет - " + account + ", Дата: " + date;
                
                ops.Add(opStr);
            }

            return ops;
        }
    }
    
    /// <summary>
    /// Находит операцию с данным ID
    /// </summary>
    /// <param name="id">ID искомой операции</param>
    /// <returns>Найденную операцию</returns>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если операция не была найдена</exception>
    public static Operation OperationById(int id)
    {
        foreach (var op in Operations.Where(op => op.Id == id))
        {
            return op;
        }

        throw new ArgumentOutOfRangeException($"Нет существует операции с ID {id}");
    }
    
    /// <summary>
    /// Добавляет операцию
    /// </summary>
    /// <param name="amount">Сумма</param>
    /// <param name="bankAccount">Счет</param>
    /// <param name="description">Описание</param>
    /// <param name="category">Категория</param>
    
    public static void AddOperation(double amount, BankAccount.BankAccount bankAccount, string? description, Category.Category category)
    {
        var date = DateTime.Now;
        var operation = OperationFactory.CreateOperation(amount, bankAccount, date, description, category);

        if (category.Type == Type.Withdrawal)
        {
            BankAccountManager.Withdraw(bankAccount, amount);
        }
        else
        {
            BankAccountManager.Deposit(bankAccount, amount);
        }
        
        Operations.Add(operation);
    }

    /// <summary>
    /// Удаляет операцию
    /// </summary>
    /// <param name="id">ID удаляемой операции</param>
    public static void RemoveOperation(int id)
    {
        var operation = OperationById(id);

        if (operation.Category.Type == Type.Withdrawal)
        { 
            BankAccountManager.Deposit(operation.BankAccount, operation.Amount);
        }
        else
        { 
            BankAccountManager.Withdraw(operation.BankAccount, operation.Amount);
        }
    }
    
    /// <summary>
    /// Заменяет все операции новыми
    /// </summary>
    /// <param name="operations">Новые операции</param>
    public static void ReplaceOperations(List<Operation> operations)
    {
        Operations.Clear();
        
        foreach (var operation in operations)
        {
            if (!CategoryRepository.Categories.Contains(operation.Category))
            {
                CategoryRepository.Categories.Add(operation.Category);
            }
            if (!BankAccountRepository.BankAccounts.Contains(operation.BankAccount))
            {
                BankAccountRepository.BankAccounts.Add(operation.BankAccount);
            }
            
            AddOperation(operation.Amount, operation.BankAccount, operation.Description, operation.Category);
        }
    }

    /// <summary>
    /// Пересчитывает балансы счетов у операций с переданной категорией
    /// </summary>
    /// <param name="category">Категория операций, которые нужно пересчитать</param>
    public static void RefactorOperationsWithCategory(Category.Category category)
    {
        foreach (var operation in Operations)
        {
            if (operation.Category != category) continue;
            
            if (operation.Category.Type == Type.Withdrawal)
            {
                BankAccountManager.Withdraw(operation.BankAccount, 2 * operation.Amount);
            }
            else if (operation.Category.Type == Type.Deposit)
            {
                BankAccountManager.Deposit(operation.BankAccount, 2 * operation.Amount);
            }
        }
    }
}