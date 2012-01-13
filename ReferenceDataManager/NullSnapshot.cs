using System;

namespace ReferenceDataManager
{
    public sealed class NullSnapshot : ISnapshot
    {
        public DataObject GetById(Guid objectId)
        {
            return null;
        }
    }
}