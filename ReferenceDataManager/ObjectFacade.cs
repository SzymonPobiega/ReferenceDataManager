using System;

namespace ReferenceDataManager
{
    public class ObjectFacade
    {
        public ObjectFacade(IDataFacade dataFacade)
        {
            
        }

        public T GetById<T>(ObjectId objectId)
        {
            return default(T);
        }
    }
}