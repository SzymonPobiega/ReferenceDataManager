namespace ReferenceDataManager
{
    public interface ICommandHandler<in T>
        where T : AbstractCommand
    {
        void Handle(T command, ICommandExecutionContext context);
    }
}