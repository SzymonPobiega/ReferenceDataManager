using System;

namespace ReferenceDataManager
{
    public interface ICommandExecutionContext
    {
        void Create(Guid objectTypeId, Guid objectId);
        void Attach(Guid refererObjectId, Guid refereeObjectId, string relationName);
    }
}