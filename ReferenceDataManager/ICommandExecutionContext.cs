using System;

namespace ReferenceDataManager
{
    public interface ICommandExecutionContext
    {
        void Create(Guid objectTypeId);
        void Attach(ObjectId refereeObjectId, string relationName);
        void ModifyProperty(string propertyName, object value);
    }
}