using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface ISnapshot
    {
        ObjectState GetById(ObjectId objectId);
        IEnumerable<ObjectState> Enumerate();
    }
}