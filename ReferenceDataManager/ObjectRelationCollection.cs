using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectRelationCollection
    {
        private readonly Dictionary<string, List<Guid>> relations = new Dictionary<string, List<Guid>>();
        private static readonly Guid[] emptyRelatedList = new Guid[] {};

        public void Attach(Guid refereeObjectId, string relationName)
        {
            List<Guid> existingRelation;
            if (relations.TryGetValue(relationName, out existingRelation))
            {
                existingRelation.Add(refereeObjectId);
            }
            else
            {
                var newRelation = new List<Guid> {refereeObjectId};
                relations[relationName] = newRelation;
            }
        }

        public IEnumerable<Guid> GetRelated(string relationName)
        {
            List<Guid> existingRelation;
            if (relations.TryGetValue(relationName, out existingRelation))
            {
                return existingRelation;
            }
            return emptyRelatedList;
        }
    }
}