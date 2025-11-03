namespace SD_HW2.Operation.Commands;

public class AddOperationCommandTimeMeasurer : AddOperationCommandDecorator
{
    public AddOperationCommandTimeMeasurer(ICommand command) : base(command) { }

    public void Execute()
    {
        
    }
}