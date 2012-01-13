using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class CommandsByObjectCollection
    {
        private readonly Dictionary<Guid, List<AbstractCommand>> commands = new Dictionary<Guid,List<AbstractCommand>>();

        public void Add(AbstractCommand command)
        {
            List<AbstractCommand> existingList;
            if (commands.TryGetValue(command.TargetObjectId, out existingList))
            {
                existingList.Add(command);
            }
            else
            {
                var newList = new List<AbstractCommand> {command};
                commands[command.TargetObjectId] = newList;
            }
        }

        public void ExecuteCommands(Guid targetObjectId, ICommandExecutionContext context)
        {
            List<AbstractCommand> commandsForObject;
            if (!commands.TryGetValue(targetObjectId, out commandsForObject))
            {
                return;
            }
            foreach (var command in commandsForObject)
            {
                command.Execute(context);
            }
        }
    }
}