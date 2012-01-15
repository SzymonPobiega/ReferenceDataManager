using System;

namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class AttachChildCommand : AbstractCommand
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

    public class AttachChildCommandHandler : ICommandHandler<AttachChildCommand>
    {
        public void Handle(AttachChildCommand command, ICommandExecutionContext context)
        {
            context.Attach(command.ChildNodeId, "Children");
        }
    }
}