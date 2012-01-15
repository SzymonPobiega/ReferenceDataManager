namespace ReferenceDataManager.Sample.OrgHierarchy
{
    [ObjectType(TypeId)]
    public class Hierarchy
    {
        public const string TypeId = "47F232CC-1EEA-4981-87A1-BD7C7EF9D88A";

        public virtual ObjectId Id { get; protected set; }

        [ObjectRelation]
        public virtual HierarchyNode Root { get; protected set; }
    }
}