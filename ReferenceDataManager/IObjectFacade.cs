namespace ReferenceDataManager
{
    public interface IObjectFacade
    {
        IObjectSpaceSnapshot GetSnapshot(ChangeSetId changeSetId);
        IUpdatableObjectSpaceSnapshot GetSnapshot(UncommittedChangeSet pendingChanges);
        void Commit(UncommittedChangeSet newChangeSet);
    }
}