using System;

namespace ReferenceDataManager
{
    public class ObjectFacade
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
            return new ObjectSpaceSnapshot(dataFacade, objectTypeDescriptorRepository, changeSetId);
        }
    }
}