using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface IDataStore
    {
        IEnumerable<ChangeSet> LoadAll();
        void Store(UncommittedChangeSet changeSet);
    }
}