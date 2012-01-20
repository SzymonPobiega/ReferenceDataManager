using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class UpdatableObjectSpaceSnapshot : ObjectSpaceSnapshot, IUpdatableObjectSpaceSnapshot
    {
        private readonly ICommandExecutor commandExecutor;
        private readonly Dictionary<ObjectId, ObjectState> loadedStates = new Dictionary<ObjectId, ObjectState>();

        public UpdatableObjectSpaceSnapshot(ObjectTypeDescriptorRepository objectTypeDescriptorRepository, IDataRetrievalStrategy dataRetrievalStrategy, ICommandExecutor commandExecutor)
            : base(objectTypeDescriptorRepository, dataRetrievalStrategy)
        {
            this.commandExecutor = commandExecutor;
        }

        public void Update(AbstractCommand command)
        {
            ObjectState alreadyLoadedState;
            if (loadedStates.TryGetValue(command.TargetObjectId, out alreadyLoadedState))
            {
                var context = new CommandExecutionContext(command.TargetObjectId, alreadyLoadedState);
                commandExecutor.Execute(command, context);
            }
        }
        protected override ObjectState RetrieveData(ObjectId objectId)
        {
            var data = base.RetrieveData(objectId);
            loadedStates[objectId] = data;
            return data;
        }

        protected override IEnumerable<ObjectState> RetrieveData(ObjectTypeId objectTypeId)
        {
            var data = base.RetrieveData(objectTypeId);
            foreach (var loadedState in data)
            {
                loadedStates[loadedState.Id] = loadedState;                
            }
            return data;
        }
    }
}