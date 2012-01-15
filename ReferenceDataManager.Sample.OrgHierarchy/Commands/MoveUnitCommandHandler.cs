namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class MoveUnitCommandHandler : ITypedCommandHandler<MoveUnitCommand, Unit>
    {
        public void Handle(MoveUnitCommand command, ICommandExecutionContext context)
        {
            context.ModifyAttribute(this, x => x.Address, command.NewAddress);
        }
    }
}