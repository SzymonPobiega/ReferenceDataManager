using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectRelationCollection
    {
        private readonly Dictionary<string, List<ObjectId>> relations = new Dictionary<string, List<ObjectId>>();
        private static readonly ObjectId[] emptyRelatedList = new ObjectId[] {};

        public void Attach(ObjectId refereeObjectId, string relationName)
        {
            List<ObjectId> existingRelation;
            if (relations.TryGetValue(relationName, out existingRelation))
            {
                existingRelation.Add(refereeObjectId);
            }
            else
            {
                var newRelation = new List<ObjectId> {refereeObjectId};
                relations[relationName] = newRelation;
            }
        }

        public IEnumerable<ObjectId> GetRelated(string relationName)
        {
            List<ObjectId> existingRelation;
            if (relations.TryGetValue(relationName, out existingRelation))
            {
                return new List<ObjectId>(existingRelation);
            }
            return emptyRelatedList;
        }

        public void Detach(ObjectId refereeObjectId, string relationName)
        {
            List<ObjectId> existingRelation;
            if (relations.TryGetValue(relationName, out existingRelation))
            {
                existingRelation.Remove(refereeObjectId);
            }
        }
    }
}