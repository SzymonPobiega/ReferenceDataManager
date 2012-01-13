using System;

namespace ReferenceDataManager
{
    public interface ISnapshot
    {
        DataObject GetById(Guid objectId);
    }
}