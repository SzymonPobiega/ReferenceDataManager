using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectState
    {
        private readonly Guid id;
        private readonly ObjectRelationCollection relations = new ObjectRelationCollection();

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
            relations.Attach(refereeObjectId, relationName);
        }

        public IEnumerable<Guid> GetRelated(string relationName)
        {
            return relations.GetRelated(relationName);
        }
    }
}