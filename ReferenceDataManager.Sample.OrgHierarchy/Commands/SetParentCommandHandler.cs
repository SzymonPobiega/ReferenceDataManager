namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class SetParentCommandHandler : ITypedCommandHandler<SetParentCommand, HierarchyNode>
    {
        public void Handle(SetParentCommand command, ICommandExecutionContext context)
        {
            foreach (var formerParentId in context.GetRelated(this, x => x.Parent))
            {
                context.Detach(this, x => x.Parent, formerParentId);
            }
            context.Attach(this, x => x.Parent, command.ParentNodeId);
        }
    }
}