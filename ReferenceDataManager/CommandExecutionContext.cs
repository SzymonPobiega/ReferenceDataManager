using System;
using System.Collections.Generic;

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

        public void Create(ObjectTypeId objectTypeId)
        {
            instance = new ObjectState(targetObjectId, objectTypeId);
        }

        public void Delete()
        {
            instance = null;
        }

        public void Attach(ObjectId refereeObjectId, string relationName)
        {
            Instance.Attach(refereeObjectId, relationName);
        }

        public void Detach(ObjectId refereeObjectId, string relationName)
        {
            Instance.Detach(refereeObjectId, relationName);
        }

        public IEnumerable<ObjectId> GetRelated(string relationName)
        {
            return Instance.GetRelated(relationName);
        }

        public void ModifyAttribute(string attributeName, object value)
        {
            Instance.ModifyAttribute(attributeName, value);
        }
    }
}