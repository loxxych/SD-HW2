using SD_HW2.BankAccount;
using Type = SD_HW2.Category.Type;

namespace SD_HW2.Operation;

public class OperationRepository
{
    private IOperationFactory _operationFactory;
    private BankAccountManager _bankAccountManager;
    
    public List<Operation> Operations { get; }
    
    public List<String> OperationsInfo
    {
        get
        {
            List<String> ops = [];
            
            foreach (var op in Operations)
            {
                var id = op.Id.ToString();
                var amount = op.Amount.ToString("C");
                var account = op.BankAccount.Name;
                var date = op.Date.ToString("yyyy/MM/dd");
                
                var opStr = id + ": Сумма - " + amount + ", Счет - " + account + ", Дата: " + date;
                
                ops.Add(opStr);
            }

            return ops;
        }
    }
    public OperationRepository(IOperationFactory operationFactory,  BankAccountManager bankAccountManager)
    {
        _operationFactory = operationFactory;
        _bankAccountManager = bankAccountManager;
        Operations = [];
    }

    public Operation OperationById(int id)
    {
        foreach (var op in Operations.Where(op => op.Id == id))
        {
            return op;
        }

        throw new ArgumentOutOfRangeException($"Нет существует операции с ID {id}");
    }
    
    public void AddOperation(double amount, BankAccount.BankAccount bankAccount, string? description, Category.Category category)
    {
        var date = DateTime.Now;
        var operation = _operationFactory.createOperation(amount, bankAccount, date, description, category);

        if (category.Type == Type.Withdrawal)
        {
            _bankAccountManager.Withdraw(bankAccount, amount);
        }
        else
        {
            _bankAccountManager.Deposit(bankAccount, amount);
        }
        
        Operations.Add(operation);
    }

    public void RemoveOperation(int id)
    {
        var found = false;
        foreach (var operation in Operations.Where(operation => operation.Id == id))
        {
            Operations.Remove(operation);
            found = true;
            break;
        }

        if (!found)
        {
            throw new ArgumentOutOfRangeException($"Не существует операции с id {id} ");
        }
    }
}