namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class AttachToHierarchyCommand : TypedCommand<Unit>
    {
        private readonly ObjectId nodeId;

        public AttachToHierarchyCommand(ObjectId unitId, ObjectId nodeId) : base(unitId)
        {
            this.nodeId = nodeId;
        }

        public ObjectId NodeId
        {
            get { return nodeId; }
        }
    }
}