namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class CreateUnitCommand : AbstractCommand
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public CreateUnitCommand(ObjectId targetObjectId) : base(targetObjectId)
        {
        }
    }

    public class CreateUnitCommandHandler : ICommandHandler<CreateUnitCommand>
    {
        public void Handle(CreateUnitCommand command, ICommandExecutionContext context)
        {
            context.Create(ObjectTypeId.Parse(Unit.TypeId));
            context.ModifyAttribute("Name", command.Name);
            context.ModifyAttribute("Address", command.Address);
        }
    }
}