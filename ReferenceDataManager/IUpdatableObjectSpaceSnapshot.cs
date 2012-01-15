namespace ReferenceDataManager
{
    public interface IUpdatableObjectSpaceSnapshot : IObjectSpaceSnapshot
    {
        void Update(AbstractCommand command);
    }
}