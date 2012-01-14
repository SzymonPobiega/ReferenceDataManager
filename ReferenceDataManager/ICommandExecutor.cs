namespace ReferenceDataManager
{
    public interface ICommandExecutor
    {
        void Execute(AbstractCommand command, ICommandExecutionContext context);
    }
}