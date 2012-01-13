using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class DataObject
    {
        private readonly Guid id;

        public DataObject(Guid id)
        {
            this.id = id;
        }

        public IEnumerable<DataObject> GetReleated(string relationname, Snapshot context)
        {
            yield break;
        }

        public Guid Id
        {
            get { return id; }
        }

    }
}