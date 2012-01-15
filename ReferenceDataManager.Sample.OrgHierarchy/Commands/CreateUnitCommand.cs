namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class CreateUnitCommand : TypedCommand<Unit>
    {
        private readonly string name;
        private readonly Address address;

        public CreateUnitCommand(string name, Address address)
            : this(ObjectId.NewUniqueId(), name, address)
        {
        }

        public CreateUnitCommand(ObjectId targetObjectId, string name, Address address) : base(targetObjectId)
        {
            this.name = name;
            this.address = address;
        }

        public string Name
        {
            get { return name; }
        }

        public Address Address
        {
            get { return address; }
        }
    }
}