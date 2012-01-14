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

        public string RelationName
        {
            get { return relationName; }
        }

        public ObjectId RefereeObjectId
        {
            get { return refereeObjectId; }
        }
    }
}