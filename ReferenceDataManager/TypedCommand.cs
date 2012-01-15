namespace ReferenceDataManager
{
    public abstract class TypedCommand<T> : AbstractCommand
    {
        protected TypedCommand(ObjectId targetObjectId) : base(targetObjectId)
        {
        }
    }

    public interface ITypedCommandHandler<in TCommand, TTarget> : ICommandHandler<TCommand>
        where TCommand : TypedCommand<TTarget>
    {
    }
}