namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class SetHierarchyRootCommand : TypedCommand<Hierarchy>
    {
        private readonly ObjectId nodeId;

        public SetHierarchyRootCommand(ObjectId hierarchyId, ObjectId nodeId) : base(hierarchyId)
        {
            this.nodeId = nodeId;
        }

        public ObjectId NodeId
        {
            get { return nodeId; }
        }
    }
}