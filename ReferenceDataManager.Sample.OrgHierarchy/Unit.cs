using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager.Sample.OrgHierarchy
{
    [ObjectType(TypeId)]
    public class Unit
    {
        public const string TypeId = "690B0A35-D115-4AD1-B5ED-CEFFF489028C";

        public virtual ObjectId Id { get; protected set; }

        [ObjectAttribute]
        public virtual string Name { get; protected set; }

        [ObjectAttribute]
        public virtual Address Address { get; protected set; }

        [ObjectRelation]
        protected internal virtual IEnumerable<HierarchyNode> Nodes { get; protected set; }

        public Unit GetParentWithin(Hierarchy hierarchy)
        {
            return GetNodeFor(hierarchy).Parent.Unit;
        }

        public IEnumerable<Unit> GetChildrenWithin(Hierarchy hierarchy)
        {
            return GetNodeFor(hierarchy).Children.Select(x => x.Unit);
        }

        private HierarchyNode GetNodeFor(Hierarchy hierarchy)
        {
            return Nodes.Single(x => x.Context == hierarchy);
        }
    }
}