using System;

namespace ReferenceDataManager
{
    public interface ISnapshot
    {
        ObjectState GetById(ObjectId objectId);
    }
}