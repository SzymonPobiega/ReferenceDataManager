using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class Snapshot : ICommandExecutionContext
    {
        private readonly Dictionary<Guid, DataObject> objects = new Dictionary<Guid, DataObject>();

        public void Load(AbstractCommand command)
        {
            command.Execute(this);
        }

        public object GetById(Guid objectId)
        {
            DataObject existing;
            return objects.TryGetValue(objectId, out existing) 
                ? existing 
                : null;
        }

        void ICommandExecutionContext.Create(Guid objectTypeId, Guid objectId)
        {
            objects[objectId] = new DataObject();
        }
    }
}