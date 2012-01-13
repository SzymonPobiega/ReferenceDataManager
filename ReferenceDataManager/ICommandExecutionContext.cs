using System;

namespace ReferenceDataManager
{
    public interface ICommandExecutionContext
    {
        void Create(Guid objectTypeId, Guid objectId);
        void Attach(Guid firstObjectid, Guid secondObjectId, string relationName);
    }
}