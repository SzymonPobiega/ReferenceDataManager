using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class PendingChangesDataRetrievalStrategy : IDataRetrievalStrategy
    {
        private readonly IDataFacade dataFacade;
        private readonly UncommittedChangeSet pendingChanges;

        public PendingChangesDataRetrievalStrategy(IDataFacade dataFacade, UncommittedChangeSet pendingChanges)
        {
            this.dataFacade = dataFacade;
            this.pendingChanges = pendingChanges;
        }

        public ObjectState GetById(ObjectId objectId)
        {
            return dataFacade.GetById(objectId, pendingChanges);
        }

        public IEnumerable<ObjectState> ListByType(ObjectTypeId objectTypeId)
        {
            return dataFacade.ListByType(objectTypeId, pendingChanges);
        }
    }
}