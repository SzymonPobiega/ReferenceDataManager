using System;

namespace ReferenceDataManager
{
    public class CommandExecutionContext : ICommandExecutionContext
    {
        private readonly Guid targetObjectId;
        private ObjectState instance;

        public CommandExecutionContext(Guid targetObjectId, ObjectState instance)
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

        public void Attach(Guid refereeObjectId, string relationName)
        {
            Instance.Attach(refereeObjectId, relationName);
        }
    }
}