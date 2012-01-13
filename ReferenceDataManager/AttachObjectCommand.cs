using System;

namespace ReferenceDataManager
{
    public class AttachObjectCommand : AbstractCommand
    {
        private readonly ObjectId refereeObjectId;
        private readonly string relationName;

        public AttachObjectCommand(ObjectId refererObjectId, ObjectId refereeObjectId, string relationName)
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