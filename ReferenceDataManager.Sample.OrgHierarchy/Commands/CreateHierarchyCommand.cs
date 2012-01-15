namespace ReferenceDataManager.Sample.OrgHierarchy.Commands
{
    public class CreateHierarchyCommand : TypedCommand<Hierarchy>
    {
        public CreateHierarchyCommand() : this(ObjectId.NewUniqueId())
        {
        }

        public CreateHierarchyCommand(ObjectId targetObjectId) : base(targetObjectId)
        {
        }
    }
}