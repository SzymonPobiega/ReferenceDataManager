using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class CompositeCommandExecutionContext
    {
        private readonly Dictionary<ObjectId, CommandExecutionContext> contextMap;

        public CompositeCommandExecutionContext()
        {
            contextMap = new Dictionary<ObjectId, CommandExecutionContext>();
        }

        public CompositeCommandExecutionContext(IEnumerable<ObjectState> inheritedState)
        {
            contextMap = inheritedState.ToDictionary(x => x.Id, x => new CommandExecutionContext(x.Id, x));
        }

        public ICommandExecutionContext GetFor(ObjectId objectId)
        {
            CommandExecutionContext existingContext;
            if (contextMap.TryGetValue(objectId, out existingContext))
            {
                return existingContext;
            }
            var newContext = new CommandExecutionContext(objectId, null);
            contextMap[objectId] = newContext;
            return newContext;
        }

        public IEnumerable<ObjectState> GetAll()
        {
            return contextMap.Values.Select(x => x.Instance).Where(x => x != null);
        }
    }
}