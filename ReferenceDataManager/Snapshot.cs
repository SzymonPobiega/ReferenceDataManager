using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class Snapshot
    {
        //private readonly Dictionary<Guid, DataObject> objects = new Dictionary<Guid, DataObject>();

        public void Load(AbstractCommand command)
        {

        }

        public object GetById(Guid objectId)
        {
            return null;
        }
    }

    public class DataObject
    {
    }
}