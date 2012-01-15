namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class CreateHierarchyCommandHandler : ITypedCommandHandler<CreateHierarchyCommand, Hierarchy>
    {
        public void Handle(CreateHierarchyCommand command, ICommandExecutionContext context)
        {
            context.Create(this);
        }
    }
}