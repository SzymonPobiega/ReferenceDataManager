namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class MoveUnitCommand : TypedCommand<Unit>
    {
        public Address NewAddress { get; set; }

        public MoveUnitCommand(ObjectId targetObjectId) : base(targetObjectId)
        {
        }
    }
}