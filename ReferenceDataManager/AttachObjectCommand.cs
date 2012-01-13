using System;

namespace ReferenceDataManager
{
    public class AttachObjectCommand : AbstractCommand
    {
        private readonly Guid refereeObjectId;
        private readonly string relationName;

        public AttachObjectCommand(Guid refererObjectId, Guid refereeObjectId, string relationName)
            : base(refererObjectId)
        {
            this.refereeObjectId = refereeObjectId;
            this.relationName = relationName;
        }

        public override void Execute(ICommandExecutionContext context)
        {
            context.Attach(refereeObjectId, relationName);
        }
    }
}