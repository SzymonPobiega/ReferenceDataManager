using System.Collections.Generic;

namespace ReferenceDataManager
{
    public interface IObjectSpaceSnapshot
    {
        T GetById<T>(ObjectId objectId);
        IEnumerable<T> List<T>();
    }
}