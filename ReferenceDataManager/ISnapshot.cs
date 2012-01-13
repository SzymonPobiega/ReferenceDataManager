using System;

namespace ReferenceDataManager
{
    public interface ISnapshot
    {
        ObjectState GetById(Guid objectId);
    }
}