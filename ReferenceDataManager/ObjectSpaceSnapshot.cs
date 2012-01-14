using System;

namespace ReferenceDataManager
{
    public class ObjectSpaceSnapshot : IObjectSpaceSnapshot
    {
        private readonly IDataFacade dataFacade;
        private readonly ChangeSetId changeSetId;

        public ObjectSpaceSnapshot(IDataFacade dataFacade, ChangeSetId changeSetId)
        {
            this.dataFacade = dataFacade;
            this.changeSetId = changeSetId;
        }

        public T GetById<T>(ObjectId objectId)
        {
            var objectState = dataFacade.GetById(changeSetId, objectId);
            return default(T);
        }
    }
}