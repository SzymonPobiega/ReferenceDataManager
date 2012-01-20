using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class DataFacade : IDataFacade
    {
        private readonly SnapshotCache snapshots;
        private readonly IDataStore dataStore;

        public DataFacade(ICommandExecutor commandExecutor, IDataStore dataStore)
        {
            this.dataStore = dataStore;
            this.snapshots = new SnapshotCache(dataStore, commandExecutor);
        }

        public IEnumerable<ObjectState> ListByType(ObjectTypeId objectTypeId, ChangeSetId changeSetId)
        {
            var snapshot = snapshots.GetById(changeSetId);
            return snapshot.ListByType(objectTypeId);
        }

        public IEnumerable<ObjectState> ListByType(ObjectTypeId objectTypeId, UncommittedChangeSet pendingChanges)
        {
            var snapshot = snapshots.Create(pendingChanges);
            return snapshot.ListByType(objectTypeId);
        }

        public ObjectState GetById(ObjectId objectId, ChangeSetId changeSetId)
        {
            var snapshot = snapshots.GetById(changeSetId);
            return snapshot.GetById(objectId);
        }       

        public ObjectState GetById(ObjectId objectId, UncommittedChangeSet pendingChanges)
        {
            var snapshot = snapshots.Create(pendingChanges);
            return snapshot.GetById(objectId);
        }

        public void Commit(UncommittedChangeSet pendingChanges)
        {
            var snapshot = snapshots.Create(pendingChanges);
            try
            {
                dataStore.Store(pendingChanges);
                snapshots.Add(pendingChanges.Id, snapshot);
            }
            catch (Exception)
            {
                snapshots.Invalidate(snapshot);
                throw;
            }            
        }
    }
}