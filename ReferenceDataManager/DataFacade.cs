using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class DataFacade
    {
        private readonly Dictionary<Guid, Snapshot> snapshots = new Dictionary<Guid, Snapshot>();

        public object GetById(Guid objectId, Guid id)
        {
            return null;
        }

        public void LoadChangeSet(ChangeSet changeSet)
        {
            snapshots[changeSet.Id] = CreateSnapshot(changeSet);
        }

        private Snapshot CreateSnapshot(ChangeSet changeSet)
        {
            throw new NotImplementedException();
        }
    }
}