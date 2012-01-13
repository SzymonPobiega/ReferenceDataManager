using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class Snapshot : ICommandExecutionContext, ISnapshot
    {
        private readonly ISnapshot parentSnapshot;
        private readonly Dictionary<Guid, DataObject> objects = new Dictionary<Guid, DataObject>();

        public Snapshot(ISnapshot parentSnapshot)
        {
            this.parentSnapshot = parentSnapshot;
        }

        public Snapshot()
            : this(new NullSnapshot())
        {
        }        

        public void Load(AbstractCommand command)
        {
            command.Execute(this);
        }

        public DataObject GetById(Guid objectId)
        {
            return FindInCurrent(objectId) ?? FindInParent(objectId);
        }

        private DataObject FindInParent(Guid objectId)
        {
            return parentSnapshot.GetById(objectId);
        }

        private DataObject FindInCurrent(Guid objectId)
        {
            DataObject existing;
            return objects.TryGetValue(objectId, out existing) ? existing : null;
        }

        void ICommandExecutionContext.Create(Guid objectTypeId, Guid objectId)
        {
            objects[objectId] = new DataObject(objectId);
        }


        void ICommandExecutionContext.Attach(Guid firstObjectid, Guid secondObjectId, string relationName)
        {
            
        }
    }
}