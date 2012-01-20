using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class CommandsByObjectTypeCollection
    {
        private readonly List<AbstractCommand> commands = new List<AbstractCommand>();

        public void Add(AbstractCommand command)
        {
            commands.Add(command);
        }

        public void ExecuteCommands(ObjectTypeId objectTypeId, ICommandExecutor commandExecutor, CompositeCommandExecutionContext compositeContext)
        {
            foreach (var command in commands)
            {
                var context = compositeContext.GetFor(command.TargetObjectId);
                commandExecutor.Execute(command, context);
            }
        }
    }
}