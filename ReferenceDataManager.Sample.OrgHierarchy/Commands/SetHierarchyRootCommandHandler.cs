namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class SetHierarchyRootCommandHandler : ITypedCommandHandler<SetHierarchyRootCommand, Hierarchy>
    {
        public void Handle(SetHierarchyRootCommand command, ICommandExecutionContext context)
        {
            foreach (var formerRootId in context.GetRelated(this, x => x.RootNode))
            {
                context.Detach(this, x => x.RootNode, formerRootId);
            }
            context.Attach(this, x=> x.RootNode, command.NodeId);
        }
    }
}