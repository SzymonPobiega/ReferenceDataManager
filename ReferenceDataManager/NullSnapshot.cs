using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public sealed class NullSnapshot : ISnapshot
    {
        public static readonly ISnapshot Instance = new NullSnapshot();

        private NullSnapshot()
        {
        }

        public ObjectState GetById(ObjectId objectId)
        {
            return null;
        }

        public IEnumerable<ObjectState> Enumerate()
        {
            yield break;
        }
    }
}