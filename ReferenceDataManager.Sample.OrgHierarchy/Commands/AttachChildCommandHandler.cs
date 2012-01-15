namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class AttachChildCommandHandler : ITypedCommandHandler<AttachChildCommand, HierarchyNode>
    {
        public void Handle(AttachChildCommand command, ICommandExecutionContext context)
        {
            context.Attach(this, x => x.Children, command.ChildNodeId);
        }
    }
}