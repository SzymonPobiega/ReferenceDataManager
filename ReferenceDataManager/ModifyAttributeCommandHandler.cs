namespace ReferenceDataManager
{
    public class ModifyAttributeCommandHandler : ICommandHandler<ModifyAttributeCommand>
    {
        public void Handle(ModifyAttributeCommand command, ICommandExecutionContext context)
        {
            context.ModifyAttribute(command.AttributeName, command.Value);
        }
    }
}