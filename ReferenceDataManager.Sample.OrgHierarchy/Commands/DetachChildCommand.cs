namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class DetachChildCommand : TypedCommand<HierarchyNode>
    {
        private readonly ObjectId childNodeId;

        public DetachChildCommand(ObjectId parentNodeId, ObjectId childNodeId)
            : base(parentNodeId)
        {
            this.childNodeId = childNodeId;
        }

        public ObjectId ChildNodeId
        {
            get { return childNodeId; }
        }
    }
}