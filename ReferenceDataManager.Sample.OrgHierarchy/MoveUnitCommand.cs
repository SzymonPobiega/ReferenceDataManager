using System;

namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class MoveUnitCommand : AbstractCommand
    {
        public Address NewAddress { get; set; }

        public MoveUnitCommand(ObjectId targetObjectId) : base(targetObjectId)
        {
        }
    }

    public class MoveUnitCommandHandler : ICommandHandler<MoveUnitCommand>
    {
        public void Handle(MoveUnitCommand command, ICommandExecutionContext context)
        {
            context.ModifyAttribute("Address", command.NewAddress);
        }
    }
}