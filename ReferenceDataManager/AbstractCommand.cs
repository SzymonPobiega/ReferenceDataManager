namespace ReferenceDataManager
{
    public abstract class AbstractCommand
    {
        public abstract void Execute(ICommandExecutionContext context);
    }
}