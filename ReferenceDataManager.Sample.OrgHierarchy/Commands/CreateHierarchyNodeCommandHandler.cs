namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class CreateHierarchyNodeCommandHandler : ITypedCommandHandler<CreateHierarchyNodeCommand, HierarchyNode>
    {
        public void Handle(CreateHierarchyNodeCommand command, ICommandExecutionContext context)
        {
            context.Create(this);
            context.Attach(this, x => x.Unit, command.UnitId);
            context.Attach(this, x => x.Context, command.HierarchyId);
        }
    }
}