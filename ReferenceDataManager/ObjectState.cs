using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectState
    {
        private readonly ObjectId id;
        private readonly ObjectTypeId typeId;
        private readonly ObjectRelationCollection relations;
        private readonly Dictionary<string, object> attributes;

        public ObjectState(ObjectId id, ObjectTypeId typeId)
        {
            this.id = id;
            this.typeId = typeId;
            relations = new ObjectRelationCollection();
            attributes = new Dictionary<string, object>();
        }

        private ObjectState(ObjectState objectStateToClone)
        {
            id = objectStateToClone.id;
            typeId = objectStateToClone.typeId;
            relations = objectStateToClone.relations.Clone();
            attributes = new Dictionary<string, object>(objectStateToClone.attributes);
        }

        public ObjectState Clone()
        {
            return new ObjectState(this);
        }

        public ObjectId Id
        {
            get { return id; }
        }

        public ObjectTypeId TypeId
        {
            get { return typeId; }
        }

        public void Attach(ObjectId refereeObjectId, string relationName)
        {
            relations.Attach(refereeObjectId, relationName);
        }

        public IEnumerable<ObjectId> GetRelated(string relationName)
        {
            return relations.GetRelated(relationName);
        }

        public void ModifyAttribute(string propertyName, object value)
        {
            attributes[propertyName] = value;
        }

        public object GetAttributeValue(string propertyName)
        {
            object existingValue;
            return attributes.TryGetValue(propertyName, out existingValue) ? existingValue : null;
        }

        public void Detach(ObjectId refereeObjectId, string relationName)
        {
            relations.Detach(refereeObjectId, relationName);
        }
    }
}