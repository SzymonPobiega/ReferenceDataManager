using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class DataFacade : IDataFacade
    {
        private readonly Dictionary<ChangeSetId, Snapshot> snapshots = new Dictionary<ChangeSetId, Snapshot>();

        public ObjectState GetById(ChangeSetId changeSetId, ObjectId objectId)
        {
            Snapshot snapshot;
            if (!snapshots.TryGetValue(changeSetId, out snapshot))
            {
                throw new InvalidOperationException(string.Format("Cannot load state of object {0} in context of non-existing change set {1}", objectId, changeSetId));
            }
            return snapshot.GetById(objectId);
        }

        public void LoadChangeSet(ChangeSet changeSet)
        {
            if (snapshots.ContainsKey(changeSet.Id))
            {
                throw new InvalidOperationException(string.Format("Another change set with the id {0} has already been loaded.", changeSet.Id));
            }
            snapshots[changeSet.Id] = CreateSnapshot(changeSet);
        }

        private Snapshot CreateSnapshot(ChangeSet changeSet)
        {
            Snapshot snapshot;
            if (changeSet.ParentId.HasValue)
            {
                var parent = snapshots[changeSet.ParentId.Value];
                snapshot = new Snapshot(parent);
            }
            else
            {
                snapshot = new Snapshot();
            }
            foreach (var command in changeSet.Commands)
            {
                snapshot.Load(command);
            }
            return snapshot;
        }
    }
}