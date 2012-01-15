namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class DetachChildCommand : AbstractCommand
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

    public class DetachChildCommandHandler : ICommandHandler<DetachChildCommand>
    {
        public void Handle(DetachChildCommand command, ICommandExecutionContext context)
        {
            context.Detach(command.ChildNodeId, "Childrent");
        }
    }
}