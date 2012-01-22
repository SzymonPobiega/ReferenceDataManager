using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class ChangeSet : IChangeSet
    {
        private readonly ChangeSetId? parentId;
        private readonly ChangeSetId id;
        private readonly string comment;
        private readonly List<AbstractCommand> commands;

        public ChangeSet(ChangeSetId id, ChangeSetId? parentId, string comment, IEnumerable<AbstractCommand> commands)
        {
            this.id = id;
            this.parentId = parentId;
            this.comment = comment;
            this.commands = commands.ToList();
        }

        public string Comment
        {
            get { return comment; }
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