using System;

namespace ReferenceDataManager
{
    public class ObjectFacade : IObjectFacade
    {
        private readonly IDataFacade dataFacade;
        private readonly ObjectTypeDescriptorRepository objectTypeDescriptorRepository;

        public ObjectFacade(IDataFacade dataFacade, ObjectTypeDescriptorRepository objectTypeDescriptorRepository)
        {
            this.dataFacade = dataFacade;
            this.objectTypeDescriptorRepository = objectTypeDescriptorRepository;
        }

        public IObjectSpaceSnapshot GetSnapshot(ChangeSetId changeSetId)
        {
            return new ObjectSpaceSnapshot(objectTypeDescriptorRepository, new PersistentDataRetrievalStrategy(dataFacade, changeSetId));
        }

        public IObjectSpaceSnapshot GetSnapshot(UncommittedChangeSet pendingChanges)
        {
            return new ObjectSpaceSnapshot(objectTypeDescriptorRepository, new PendingChangesDataRetrievalStrategy(dataFacade, pendingChanges));
        }

        public void Commit(UncommittedChangeSet newChangeSet)
        {
            dataFacade.Commit(newChangeSet);
        }
    }
}