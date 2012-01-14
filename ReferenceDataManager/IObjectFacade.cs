namespace ReferenceDataManager
{
    public interface IObjectFacade
    {
        IObjectSpaceSnapshot GetSnapshot(ChangeSetId changeSetId);
        IObjectSpaceSnapshot GetSnapshot(UncommittedChangeSet pendingChanges);
        void Commit(UncommittedChangeSet newChangeSet);
    }
}