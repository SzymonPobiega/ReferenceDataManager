using System;

namespace ReferenceDataManager
{
    public class Snapshot : ISnapshot
    {
        private readonly CommandsByObjectCollection commandsByObject = new CommandsByObjectCollection();
        private readonly ISnapshot parentSnapshot;
        private readonly ICommandExecutor commandExecutor;

        public Snapshot(ISnapshot parentSnapshot, ICommandExecutor commandExecutor)
        {
            this.parentSnapshot = parentSnapshot;
            this.commandExecutor = commandExecutor;
        }

        public Snapshot(ICommandExecutor commandExecutor)
            : this(NullSnapshot.Instance, commandExecutor)
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
            commandsByObject.ExecuteCommands(objectId, commandExecutor, context);
            return context.Instance;
        }
    }
}