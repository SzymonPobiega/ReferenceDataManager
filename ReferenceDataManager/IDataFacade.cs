namespace ReferenceDataManager
{
    public interface IDataFacade
    {
        ObjectState GetById(ChangeSetId changeSetId, ObjectId objectId);
    }
}