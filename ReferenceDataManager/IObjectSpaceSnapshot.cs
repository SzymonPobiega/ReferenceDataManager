namespace ReferenceDataManager
{
    public interface IObjectSpaceSnapshot
    {
        T GetById<T>(ObjectId objectId);
    }
}