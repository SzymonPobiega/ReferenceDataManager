using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface IDataRetrievalStrategy
    {
        ObjectState GetById(ObjectId objectId);
        IEnumerable<ObjectState> Enumerate();
    }
}