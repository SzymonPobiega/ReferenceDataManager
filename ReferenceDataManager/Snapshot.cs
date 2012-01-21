using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class Snapshot : ISnapshot
    {
        private readonly ISnapshot parentSnapshot;
        private readonly ICommandExecutor commandExecutor;
        private readonly Dictionary<ObjectId, ObjectState> materializedObjectStates = new Dictionary<ObjectId, ObjectState>();

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

        public ObjectState GetByIdInternal(ObjectId objectId)
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