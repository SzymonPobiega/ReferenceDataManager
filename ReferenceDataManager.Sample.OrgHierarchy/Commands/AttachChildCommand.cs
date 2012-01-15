namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class AttachChildCommand : TypedCommand<HierarchyNode>
    {
        private readonly ObjectId childNodeId;

        public AttachChildCommand(ObjectId parentNodeId, ObjectId childNodeId) : base(parentNodeId)
        {
            this.childNodeId = childNodeId;
        }

        public ObjectId ChildNodeId
        {
            get { return childNodeId; }
        }
    }
}