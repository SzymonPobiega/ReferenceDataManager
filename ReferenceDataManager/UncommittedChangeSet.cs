using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class UncommittedChangeSet : IChangeSet
    {
        private readonly ChangeSetId? parentId;
        private readonly ChangeSetId id = ChangeSetId.NewUniqueId();
        private readonly List<AbstractCommand> commands = new List<AbstractCommand>();

        public UncommittedChangeSet(ChangeSetId? parentId)
        {
            this.parentId = parentId;
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