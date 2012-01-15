namespace ReferenceDataManager
{
    public class DeleteObjectCommandHandler : ICommandHandler<DeleteObjectCommand>
    {
        public void Handle(DeleteObjectCommand command, ICommandExecutionContext context)
        {
            context.Delete();
        }
    }
}