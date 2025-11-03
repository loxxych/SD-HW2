namespace SD_HW2.Operation.Commands;

public class AddOperationCommandDecorator : ICommand
{
    private readonly ICommand _command;

    public AddOperationCommandDecorator(ICommand command)
    {
        _command = command;
    }
    
    public void Execute()
    {
        _command.Execute();
    }
}