using System.Collections.Generic;

namespace ReferenceDataManager.Sample.OrgHierarchy
{
    [ObjectType(TypeId)]
    public class HierarchyNode
    {
        public const string TypeId = "6A3AEBBB-D42D-471A-8AF4-E46CB3DD453B";

        public virtual ObjectId Id { get; protected set; }

        [ObjectRelation]
        protected internal virtual Unit Unit { get; protected set; }

        [ObjectRelation]
        protected internal virtual HierarchyNode Parent { get; protected set; }

        [ObjectRelation]
        protected internal virtual IEnumerable<HierarchyNode> Children { get; protected set; }

        [ObjectRelation]
        protected internal virtual Hierarchy Context { get; protected set; }
    }
}