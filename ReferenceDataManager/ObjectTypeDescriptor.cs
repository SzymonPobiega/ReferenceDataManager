using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class ObjectTypeDescriptor
    {
        private readonly List<AttributeDescriptor> attributes;
        private readonly List<RelationDescriptor> relations;

        public ObjectTypeDescriptor(IEnumerable<AttributeDescriptor> attributes, IEnumerable<RelationDescriptor> relations)
        {
            this.attributes = attributes.ToList();
            this.relations = relations.ToList();
        }

        public List<AttributeDescriptor> Attributes
        {
            get { return attributes; }
        }

        public List<RelationDescriptor> Relations
        {
            get { return relations; }
        }
    }
}