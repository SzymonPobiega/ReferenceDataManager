namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class CreateHierarchyNodeCommand : TypedCommand<HierarchyNode>
    {
        private readonly ObjectId unitId;
        private readonly ObjectId hierarchyId;

        public CreateHierarchyNodeCommand(ObjectId nodeId, ObjectId unitId, ObjectId hierarchyId) : base(nodeId)
        {
            this.unitId = unitId;
            this.hierarchyId = hierarchyId;
        }

        public ObjectId HierarchyId
        {
            get { return hierarchyId; }
        }

        public ObjectId UnitId
        {
            get { return unitId; }
        }
    }
}