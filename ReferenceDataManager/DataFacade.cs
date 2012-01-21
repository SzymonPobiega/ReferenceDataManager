using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class DataFacade : IDataFacade
    {
        private readonly SnapshotCache snapshots;
        private readonly IDataStore dataStore;

        public DataFacade(ICommandExecutor commandExecutor, IDataStore dataStore, ISnapshotFactory snapshotFactory)
        {
            this.dataStore = dataStore;
            this.snapshots = new SnapshotCache(snapshotFactory, dataStore, commandExecutor);
        }

        public IEnumerable<ObjectState> Enumerate(ChangeSetId changeSetId)
        {
            var snapshot = snapshots.GetById(changeSetId);
            return snapshot.Enumerate();
        }

        public IEnumerable<ObjectState> Enumerate(UncommittedChangeSet pendingChanges)
        {
            var snapshot = snapshots.Create(pendingChanges);
            return snapshot.Enumerate();
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