using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectRelationCollection
    {
        public void Attach(Guid refereeObjectId, string relationName)
        {
            
        }

        public IEnumerable<Guid> GetRelated(string relationName)
        {
            yield break;
        }
    }
}