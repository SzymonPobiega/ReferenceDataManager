using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class ChangeSet
    {
        private readonly ChangeSetId? parentId;
        private readonly ChangeSetId id;
        private readonly List<AbstractCommand> commands;

        public ChangeSet(ChangeSetId id, ChangeSetId? parentId, IEnumerable<AbstractCommand> commands)
        {
            this.id = id;
            this.parentId = parentId;
            this.commands = commands.ToList();
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