namespace ReferenceDataManager
{
    public interface IDataFacade
    {
        ObjectState GetById(ObjectId objectId, ChangeSetId changeSetId);
        ObjectState GetById(ObjectId objectId, UncommittedChangeSet pendingChanges);
        void Commit(UncommittedChangeSet pendingChanges);
    }
}