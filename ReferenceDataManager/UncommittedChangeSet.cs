using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class UncommittedChangeSet : IChangeSet
    {
        private readonly ChangeSetId? parentId;
        private readonly ChangeSetId id = ChangeSetId.NewUniqueId();
        private readonly string comment;
        private readonly List<AbstractCommand> commands = new List<AbstractCommand>();

        public UncommittedChangeSet(ChangeSetId? parentId, string comment)
        {
            this.parentId = parentId;
            this.comment = comment;
        }

        public string Comment
        {
            get { return comment; }
        }

        public UncommittedChangeSet Add(AbstractCommand command)
        {
            commands.Add(command);
            return this;
        }

        public IEnumerable<AbstractCommand> Commands
        {
            get { return commands; }
        }

        public ChangeSetId? ParentId
        {
            get { return parentId; }
        }

        public ChangeSetId Id
        {
            get { return id; }
        }
    }
}