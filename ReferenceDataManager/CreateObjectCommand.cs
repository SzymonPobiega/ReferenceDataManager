using System;

namespace ReferenceDataManager
{
    public class CreateObjectCommand : AbstractCommand
    {
        public CreateObjectCommand(Guid objectTypeId, Guid objectId)
        {
        }

        public override void Execute(ICommandExecutionContext context)
        {
            
        }
    }
}