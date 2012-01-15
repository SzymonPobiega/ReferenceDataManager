namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class CreateUnitCommandHandler : ITypedCommandHandler<CreateUnitCommand, Unit>
    {
        public void Handle(CreateUnitCommand command, ICommandExecutionContext context)
        {
            context.Create(this);
            context.ModifyAttribute(this, x => x.Name, command.Name);
            context.ModifyAttribute(this, x => x.Address, command.Address);
        }
    }
}