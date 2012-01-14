using System;

namespace ReferenceDataManager
{
    public class CommandExecutionContext : ICommandExecutionContext
    {
        private readonly ObjectId targetObjectId;
        private ObjectState instance;

        public CommandExecutionContext(ObjectId targetObjectId, ObjectState instance)
        {
            this.targetObjectId = targetObjectId;
            this.instance = instance;
        }

        public ObjectState Instance
        {
            get { return instance; }
        }

        public void Create(Guid objectTypeId)
        {
            instance = new ObjectState(targetObjectId);
        }

        public void Attach(ObjectId refereeObjectId, string relationName)
        {
            Instance.Attach(refereeObjectId, relationName);
        }

        public void ModifyProperty(string propertyName, object value)
        {
            Instance.ModifyProperty(propertyName, value);
        }
    }
}