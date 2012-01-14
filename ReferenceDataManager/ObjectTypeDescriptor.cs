using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class ObjectTypeDescriptor
    {
        private readonly ObjectTypeId objectTypeId;
        private readonly Type runtimeType;
        private readonly List<AttributeDescriptor> attributes;
        private readonly List<RelationDescriptor> relations;

        public ObjectTypeDescriptor(Type runtimeType, ObjectTypeId objectTypeId, IEnumerable<AttributeDescriptor> attributes, IEnumerable<RelationDescriptor> relations)
        {
            this.attributes = attributes.ToList();
            this.relations = relations.ToList();
            this.objectTypeId = objectTypeId;
            this.runtimeType = runtimeType;
        }

        public Type RuntimeType
        {
            get { return runtimeType; }
        }

        public ObjectTypeId ObjectTypeId
        {
            get { return objectTypeId; }
        }

        public List<AttributeDescriptor> Attributes
        {
            get { return attributes; }
        }

        public List<RelationDescriptor> Relations
        {
            get { return relations; }
        }

        public AttributeDescriptor GetAttributeByPropertyName(string propertyName)
        {
            return attributes.FirstOrDefault(x => x.PropertyName == propertyName);
        }

        public RelationDescriptor GetRelationByPropertyName(string propertyName)
        {
            return relations.FirstOrDefault(x => x.PropertyName == propertyName);
        }
    }
}