using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class Snapshot : ICommandExecutionContext, ISnapshot
    {
        private readonly CommandsByObjectCollection commandsByObject = new CommandsByObjectCollection();
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


        void ICommandExecutionContext.Attach(Guid refererObjectId, Guid refereeObjectId, string relationName)
        {
            var first = GetById(refererObjectId);
            var second = GetById(refereeObjectId);

        }
    }

    public class Relation
    {
    }
}