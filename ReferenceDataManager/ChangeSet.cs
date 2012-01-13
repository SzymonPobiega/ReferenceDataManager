using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class ChangeSet
    {
        private readonly Guid? parentId;
        private readonly Guid id;
        private readonly List<AbstractCommand> commands;

        public ChangeSet(Guid id, Guid? parentId, IEnumerable<AbstractCommand> commands)
        {
            this.id = id;
            this.parentId = parentId;
            this.commands = commands.ToList();
        }

        public IEnumerable<AbstractCommand> Commands
        {
            get { return commands; }
        }

        public Guid? ParentId
        {
            get { return parentId; }
        }

        public Guid Id
        {
            get { return id; }
        }
    }

}