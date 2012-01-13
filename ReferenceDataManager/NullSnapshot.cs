using System;

namespace ReferenceDataManager
{
    public sealed class NullSnapshot : ISnapshot
    {
        public ObjectState GetById(Guid objectId)
        {
            return null;
        }
    }
}