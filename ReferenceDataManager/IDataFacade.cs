using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface IDataFacade
    {
        IEnumerable<ObjectState> Enumerate(ChangeSetId changeSetId);
        IEnumerable<ObjectState> Enumerate(UncommittedChangeSet pendingChanges);
        ObjectState GetById(ObjectId objectId, ChangeSetId changeSetId);
        ObjectState GetById(ObjectId objectId, UncommittedChangeSet pendingChanges);
        void Commit(UncommittedChangeSet pendingChanges);
    }
}