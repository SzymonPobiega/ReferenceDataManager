namespace ReferenceDataManager
{
    public abstract class AbstractCommand
    {
        private readonly ObjectId targetObjectId;

        protected AbstractCommand(ObjectId targetObjectId)
        {
            this.targetObjectId = targetObjectId;
        }

        public ObjectId TargetObjectId
        {
            get { return targetObjectId; }
        }
    }
}