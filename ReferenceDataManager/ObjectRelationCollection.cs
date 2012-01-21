using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectRelationCollection
    {
        private readonly Dictionary<string, List<ObjectId>> relations;
        private static readonly ObjectId[] emptyRelatedList = new ObjectId[] {};

        public ObjectRelationCollection()
        {
            relations = new Dictionary<string, List<ObjectId>>();
        }

        private ObjectRelationCollection(ObjectRelationCollection collectionToClone)
        {
            relations = new Dictionary<string, List<ObjectId>>();
            foreach (var pair in collectionToClone.relations)
            {
                var clonedList = new List<ObjectId>(pair.Value);
                relations.Add(pair.Key, clonedList);
            }
        }

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

        public ObjectRelationCollection Clone()
        {
            return new ObjectRelationCollection(this);
        }
    }
}