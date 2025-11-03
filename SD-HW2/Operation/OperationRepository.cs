namespace SD_HW2.Operation;

public class OperationRepository
{
    private IOperationFactory _operationFactory;
    public List<Operation> Operations { get; }
    
    public OperationRepository(IOperationFactory operationFactory)
    {
        _operationFactory = _operationFactory;
        Operations = [];
    }
    
    public void AddOperation(double amount, BankAccount.BankAccount bankAccount, string? description, Category.Category category)
    {
        var date = DateTime.Now;
        var operation = _operationFactory.createOperation(amount, bankAccount, date, description, category);
        Operations.Add(operation);
    }

    public void RemoveOperation(int id)
    {
        bool found = false;
        foreach (var operation in Operations)
        {
            if (operation.Id == id)
            {
                Operations.Remove(operation);
                found = true;
            }
        }

        if (!found)
        {
            throw new ArgumentOutOfRangeException($"Не существует операции с id {id} ");
        }
    }
}