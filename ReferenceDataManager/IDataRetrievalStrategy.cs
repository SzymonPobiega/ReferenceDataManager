namespace ReferenceDataManager
{
    public interface IDataRetrievalStrategy
    {
        ObjectState GetById(ObjectId objectId);
    }
}