using System.Collections.Generic;

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
        public virtual IEnumerable<HierarchyNode> Nodes { get; protected set; }
    }
}