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

        public object GetById(Guid objectId)
        {
            return FindInCurrent(objectId) ?? FindInParent(objectId);
        }

        private object FindInParent(Guid objectId)
        {
            return parentSnapshot.GetById(objectId);
        }

        private object FindInCurrent(Guid objectId)
        {
            DataObject existing;
            return objects.TryGetValue(objectId, out existing) ? existing : null;
        }

        void ICommandExecutionContext.Create(Guid objectTypeId, Guid objectId)
        {
            objects[objectId] = new DataObject();
        }
    }
}