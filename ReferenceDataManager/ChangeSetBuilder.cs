namespace ReferenceDataManager
{
    public class ChangeSetBuilder
    {
        private readonly UncommittedChangeSet pendingChanges;
        private readonly IUpdatableObjectSpaceSnapshot updatableSnapshot;

        public ChangeSetBuilder(IObjectFacade objectFacade, ChangeSetId? parentId)
        {
            pendingChanges = new UncommittedChangeSet(parentId);
            updatableSnapshot = objectFacade.GetSnapshot(pendingChanges);
        }

        public UncommittedChangeSet PendingChanges
        {
            get { return pendingChanges; }
        }

        public T AddCommandAndPreviewTarget<T>(AbstractCommand command)
        {
            return Add(command).GetPreview().GetById<T>(command.TargetObjectId);
        }

        public ChangeSetBuilder Add(AbstractCommand command)
        {
            PendingChanges.Add(command);
            updatableSnapshot.Update(command);
            return this;
        }

        public IObjectSpaceSnapshot GetPreview()
        {
            return updatableSnapshot;
        }
    }
}