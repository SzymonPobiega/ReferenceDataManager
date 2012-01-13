using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectRelationCollection
    {
        
    }

    public class ObjectState
    {
        private readonly Guid id;

        public ObjectState(Guid id)
        {
            this.id = id;
        }

        public Guid Id
        {
            get { return id; }
        }

        public void Attach(Guid refereeObjectId, string relationName)
        {
        }

        public IEnumerable<Guid> GetRelated(string relationName)
        {
            yield break;
        }
    }
}