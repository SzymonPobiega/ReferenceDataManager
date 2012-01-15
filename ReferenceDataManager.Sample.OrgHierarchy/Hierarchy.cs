namespace ReferenceDataManager.Sample.OrgHierarchy
{
    [ObjectType(TypeId)]
    public class Hierarchy
    {
        public const string TypeId = "47F232CC-1EEA-4981-87A1-BD7C7EF9D88A";

        public virtual ObjectId Id { get; protected set; }

        [ObjectRelation]
        protected internal virtual HierarchyNode RootNode { get; protected set; }

        public Unit RootUnit
        {
            get
            {
                return RootNode != null ? RootNode.Unit : null;
            }
        }
    }
}