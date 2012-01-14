using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly Dictionary<Type, object> handlers = new Dictionary<Type, object>();

        public CommandExecutor RegisterCommandHandler(object commandHandler)
        {
            Type handledType = GetHandledType(commandHandler);
            handlers[handledType] = commandHandler;
            return this;
        }

        private static Type GetHandledType(object commandHandler)
        {
            var handlerType = commandHandler.GetType();
            var handledTypes = handlerType
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (ICommandHandler<>))
                .Select(i => i.GetGenericArguments()[0]);

            if (!handledTypes.Any())
            {
                throw new InvalidOperationException(string.Format("Handler {0} does not handle any commands (does not implement ICommandHandler<> interface).", handlerType.AssemblyQualifiedName));
            }
            if (handledTypes.Count() > 1)
            {
                throw new InvalidOperationException(string.Format("Handler {0} tries to handle more than one command. Split the implementation into two types.", handlerType.AssemblyQualifiedName));
            }
            return handledTypes.First();
        }

        public void Execute(AbstractCommand command, ICommandExecutionContext context)
        {
            var commandType = command.GetType();
            dynamic handler;
            if (!handlers.TryGetValue(commandType, out handler))
            {
                throw new InvalidOperationException(string.Format("No handler for {0} registered.", commandType.AssemblyQualifiedName));
            }
            dynamic dynamicCommand = command;
            handler.Handle(dynamicCommand, context);
        }
    }
}