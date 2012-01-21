using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class PersistentDataRetrievalStrategy : IDataRetrievalStrategy
    {
        private readonly IDataFacade dataFacade;
        private readonly ChangeSetId lastedChangeSetId;

        public PersistentDataRetrievalStrategy(IDataFacade dataFacade, ChangeSetId lastedChangeSetId)
        {
            this.dataFacade = dataFacade;
            this.lastedChangeSetId = lastedChangeSetId;
        }

        public ObjectState GetById(ObjectId objectId)
        {
            return dataFacade.GetById(objectId, lastedChangeSetId);
        }

        public IEnumerable<ObjectState> Enumerate()
        {
            return dataFacade.Enumerate(lastedChangeSetId);
        }
    }
}