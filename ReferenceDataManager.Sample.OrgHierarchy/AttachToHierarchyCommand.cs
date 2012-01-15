using System;

namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class AttachToHierarchyCommand : AbstractCommand
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

    public class AttachToHierarchyCommandHandler : ICommandHandler<AttachToHierarchyCommand>
    {
        public void Handle(AttachToHierarchyCommand command, ICommandExecutionContext context)
        {
            context.Attach(command.NodeId, "Nodes");
        }
    }
}