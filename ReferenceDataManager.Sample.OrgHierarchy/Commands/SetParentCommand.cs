namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class SetParentCommand : TypedCommand<HierarchyNode>
    {
        private readonly ObjectId parentNodeId;

        public SetParentCommand(ObjectId nodeId, ObjectId parentNodeId) : base(nodeId)
        {
            this.parentNodeId = parentNodeId;
        }

        public ObjectId ParentNodeId
        {
            get { return parentNodeId; }
        }
    }
}