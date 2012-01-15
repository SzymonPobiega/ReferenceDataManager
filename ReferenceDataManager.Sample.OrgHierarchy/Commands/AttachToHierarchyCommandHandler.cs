namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class AttachToHierarchyCommandHandler : ITypedCommandHandler<AttachToHierarchyCommand, Unit>
    {
        public void Handle(AttachToHierarchyCommand command, ICommandExecutionContext context)
        {
            context.Attach(this, x => x.Nodes, command.NodeId);
        }
    }
}