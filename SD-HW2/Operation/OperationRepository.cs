using SD_HW2.BankAccount;
using Type = SD_HW2.Category.Type;

namespace SD_HW2.Operation;

public static class OperationRepository
{
    private static readonly IOperationFactory _operationFactory = new OperationFactory();
    public static List<Operation> Operations { get; private set; } = [];
    
    public static List<String> OperationsInfo
    {
        get
        {
            List<String> ops = [];
            
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
    
    public static Operation OperationById(int id)
    {
        foreach (var op in Operations.Where(op => op.Id == id))
        {
            return op;
        }

        throw new ArgumentOutOfRangeException($"Нет существует операции с ID {id}");
    }
    
    public static void AddOperation(double amount, BankAccount.BankAccount bankAccount, string? description, Category.Category category)
    {
        var date = DateTime.Now;
        var operation = _operationFactory.CreateOperation(amount, bankAccount, date, description, category);

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

    public static void RemoveOperation(int id)
    {
        var found = false;
        foreach (var operation in Operations.Where(operation => operation.Id == id))
        {
            Operations.Remove(operation);

            if (operation.Category.Type == Type.Withdrawal)
            {
                BankAccountManager.Deposit(operation.BankAccount, operation.Amount);
            }
            else
            {
                BankAccountManager.Withdraw(operation.BankAccount, operation.Amount);
            }
            
            found = true;
            break;
        }

        if (!found)
        {
            throw new ArgumentOutOfRangeException($"Не существует операции с id {id} ");
        }
    }
    
    public static void AddOperations(List<Operation> operations)
    {
        Operations =  operations;
    }
}