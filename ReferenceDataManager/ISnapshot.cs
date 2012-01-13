using System;

namespace ReferenceDataManager
{
    public interface ISnapshot
    {
        object GetById(Guid objectId);
    }
}