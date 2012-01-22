using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class IncrementalCachingSnapshot : ISnapshot
    {
        private readonly ISnapshot parentSnapshot;
        private readonly ICommandExecutor commandExecutor;
        private readonly Dictionary<ObjectId, ObjectState> materializedObjectStates = new Dictionary<ObjectId, ObjectState>();

        public IncrementalCachingSnapshot(ISnapshot parentSnapshot, ICommandExecutor commandExecutor, IChangeSet changeSet)
        {
            this.parentSnapshot = parentSnapshot;
            this.commandExecutor = commandExecutor;
            foreach (var command in changeSet.Commands)
            {
                Load(command);
            }
        }
        
        private void Load(AbstractCommand command)
        {
            var currentObjectState = GetByIdInternal(command.TargetObjectId);
            var context = new CommandExecutionContext(command.TargetObjectId, currentObjectState);
            commandExecutor.Execute(command, context);
            materializedObjectStates[command.TargetObjectId] = context.Instance;
        }

        public IEnumerable<ObjectState> Enumerate()
        {
            return GetParentObjects().Concat(GetOwnObjects());
        }

        private IEnumerable<ObjectState> GetOwnObjects()
        {
            return materializedObjectStates.Values.Where(objectState => objectState != null);
        }

        private IEnumerable<ObjectState> GetParentObjects()
        {
            return parentSnapshot.Enumerate().Where(objectState => !materializedObjectStates.ContainsKey(objectState.Id));
        }

        private ObjectState GetByIdInternal(ObjectId objectId)
        {
            ObjectState ownObject;
            if (materializedObjectStates.TryGetValue(objectId, out ownObject))
            {
                return ownObject;
            }
            var parentObjectState = parentSnapshot.GetById(objectId);
            if (parentObjectState != null)
            {
                parentObjectState = parentObjectState.Clone();
            }
            return parentObjectState ;
        }

        public ObjectState GetById(ObjectId objectId)
        {
            ObjectState ownObject;
            if (materializedObjectStates.TryGetValue(objectId, out ownObject))
            {
                return ownObject;
            }
            return parentSnapshot.GetById(objectId);
        }
    }
}