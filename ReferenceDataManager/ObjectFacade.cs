using System;

namespace ReferenceDataManager
{
    public class ObjectFacade : IObjectFacade
    {
        private readonly IDataFacade dataFacade;
        private readonly ICommandExecutor commandExecutor;
        private readonly ObjectTypeDescriptorRepository objectTypeDescriptorRepository;

        public ObjectFacade(IDataFacade dataFacade, ObjectTypeDescriptorRepository objectTypeDescriptorRepository, ICommandExecutor commandExecutor)
        {
            this.dataFacade = dataFacade;
            this.commandExecutor = commandExecutor;
            this.objectTypeDescriptorRepository = objectTypeDescriptorRepository;
        }

        public IObjectSpaceSnapshot GetSnapshot(ChangeSetId changeSetId)
        {
            return new ObjectSpaceSnapshot(objectTypeDescriptorRepository, new PersistentDataRetrievalStrategy(dataFacade, changeSetId));
        }

        public IUpdatableObjectSpaceSnapshot GetSnapshot(UncommittedChangeSet pendingChanges)
        {
            return new UpdatableObjectSpaceSnapshot(objectTypeDescriptorRepository, new PendingChangesDataRetrievalStrategy(dataFacade, pendingChanges), commandExecutor);
        }

        public void Commit(UncommittedChangeSet newChangeSet)
        {
            dataFacade.Commit(newChangeSet);
        }
    }
}