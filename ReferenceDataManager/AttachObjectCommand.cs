using System;

namespace ReferenceDataManager
{
    public class AttachObjectCommand : AbstractCommand
    {
        private readonly Guid firstObjectid;
        private readonly Guid secondObjectId;
        private readonly string relationName;

        public AttachObjectCommand(Guid firstObjectid, Guid secondObjectId, string relationName)
        {
            this.firstObjectid = firstObjectid;
            this.secondObjectId = secondObjectId;
            this.relationName = relationName;
        }

        public override void Execute(ICommandExecutionContext context)
        {
            context.Attach(firstObjectid, secondObjectId, relationName);
        }
    }
}