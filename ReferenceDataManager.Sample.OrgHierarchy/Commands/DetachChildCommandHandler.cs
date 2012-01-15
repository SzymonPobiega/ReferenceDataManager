namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class DetachChildCommandHandler : ITypedCommandHandler<DetachChildCommand, HierarchyNode>
    {
        public void Handle(DetachChildCommand command, ICommandExecutionContext context)
        {
            context.Detach(this, x=> x.Children, command.ChildNodeId);
        }
    }
}