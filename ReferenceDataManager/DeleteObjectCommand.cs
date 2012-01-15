namespace ReferenceDataManager
{
    public class DeleteObjectCommand : AbstractCommand
    {
        public DeleteObjectCommand(ObjectId targetObjectId) : base(targetObjectId)
        {
        }
    }
}