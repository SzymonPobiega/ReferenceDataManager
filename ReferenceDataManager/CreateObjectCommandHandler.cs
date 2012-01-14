namespace ReferenceDataManager
{
    public class CreateObjectCommandHandler : ICommandHandler<CreateObjectCommand>
    {
        public void Handle(CreateObjectCommand command, ICommandExecutionContext context)
        {
            context.Create(command.ObjectTypeId);
        }
    }
}