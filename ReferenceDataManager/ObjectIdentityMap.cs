using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectIdentityMap
    {
        private readonly Dictionary<ObjectId, object> map = new Dictionary<ObjectId, object>();

        public object GetById(ObjectId objectId)
        {
            object existing;
            return map.TryGetValue(objectId, out existing) ? existing : null;
        }

        public void Put(ObjectId objectId, object theObject)
        {
            if (map.ContainsKey(objectId))
            {
                throw new InvalidOperationException(string.Format("Object with id {0} already present in the identity map.", objectId));
            }
            map[objectId] = theObject;
        }
    }
}