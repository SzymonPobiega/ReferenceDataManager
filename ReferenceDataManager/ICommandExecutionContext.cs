using System;

namespace ReferenceDataManager
{
    public interface ICommandExecutionContext
    {
        void Create(Guid objectTypeId);
        void Attach(Guid refereeObjectId, string relationName);
    }
}