using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface ICommandExecutionContext
    {
        void Create(ObjectTypeId objectTypeId);
        void Delete();
        void Attach(ObjectId refereeObjectId, string relationName);
        void Detach(ObjectId refereeObjectId, string relationName);
        IEnumerable<ObjectId> GetRelated(string relationName);
        void ModifyAttribute(string attributeName, object value);
    }
}