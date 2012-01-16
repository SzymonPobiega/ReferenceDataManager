using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface IDataStore
    {
        IEnumerable<ChangeSet> LoadAllChangeSets();
        void Store(UncommittedChangeSet changeSet);
    }
}