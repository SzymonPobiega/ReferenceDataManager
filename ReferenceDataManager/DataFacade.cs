using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class DataFacade
    {
        private readonly Dictionary<Guid, Snapshot> snapshots = new Dictionary<Guid, Snapshot>();

        public object GetById(Guid changeSetId, Guid objectId)
        {
            var snapshot = snapshots[changeSetId];
            return snapshot.GetById(objectId);
        }

        public void LoadChangeSet(ChangeSet changeSet)
        {
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