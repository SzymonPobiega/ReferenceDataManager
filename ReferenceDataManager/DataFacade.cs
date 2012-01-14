using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class DataFacade : IDataFacade
    {
        private readonly Dictionary<ChangeSetId, Snapshot> snapshots = new Dictionary<ChangeSetId, Snapshot>();

        public ObjectState GetById(ChangeSetId changeSetId, ObjectId objectId)
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