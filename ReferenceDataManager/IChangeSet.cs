using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface IChangeSet
    {
        IEnumerable<AbstractCommand> Commands { get; }
        ChangeSetId? ParentId { get; }
        ChangeSetId Id { get; }
    }
}