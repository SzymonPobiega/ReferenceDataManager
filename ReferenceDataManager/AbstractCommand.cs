using System;

namespace ReferenceDataManager
{
    public abstract class AbstractCommand
    {
        private readonly Guid targetObjectId;

        protected AbstractCommand(Guid targetObjectId)
        {
            this.targetObjectId = targetObjectId;
        }

        public Guid TargetObjectId
        {
            get { return targetObjectId; }
        }

        public abstract void Execute(ICommandExecutionContext context);
    }
}