using System;

namespace ReferenceDataManager
{
    public class IncrementalCachingSnapshotFactory : ISnapshotFactory
    {
        public ISnapshot CreateSnapshot(ISnapshot parent, ICommandExecutor commandExecutor, IChangeSet changeSet)
        {
            return new Snapshot(parent, commandExecutor, changeSet);
        }
    }
}