namespace ReferenceDataManager
{
    public interface ISnapshotFactory
    {
        ISnapshot CreateSnapshot(ISnapshot parent, ICommandExecutor commandExecutor, IChangeSet changeSet);
    }
}