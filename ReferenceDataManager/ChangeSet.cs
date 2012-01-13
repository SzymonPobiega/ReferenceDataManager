using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceDataManager
{
    public class ChangeSet
    {
        private readonly Guid id;
        private readonly List<AbstractCommand> commands;

        public ChangeSet(Guid id, IEnumerable<AbstractCommand> commands)
        {
            this.id = id;
            this.commands = commands.ToList();
        }

        public Guid Id
        {
            get { return id; }
        }
    }

}