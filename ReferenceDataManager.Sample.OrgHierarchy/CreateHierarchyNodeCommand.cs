namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class CreateHierarchyNodeCommand : AbstractCommand
    {
        private readonly ObjectId unitId;
        private readonly ObjectId hierarchyId;

        public CreateHierarchyNodeCommand(ObjectId nodeId, ObjectId unitId, ObjectId hierarchyId) : base(nodeId)
        {
            this.unitId = unitId;
            this.hierarchyId = hierarchyId;
        }

        public ObjectId HierarchyId
        {
            get { return hierarchyId; }
        }

        public ObjectId UnitId
        {
            get { return unitId; }
        }
    }

    public class CreateHierarchyNodeCommandHandler : ICommandHandler<CreateHierarchyNodeCommand>
    {
        public void Handle(CreateHierarchyNodeCommand command, ICommandExecutionContext context)
        {
            context.Create(ObjectTypeId.Parse(HierarchyNode.TypeId));
            context.Attach(command.UnitId, "Unit");
            context.Attach(command.HierarchyId, "Context");
        }
    }
}