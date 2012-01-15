namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class SetHierarchyRootCommand : AbstractCommand
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

    public class SetHierarchyRootCommandHandler : ICommandHandler<SetHierarchyRootCommand>
    {
        public void Handle(SetHierarchyRootCommand command, ICommandExecutionContext context)
        {
            const string relationName = "Root";
            foreach (var formerRootId in context.GetRelated(relationName))
            {
                context.Detach(formerRootId, relationName);
            }
            context.Attach(command.NodeId, relationName);
        }
    }
}