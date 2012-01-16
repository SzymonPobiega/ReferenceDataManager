using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class SnapshotCache
    {
        private readonly object synchRoot = new object();
        private Dictionary<ChangeSetId, ISnapshot> snapshots;
        private readonly IDataStore dataStore;
        private readonly ICommandExecutor commandExecutor;

        public SnapshotCache(IDataStore dataStore, ICommandExecutor commandExecutor)
        {
            this.dataStore = dataStore;
            this.commandExecutor = commandExecutor;
        }

        public ISnapshot GetById(ChangeSetId changeSetId)
        {
            lock (synchRoot)
            {
                EnsureLoaded();
                return GetSnapshot(changeSetId);
            }
        }        

        public ISnapshot Create(UncommittedChangeSet pendingChanges)
        {
            lock (synchRoot)
            {
                EnsureLoaded();
                return CreateSnapshot(pendingChanges);
            }
        }

        public void Add(ChangeSetId changeSetId, ISnapshot snapshot)
        {
            lock (synchRoot)
            {
                if (snapshots.ContainsKey(changeSetId))
                {
                    throw new InvalidOperationException(
                        string.Format("Another change set with the id {0} has already been loaded.", changeSetId));
                }
                snapshots[changeSetId] = snapshot;
            }
        }

        public void Invalidate(ISnapshot tentativeSnapshot)
        {
            lock (synchRoot)
            {
                snapshots = null;
            }
        }
        
        private void EnsureLoaded()
        {
            if (snapshots != null)
            {
                return;
            }
            snapshots = new Dictionary<ChangeSetId, ISnapshot>();
            var changeSets = dataStore.LoadAllChangeSets();
            foreach (var changeSet in changeSets)
            {
                var snapshot = CreateSnapshot(changeSet);
                if (snapshots.ContainsKey(changeSet.Id))
                {
                    throw new InvalidOperationException(string.Format("Another change set with the id {0} has already been loaded.", changeSet.Id));
                }
                snapshots[changeSet.Id] = snapshot;
            }
        }

        private ISnapshot GetSnapshot(ChangeSetId changeSetId)
        {
            ISnapshot snapshot;
            if (!snapshots.TryGetValue(changeSetId, out snapshot))
            {
                throw new InvalidOperationException(string.Format("Requested change set {0} has not been loaded.", changeSetId));
            }
            return snapshot;
        }

        private Snapshot CreateSnapshot(IChangeSet changeSet)
        {
            var snapshot = changeSet.ParentId.HasValue
                                    ? new Snapshot(GetSnapshot(changeSet.ParentId.Value), commandExecutor)
                                    : new Snapshot(commandExecutor);
            foreach (var command in changeSet.Commands)
            {
                snapshot.Load(command);
            }
            return snapshot;
        }
    }
}