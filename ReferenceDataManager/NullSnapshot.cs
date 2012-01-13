using System;

namespace ReferenceDataManager
{
    public sealed class NullSnapshot : ISnapshot
    {
        public ObjectState GetById(ObjectId objectId)
        {
            return null;
        }
    }
}