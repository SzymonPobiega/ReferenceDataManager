namespace ReferenceDataManager
{
    public class CreateObjectCommand : AbstractCommand
    {
        private readonly ObjectTypeId objectTypeId;

        public CreateObjectCommand(ObjectTypeId objectTypeId, ObjectId objectId)
            : base(objectId)
        {
            this.objectTypeId = objectTypeId;
        }

        public ObjectTypeId ObjectTypeId
        {
            get { return objectTypeId; }
        }
    }
}