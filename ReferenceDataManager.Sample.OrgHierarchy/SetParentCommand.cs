namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class SetParentCommand : AbstractCommand
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

    public class SetParentCommandHandler : ICommandHandler<SetParentCommand>
    {
        public void Handle(SetParentCommand command, ICommandExecutionContext context)
        {
            const string relationName = "Parent";
            foreach (var formerParentId in context.GetRelated(relationName))
            {
                context.Detach(formerParentId, relationName);
            }
            context.Attach(command.ParentNodeId, relationName);
        }
    }
}