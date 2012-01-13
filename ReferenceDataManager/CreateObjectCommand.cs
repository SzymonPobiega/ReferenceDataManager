using System;

namespace ReferenceDataManager
{
    public class CreateObjectCommand : AbstractCommand
    {
        private readonly Guid objectTypeId;
        private readonly Guid objectId;

        public CreateObjectCommand(Guid objectTypeId, Guid objectId)
        {
            this.objectTypeId = objectTypeId;
            this.objectId = objectId;
        }

        public override void Execute(ICommandExecutionContext context)
        {
            context.Create(objectTypeId, objectId);
            //context.Detach(objectId, objectId);
            //context.Attach(objectId, objectId);
            //context.Delete(objectId);
            //context.Modify(objectId, "property", "SomeValue");
        }
    }
}