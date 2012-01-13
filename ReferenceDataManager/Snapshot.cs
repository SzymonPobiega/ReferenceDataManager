using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class Snapshot : ISnapshot
    {
        private readonly CommandsByObjectCollection commandsByObject = new CommandsByObjectCollection();
        private readonly ISnapshot parentSnapshot;

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
            commandsByObject.Add(command);
        }

        public ObjectState GetById(ObjectId objectId)
        {
            var inheritedObjectState = parentSnapshot.GetById(objectId);
            var context = new CommandExecutionContext(objectId, inheritedObjectState);
            commandsByObject.ExecuteCommands(objectId, context);
            return context.Instance;
        }
    }
}