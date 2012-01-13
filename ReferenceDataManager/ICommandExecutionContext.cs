using System;

namespace ReferenceDataManager
{
    public interface ICommandExecutionContext
    {
        void Create(Guid objectTypeId, Guid objectId);
    }
}