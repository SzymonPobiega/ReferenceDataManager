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

        public IEnumerable<DataObject> GetReleated(Guid firstObjectid, string relationname)
        {
            yield break;
        }

        public Guid Id
        {
            get {
                return id;
            }
        }
    }
}