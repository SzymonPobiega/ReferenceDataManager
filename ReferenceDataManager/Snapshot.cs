using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class Snapshot : ISnapshot
    {
        private readonly CommandsByObjectCollection commandsByObject = new CommandsByObjectCollection();
        private readonly CommandsByObjectTypeCollection commandsByType = new CommandsByObjectTypeCollection();
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
            commandsByType.Add(command);
        }

        public ObjectState GetById(ObjectId objectId)
        {
            var inheritedObjectState = parentSnapshot.GetById(objectId);
            var context = new CommandExecutionContext(objectId, inheritedObjectState);
            commandsByObject.ExecuteCommands(objectId, commandExecutor, context);
            return context.Instance;
        }

        public IEnumerable<ObjectState> ListByType(ObjectTypeId objectTypeId)
        {
            var inheritedState = parentSnapshot.ListByType(objectTypeId);
            var context = new CompositeCommandExecutionContext(inheritedState);
            commandsByType.ExecuteCommands(objectTypeId, commandExecutor, context);

            return context.GetAll()
                .Where(x => x.TypeId == objectTypeId);
        }
    }
}