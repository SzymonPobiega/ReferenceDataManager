using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface IDataFacade
    {
        IEnumerable<ObjectState> ListByType(ObjectTypeId objectTypeId, ChangeSetId changeSetId);
        IEnumerable<ObjectState> ListByType(ObjectTypeId objectTypeId, UncommittedChangeSet pendingChanges);
        ObjectState GetById(ObjectId objectId, ChangeSetId changeSetId);
        ObjectState GetById(ObjectId objectId, UncommittedChangeSet pendingChanges);
        void Commit(UncommittedChangeSet pendingChanges);
    }
}