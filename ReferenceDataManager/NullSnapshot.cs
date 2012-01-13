using System;

namespace ReferenceDataManager
{
    public sealed class NullSnapshot : ISnapshot
    {
        public object GetById(Guid objectId)
        {
            return null;
        }
    }
}