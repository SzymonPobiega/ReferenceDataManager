using System;

namespace ReferenceDataManager
{
    public class ObjectFacade
    {
        private readonly IDataFacade dataFacade;

        public ObjectFacade(IDataFacade dataFacade)
        {
            this.dataFacade = dataFacade;
        }

        public IObjectSpaceSnapshot GetSnapshot(ChangeSetId changeSetId)
        {
            return new ObjectSpaceSnapshot(dataFacade, changeSetId);
        }
    }
}