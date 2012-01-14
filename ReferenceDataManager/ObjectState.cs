using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectState
    {
        private readonly ObjectId id;
        private readonly ObjectTypeId typeId;
        private readonly ObjectRelationCollection relations = new ObjectRelationCollection();
        private readonly Dictionary<string, object> attributes = new Dictionary<string, object>();

        public ObjectState(ObjectId id, ObjectTypeId typeId)
        {
            this.id = id;
            this.typeId = typeId;
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
    }
}