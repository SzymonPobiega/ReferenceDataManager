namespace ReferenceDataManager
{
    public class AttachObjectCommandHandler : ICommandHandler<AttachObjectCommand>
    {
        public void Handle(AttachObjectCommand command, ICommandExecutionContext context)
        {
            context.Attach(command.RefereeObjectId, command.RelationName);
        }
    }
}